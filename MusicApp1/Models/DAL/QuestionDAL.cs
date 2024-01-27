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
    class QuestionDAL
    {
        public void CreateQuestionTable()
        {
            var dbs = DALHelper.Connection;
            dbs.CreateTable<QuestionTable>();
        }
        public void AddQuestion(QuestionTable question)
        {
            var dbs = DALHelper.Connection;
            dbs.Insert(question);
        }

        public void DeleteQuestion(QuestionTable question)
        {
            var dbs = DALHelper.Connection;
            dbs.Delete(question);
        }

        public void UpdateQuestion(QuestionTable question)
        {
            var dbs = DALHelper.Connection;
            dbs.Update(question);
        }

        public SQLite.TableQuery<QuestionTable> GetAllQuestions()
        {
            var dbs = DALHelper.Connection;
            var data = dbs.Table<QuestionTable>();
            return data;
        }
    }
}