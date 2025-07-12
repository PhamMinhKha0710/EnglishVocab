using EnglishVocab.Domain.Common;
using System;

namespace EnglishVocab.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; } // Changed to string to match AspNetUsers Id type
        public string Message { get; set; }
        public string Type { get; set; } // achievement, reminder, system
        public bool IsRead { get; set; } = false;
        public string RedirectUrl { get; set; } // URL để chuyển hướng khi nhấn vào thông báo
    }
} 
 