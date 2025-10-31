namespace ChatClientWPF
{
    public sealed class UserItem
    {
        public string Name { get; set; }
        public string? Avatar { get; set; }

        public UserItem(string name, string? avatar)
        {
            Name = name;
            Avatar = avatar;
        }
    }
}

