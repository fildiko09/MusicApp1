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
    class AdminVM
    {
        TopicBLL topicBLL = new TopicBLL();
        QuizBLL quizBLL = new QuizBLL();
        QuestionBLL questionBLL = new QuestionBLL();
        RequestBLL requestBLL = new RequestBLL();
        public void CreateTables()
        {
            topicBLL.CreateTopicTable();
            quizBLL.CreateQuizTable();
            questionBLL.CreateQuestionTable();
            requestBLL.CreateRequestTable();
        }

        public void DeleteRequest(RequestTable request)
        {
            requestBLL.DeleteRequest(request);
        }

        public RequestTable[] GetAllRequests()
        {
            return requestBLL.GetAllRequests();
        }
        public void AddTopic(TopicTable topic)
        {
            topicBLL.AddTopic(topic);
        }

        public void DeleteTopic(TopicTable topic)
        {
            topicBLL.DeleteTopic(topic);
        }

        public void UpdateTopic(TopicTable topic)
        {
            topicBLL.UpdateTopic(topic);
        }

        public TopicTable[] GetAllTopics()
        {
            return topicBLL.GetAllTopics();
        }

        public void DeleteQuiz(QuizTable quiz)
        {
            quizBLL.DeleteQuiz(quiz);
        }

        public void UpdateQuiz(QuizTable quiz)
        {
            quizBLL.UpdateQuiz(quiz);
        }

        public QuizTable[] GetAllQuizzes()
        {
            return quizBLL.GetAllQuizzes();
        }

        public void DeleteQuestion(QuestionTable question)
        {
            questionBLL.DeleteQuestion(question);
        }

        public void UpdateQuestion(QuestionTable question)
        {
            questionBLL.UpdateQuestion(question);
        }

        public QuestionTable[] GetAllQuestions()
        {
            return questionBLL.GetAllQuestions();
        }

        public TopicTable GetTopicByID(int id)
        {
            return topicBLL.GetTopicByID(id);
        }

        public QuizTable GetQuizByID(int id)
        {
            return quizBLL.GetQuizByID(id);
        }
    }
}