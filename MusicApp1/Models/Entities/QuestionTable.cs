using Android.App;
using SQLite;


namespace MusicApp1.Models.Entities
{
    class QuestionTable
    {
        [PrimaryKey, AutoIncrement]
        public int questionId { get; set; }
        public int quizIdFK { get; set; }
        [MaxLength(50)]
        public string question { get; set; }
        [MaxLength(20)]
        public string answerA { get; set; }
        [MaxLength(20)]
        public string answerB { get; set; }
        [MaxLength(20)]
        public string answerC { get; set; }
        [MaxLength(20)]
        public string correctAnswer { get; set; }
    }
}