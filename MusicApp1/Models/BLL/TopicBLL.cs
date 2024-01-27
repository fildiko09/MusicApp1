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
    class TopicBLL
    {
        TopicDAL topicDAL = new TopicDAL();

        public void CreateTopicTable()
        {
            topicDAL.CreateTopicTable();
        }

        public void AddTopic(TopicTable topic)
        {
            topicDAL.AddTopic(topic);
        }

        public void DeleteTopic(TopicTable topic)
        {
            topicDAL.DeleteTopic(topic);
        }

        public void UpdateTopic(TopicTable topic)
        {
            topicDAL.UpdateTopic(topic);
        }

        public TopicTable[] GetAllTopics()
        {
            return topicDAL.GetAllTopics().ToArray();
        }

        public TopicTable GetTopicByID(int id)
        {
            var data = topicDAL.GetAllTopics();
            var data11 = data.Where(x => x.topicId == id).FirstOrDefault();
            return data11;
        }

        public TopicTable[] GetTopicsInCategory(string category)
        {
            var data = topicDAL.GetAllTopics();
            var data11 = data.Where(x => x.category == category);
            return data11.ToArray();
        }
    }
}