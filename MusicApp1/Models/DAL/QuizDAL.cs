using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicApp1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.Models.DAL
{
    class QuizDAL
    {
        public void CreateQuizTable()
        {
            var dbs = DALHelper.Connection;
            dbs.CreateTable<QuizTable>();
        }
        public void AddQuiz(QuizTable quiz)
        {
            var dbs = DALHelper.Connection;
            dbs.Insert(quiz);
        }

        public void DeleteQuiz(QuizTable quiz)
        {
            var dbs = DALHelper.Connection;
            dbs.Delete(quiz);
        }

        public void UpdateQuiz(QuizTable quiz)
        {
            var dbs = DALHelper.Connection;
            dbs.Update(quiz);
        }

        public SQLite.TableQuery<QuizTable> GetAllQuizzes()
        {
            var dbs = DALHelper.Connection;
            var data = dbs.Table<QuizTable>();
            return data;
        }
    }
}