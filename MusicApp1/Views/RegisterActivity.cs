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
using System.Text.RegularExpressions;
using MusicApp1.Services;

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
        TextView passwordHint;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            var btncreate = FindViewById<FloatingActionButton>(Resource.Id.btnRegister);
            txtEmail = FindViewById<EditText>(Resource.Id.regEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.regPassword);
            txtUserName = FindViewById<EditText>(Resource.Id.regUser);
            chkAdmin = FindViewById<CheckBox>(Resource.Id.checkAdmin);
            passwordHint = FindViewById<TextView>(Resource.Id.passHint);
            btncreate.Click += ncreate_Click;

        }

        Validations validations = new Validations();
        private void ncreate_Click(object sender, EventArgs e)
        {
            try
            {
                bool emailCorrect = validations.ValidateEmail(txtEmail.Text);
                bool passwordCorrect = validations.ValidatePassword(txtPassword.Text);
                bool newEmail = CheckEmailNotUsed(txtEmail.Text);
                bool isEmptyUser = string.IsNullOrWhiteSpace(txtUserName.Text);
                if (emailCorrect == true && passwordCorrect == true && newEmail == true &&  isEmptyUser == false)
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
                else if (newEmail == false)
                {
                    Toast.MakeText(this, "Email already has account...", ToastLength.Long).Show();
                }
                else if (emailCorrect == false)
                {
                    Toast.MakeText(this, "Email invaild...", ToastLength.Long).Show();
                }
                else if (passwordCorrect == false)
                {
                    passwordHint.Visibility = ViewStates.Visible;
                }
                else
                {
                    Toast.MakeText(this, "Fields can't be empty", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private bool CheckEmailNotUsed(string email)
        {
            LoginTable[] allUsers = null;
            allUsers = registerVM.GetAllLogin();
            for (int i = 0; i < allUsers.Length; i++)
            {
                if (email == allUsers[i].email)
                    return false;
            }
            return true;
        }
    }
}