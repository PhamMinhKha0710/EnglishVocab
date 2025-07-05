using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EnglishVocab.Application.Common.Models
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
} 