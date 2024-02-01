using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
using MusicApp1.Models.Entities;
using MusicApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.Views
{
    [Activity(Label = "AddQuizActivity")]
    public class AddQuizActivity : Activity
    {
        EditText txtName;
        AddQuizVM addQuizVM;
        int idTopic;
        TopicTable[] topics;
        string[] items;
        public AddQuizActivity()
        {
            addQuizVM = new AddQuizVM();
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddQuiz);

            var btnSave = FindViewById<FloatingActionButton>(Resource.Id.btnSaveQuiz);
            txtName = FindViewById<EditText>(Resource.Id.addQuizName);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.selectTopic);

            topics = addQuizVM.GetAllTopics();
            items = (from o in topics select o.topicName).ToArray();

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            btnSave.Click += save_Click;
        }


        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            idTopic = topics[e.Position].topicId;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Toast.MakeText(this, "Fields can't be empty", ToastLength.Long).Show();
                }
                else 
                {
                    QuizTable table = new QuizTable();
                    table.quizName = txtName.Text;
                    table.topicIdFK = idTopic;
                    addQuizVM.AddQuiz(table);
                    Toast.MakeText(this, "Quiz Added…", ToastLength.Long).Show();
                    StartActivity(typeof(AdminActivity));
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}