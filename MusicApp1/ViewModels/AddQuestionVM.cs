using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicApp1.Models.BLL;
using MusicApp1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.ViewModels
{
    class AddQuestionVM
    {
        QuestionBLL questionBLL = new QuestionBLL();
        QuizBLL quizBLL = new QuizBLL();

        public void AddQuestion(QuestionTable question)
        {
            questionBLL.AddQuestion(question);
        }

        public QuizTable[] GetAllQuizzes()
        {
            return quizBLL.GetAllQuizzes();
        }
    }
}