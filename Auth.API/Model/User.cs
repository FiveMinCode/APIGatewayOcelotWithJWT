namespace Auth.API.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string[] Scopes { get; set; }
    }
}
