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
    class QuestionBLL
    {
        QuestionDAL questionDAL = new QuestionDAL();

        public void CreateQuestionTable()
        {
            questionDAL.CreateQuestionTable();
        }
        public void AddQuestion(QuestionTable question)
        {
            questionDAL.AddQuestion(question);
        }

        public void DeleteQuestion(QuestionTable question)
        {
            questionDAL.DeleteQuestion(question);
        }

        public void UpdateQuestion(QuestionTable question)
        {
            questionDAL.UpdateQuestion(question);
        }

        public QuestionTable[] GetAllQuestions()
        {
            return questionDAL.GetAllQuestions().ToArray();
        }

        public QuestionTable[] GetQuestionsInQuiz(int quizID)
        {
            var data = questionDAL.GetAllQuestions();
            var data11 = data.Where(x => x.quizIdFK == quizID);
            return data11.ToArray();
        }
    }
}