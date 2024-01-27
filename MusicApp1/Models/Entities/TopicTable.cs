using SQLite;

namespace MusicApp1.Models.Entities
{
    class TopicTable
    {
        [PrimaryKey, AutoIncrement]
        public int topicId { get; set; }
        [MaxLength(50)]
        public string topicName { get; set; }
        public string description { get; set; }
        [MaxLength(10)]
        public string category { get; set; }
    }
}