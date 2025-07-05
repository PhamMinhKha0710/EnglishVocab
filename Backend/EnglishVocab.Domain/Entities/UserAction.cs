using EnglishVocab.Domain.Common;

namespace EnglishVocab.Domain
{
    public class UserAction : BaseEntity
    {
        public string ActionType { get; set; }
        public string Source { get; set; }
        public long SourceId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }
} 