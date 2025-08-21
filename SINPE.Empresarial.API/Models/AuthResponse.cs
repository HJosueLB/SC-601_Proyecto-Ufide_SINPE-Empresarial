namespace SINPE.Empresarial.API.Models
{
    public class AuthResponse
    {
        public required string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}