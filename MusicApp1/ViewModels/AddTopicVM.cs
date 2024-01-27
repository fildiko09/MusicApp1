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
    class AddTopicVM
    {
        TopicBLL topicBLL = new TopicBLL();

        public void AddTopic(TopicTable topic)
        {
            topicBLL.AddTopic(topic);
        }
    }
}