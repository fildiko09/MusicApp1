using SQLite;

namespace MusicApp1.Models.Entities
{
    class QuizTable
    {
        [PrimaryKey, AutoIncrement]
        public int quizId { get; set; }
        public int topicIdFK { get; set; }
        [MaxLength(50)]
        public string quizName { get; set; }
    }
}