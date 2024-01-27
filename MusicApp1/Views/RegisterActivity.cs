using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        RegisterVM registerVM;

        public RegisterActivity()
        {
            registerVM = new RegisterVM();
        }

        EditText txtEmail;
        EditText txtPassword;
        EditText txtUserName;
        CheckBox chkAdmin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            var btncreate = FindViewById<FloatingActionButton>(Resource.Id.btnRegister);
            txtEmail = FindViewById<EditText>(Resource.Id.regEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.regPassword);
            txtUserName = FindViewById<EditText>(Resource.Id.regUser);
            chkAdmin = FindViewById<CheckBox>(Resource.Id.checkAdmin);
            btncreate.Click += ncreate_Click;

        }
        private void ncreate_Click(object sender, EventArgs e)
        {
            try
            {
                LoginTable table = new LoginTable();
                table.email = txtEmail.Text;
                table.password = txtPassword.Text;
                table.username = txtUserName.Text;
                table.is_admin = chkAdmin.Checked;
                registerVM.AddLogin(table);
                Toast.MakeText(this, "User Created…", ToastLength.Long).Show();
                StartActivity(typeof(LoginActivity));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}