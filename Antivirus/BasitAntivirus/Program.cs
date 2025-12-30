using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography; 
using System.Text;

namespace BasitAntivirus
{
    class Program
    {
        
        static Dictionary<string, string> VirusVeritabani = new Dictionary<string, string>
        {
           
            { "44d88612fea8a8f36de82e1278abb02f", "EICAR Test Dosyası (Zararsız)" },
            
           
            { "84c82835a5d21bbcf75a61706d8ab549", "WannaCry Ransomware" }
        };

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       C# Antivirüs Tarayıcısına Hoş Geldin       ");
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\nTaranacak dosyanın tam yolunu yapıştır (Çıkış için 'exit'): ");
                string dosyaYolu = Console.ReadLine().Trim('"'); 

                if (dosyaYolu.ToLower() == "exit") break;

                if (File.Exists(dosyaYolu))
                {
                    DosyayiTara(dosyaYolu);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hata: Dosya bulunamadı! Yolu doğru yazdığından emin ol.");
                    Console.ResetColor();
                }
            }
        }

        static void DosyayiTara(string dosyaYolu)
        {
            Console.WriteLine("Dosya analiz ediliyor...");

            
            string dosyaHash = MD5Hesapla(dosyaYolu);

            Console.WriteLine($"Dosya Hash Değeri (MD5): {dosyaHash}");

            
            if (VirusVeritabani.ContainsKey(dosyaHash))
            {
                Console.Beep(); 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!!!] TEHDİT TESPİT EDİLDİ [!!!]");
                Console.WriteLine($"Tespit Edilen Zararlı: {VirusVeritabani[dosyaHash]}");
                Console.WriteLine("Bu dosyayı hemen silmeniz önerilir!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[+] Temiz. Bu dosya veritabanımızdaki virüslerle eşleşmedi.");
            }
            Console.ResetColor();
        }

        
        static string MD5Hesapla(string dosyaYolu)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(dosyaYolu))
                {
                    var hashBytes = md5.ComputeHash(stream);

                    
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2")); 
                    }
                    return sb.ToString();
                }
            }
        }
    }
}