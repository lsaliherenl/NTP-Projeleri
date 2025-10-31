using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

class Server
{
    static TcpListener? listener;
    static List<TcpClient> clients = new List<TcpClient>();
    static readonly object clientsLock = new object();
    static readonly Dictionary<TcpClient, object> clientWriteLocks = new Dictionary<TcpClient, object>();
    static readonly Dictionary<TcpClient, string> clientToName = new Dictionary<TcpClient, string>();
    static readonly Dictionary<string, string> nameToAvatarBase64 = new Dictionary<string, string>();

    static void Main()
    {
        listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server başlatıldı. Bağlantılar bekleniyor...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            lock (clientsLock)
            {
                clients.Add(client);
                clientWriteLocks[client] = new object();
            }
            Console.WriteLine("Yeni kullanıcı bağlandı!");

            Thread t = new Thread(() => ClientHandler(client));
            t.IsBackground = true;
            t.Start();
        }
    }

    static void ClientHandler(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);

        try
        {
            string? line;
            bool registered = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length == 0) continue;
                if (!registered)
                {
                    if (line.StartsWith("USER:"))
                    {
                        var name = line.Substring(5).Trim();
                        if (string.IsNullOrWhiteSpace(name)) continue;
                        lock (clientsLock)
                        {
                            clientToName[client] = name;
                        }
                        Console.WriteLine($"Kayıt: {name}");
                        // Yeni kullanıcıya mevcut listeyi gönder
                        SendUsersListTo(client);
                        // Mevcut avatarları gönder
                        SendKnownAvatarsTo(client);
                        // Herkese JOIN bildir
                        BroadcastRaw($"JOIN:{name}", client);
                        registered = true;
                    }
                    continue;
                }

                if (line.StartsWith("MSG:"))
                {
                    string message = line.Substring(4);
                    string fromName = GetClientName(client) ?? "?";
                    Console.WriteLine($"Mesaj {fromName}: {message}");
                    BroadcastRaw($"MSG:{fromName}:{message}", client);
                }
                else if (line.StartsWith("AVATAR:"))
                {
                    var b64 = line.Substring(7);
                    var name = GetClientName(client);
                    if (!string.IsNullOrEmpty(name))
                    {
                        lock (clientsLock)
                        {
                            nameToAvatarBase64[name] = b64;
                        }
                        BroadcastRaw($"AVATAR:{name}:{b64}", client);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Bir hata oluştu: " + ex.Message);
        }
        finally
        {
            string? leftName = null;
            lock (clientsLock)
            {
                clients.Remove(client);
                clientWriteLocks.Remove(client);
                if (clientToName.TryGetValue(client, out var nm))
                {
                    leftName = nm;
                    clientToName.Remove(client);
                    // Kullanıcı ayrıldıysa avatarını da temizle
                    nameToAvatarBase64.Remove(nm);
                }
            }
            try { client.Close(); } catch { }
            Console.WriteLine("Bir kullanıcı ayrıldı.");
            if (!string.IsNullOrEmpty(leftName))
            {
                BroadcastRaw($"LEAVE:{leftName}", null);
            }
        }
    }

    static string? GetClientName(TcpClient client)
    {
        lock (clientsLock)
        {
            return clientToName.TryGetValue(client, out var n) ? n : null;
        }
    }

    static void SendUsersListTo(TcpClient target)
    {
        string[] names;
        lock (clientsLock)
        {
            names = new List<string>(clientToName.Values).ToArray();
        }
        var payload = "USERS:" + string.Join("|", names);
        SendRawTo(target, payload);
    }

    static void SendKnownAvatarsTo(TcpClient target)
    {
        List<(string name, string b64)> items;
        lock (clientsLock)
        {
            items = new List<(string, string)>();
            foreach (var kv in nameToAvatarBase64)
                items.Add((kv.Key, kv.Value));
        }
        foreach (var (name, b64) in items)
        {
            SendRawTo(target, $"AVATAR:{name}:{b64}");
        }
    }

    static void BroadcastRaw(string message, TcpClient? sender)
    {
        List<TcpClient> snapshot;
        lock (clientsLock)
        {
            snapshot = new List<TcpClient>(clients);
        }

        var toRemove = new List<TcpClient>();
        foreach (var client in snapshot)
        {
            if (client == sender) continue;
            try
            {
                var data = Encoding.UTF8.GetBytes(message + "\n");
                object lockObj;
                lock (clientsLock)
                {
                    if (!clientWriteLocks.TryGetValue(client, out lockObj!)) lockObj = client;
                }
                lock (lockObj)
                {
                    client.GetStream().Write(data, 0, data.Length);
                }
            }
            catch
            {
                toRemove.Add(client);
            }
        }

        if (toRemove.Count > 0)
        {
            lock (clientsLock)
            {
                foreach (var c in toRemove) clients.Remove(c);
            }
        }
    }

    static void SendRawTo(TcpClient client, string message)
    {
        try
        {
            var data = Encoding.UTF8.GetBytes(message + "\n");
            object lockObj;
            lock (clientsLock)
            {
                if (!clientWriteLocks.TryGetValue(client, out lockObj!)) lockObj = client;
            }
            lock (lockObj)
            {
                client.GetStream().Write(data, 0, data.Length);
            }
        }
        catch { }
    }
}
