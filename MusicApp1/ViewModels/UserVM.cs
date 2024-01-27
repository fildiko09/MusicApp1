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
    class UserVM
    {
        TopicBLL topicBLL = new TopicBLL();
        QuizBLL quizBLL = new QuizBLL();
        QuestionBLL questionBLL = new QuestionBLL();
        LoginBLL loginBLL = new LoginBLL();

        public TopicTable[] GetTopicsInCategory(string category)
        {
            return topicBLL.GetTopicsInCategory(category);
        }

        public QuizTable[] GetQuizzesInTopic(int topicID)
        {
            return quizBLL.GetQuizzesInTopic(topicID);
        }

        public QuestionTable[] GetQuestionsInQuiz(int quizID)
        {
            return questionBLL.GetQuestionsInQuiz(quizID);
        }

        public LoginTable GetUserById(int id)
        {
            return loginBLL.GetLoginById(id);
        }
    }
}