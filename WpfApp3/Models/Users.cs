namespace WpfApp3.Models
{
    internal class Users
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDoctor { get; set; }

    }
}
