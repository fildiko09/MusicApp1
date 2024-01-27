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
    class TopicDAL
    {
        public void CreateTopicTable()
        {
            var dbs = DALHelper.Connection;
            dbs.CreateTable<TopicTable>();
        }
        public void AddTopic(TopicTable topic)
        {
            var dbs = DALHelper.Connection;
            dbs.Insert(topic);
        }

        public void DeleteTopic(TopicTable topic)
        {
            var dbs = DALHelper.Connection;
            dbs.Delete(topic);
        }

        public void UpdateTopic(TopicTable topic)
        {
            var dbs = DALHelper.Connection;
            dbs.Update(topic);
        }

        public SQLite.TableQuery<TopicTable> GetAllTopics()
        {
            var dbs = DALHelper.Connection;
            var data = dbs.Table<TopicTable>();
            return data;
        }
    }
}