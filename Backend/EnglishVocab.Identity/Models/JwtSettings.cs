namespace EnglishVocab.Identity.Models
{
    public class JwtSettings
    {
        public string Key { get; set; } 
        public string Issuer { get; set; } 
        public string Audience { get; set; } 
        public int DurationInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
} 