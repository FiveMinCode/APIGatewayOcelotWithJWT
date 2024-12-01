namespace Auth.API.Model
{
    public class Authentication
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
