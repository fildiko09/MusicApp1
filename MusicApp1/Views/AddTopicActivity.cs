using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
using MusicApp1.Models.BLL;
using MusicApp1.Models.DAL;
using MusicApp1.Models.Entities;
using MusicApp1.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicApp1.Views
{
    [Activity(Label = "AddTopicActivity")]
    public class AddTopicActivity : Activity
    {
        EditText txtName;
        EditText txtDescription;
        AddTopicVM addTopicVM;
        string txtCategory;
        public AddTopicActivity()
        {
            addTopicVM = new AddTopicVM();
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddTopic);

            var btnSave = FindViewById<FloatingActionButton>(Resource.Id.btnSaveTopic);
            txtName = FindViewById<EditText>(Resource.Id.addTopicName);
            txtDescription = FindViewById<EditText>(Resource.Id.addTopicDescription);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.selectCategory);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.categories_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            btnSave.Click += save_Click;
        }


        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            txtCategory = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDescription.Text))
                {
                    Toast.MakeText(this, "Fields can't be empty", ToastLength.Long).Show();
                }
                else 
                {
                    TopicTable table = new TopicTable();
                    //TopicBLL topic = new TopicBLL();
                    table.topicName = txtName.Text;
                    table.description = txtDescription.Text;
                    table.category = txtCategory;
                    addTopicVM.AddTopic(table);
                    Toast.MakeText(this, "Topic Added…", ToastLength.Long).Show();
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