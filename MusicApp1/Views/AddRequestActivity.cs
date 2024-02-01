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
using Xamarin.Essentials;

namespace MusicApp1.Views
{
    [Activity(Label = "AddRequestActivity")]
    public class AddRequestActivity : Activity
    {
        EditText txtReq;
        AddRequestVM addRequestVM;
        string[] items;
        public AddRequestActivity()
        {
            addRequestVM = new AddRequestVM();
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RequestUser);

            var btnSend = FindViewById<FloatingActionButton>(Resource.Id.btnSendRequest);
            txtReq = FindViewById<EditText>(Resource.Id.addReqTxt);
     
            btnSend.Click += sendReq_Click;
        }


        private void sendReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReq.Text))
                {
                    Toast.MakeText(this, "Fields can't be empty", ToastLength.Long).Show();
                }
                else
                {
                    int myPref = Preferences.Get("my_key", 0);
                    RequestTable table = new RequestTable();
                    table.requestDescription = txtReq.Text;
                    table.loginIdFK = myPref;
                    addRequestVM.AddRequest(table);
                    Toast.MakeText(this, "Request sent…", ToastLength.Long).Show();
                    Preferences.Set("user_view", "history");
                    StartActivity(typeof(MainActivity));
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}