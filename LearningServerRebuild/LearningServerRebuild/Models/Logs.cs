using System;

namespace LearningServerRebuild.Models
{
    public class Logs
    {
        public Guid id { get; set; }
        public DateTime createdAt { get; set; }
        public string description { get; set; }

        // Relacionamento com User
        public string Log_user { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
