using SQLite;

namespace MusicApp1.Models.Entities
{
    class RequestTable
    {
        [PrimaryKey, AutoIncrement]
        public int requestId { get; set; }
        public int loginIdFK { get; set; }
        [MaxLength(50)]
        public string requestDescription { get; set; }
    }
}