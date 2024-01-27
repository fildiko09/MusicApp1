using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicApp1.Models.DAL;
using MusicApp1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.Models.BLL
{
    class QuizBLL
    {
        QuizDAL quizDAL = new QuizDAL();

        public void CreateQuizTable()
        {
            quizDAL.CreateQuizTable();
        }
        public void AddQuiz(QuizTable quiz)
        {
            quizDAL.AddQuiz(quiz);
        }

        public void DeleteQuiz(QuizTable quiz)
        {
            quizDAL.DeleteQuiz(quiz);
        }

        public void UpdateQuiz(QuizTable quiz)
        {
            quizDAL.UpdateQuiz(quiz);
        }

        public QuizTable[] GetAllQuizzes()
        {
            return quizDAL.GetAllQuizzes().ToArray();
        }

        public QuizTable GetQuizByID(int id)
        {
            var data = quizDAL.GetAllQuizzes();
            var data11 = data.Where(x => x.quizId == id).FirstOrDefault();
            return data11;
        }

        public QuizTable[] GetQuizzesInTopic(int topicID)
        {
            var data = quizDAL.GetAllQuizzes();
            var data11 = data.Where(x => x.topicIdFK == topicID);
            return data11.ToArray();
        }
    }
}