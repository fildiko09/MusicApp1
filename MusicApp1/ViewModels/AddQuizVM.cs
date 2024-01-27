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
    class AddQuizVM
    {
        QuizBLL quizBLL = new QuizBLL();
        TopicBLL topicBLL = new TopicBLL();

        public void AddQuiz(QuizTable quiz)
        {
            quizBLL.AddQuiz(quiz);
        }

        public TopicTable[] GetAllTopics()
        {
            return topicBLL.GetAllTopics();
        }
    }
}