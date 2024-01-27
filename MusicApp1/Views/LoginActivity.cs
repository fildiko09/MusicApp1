using Android.App;
using Android.OS;
using Android.Widget;
using MusicApp1.Models.Entities;
using SQLite;
using System;
using System.IO;
using Xamarin.Essentials;
using MusicApp1.Models.DAL;
using MusicApp1.ViewModels;
using Google.Android.Material.FloatingActionButton;
using Android.Bluetooth;
using MusicApp1.Services;

namespace MusicApp1.Views
{
    [Activity(Theme = "@style/MyTheme.Login", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class LoginActivity : Activity
    {
        LoginVM loginVM;
        public LoginActivity()
        {
            loginVM = new LoginVM();
        }

        EditText email;
        EditText password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            loginVM.CreateTable();

            int logged_id = Preferences.Get("my_key", 0);

            if (logged_id == 0)
            {
                SetContentView(Resource.Layout.Login);

                email = FindViewById<EditText>(Resource.Id.txtEmail);
                password = FindViewById<EditText>(Resource.Id.txtPassword);

                var buttonLogin = FindViewById<FloatingActionButton>(Resource.Id.btnLogin);
                buttonLogin.Click += DoLogin;

                var buttonRegister = FindViewById<FloatingActionButton>(Resource.Id.btnRegister);
                buttonRegister.Click += Register_Click;
            }
            else
            {
                var data = loginVM.GetLoginById(logged_id);
                if (data.is_admin == true)
                {
                    Preferences.Set("default_view", "topics");
                    StartActivity(typeof(AdminActivity));
                }
                else
                {
                    Preferences.Set("user_view", "history");
                    StartActivity(typeof(MainActivity));
                }
            }
        }
        private void Register_Click(object send, EventArgs eve)
        {
            StartActivity(typeof(RegisterActivity));
        }
        public void DoLogin(object sender, EventArgs e)
        {
            try
            {
                var data = loginVM.GetLoginByEmailAndPassword(email.Text, password.Text);
                if (data != null)
                {
                    Preferences.Set("my_key", data.id);
                    Toast.MakeText(this, "Successfully Login", ToastLength.Short).Show();
                    if (data.is_admin == true)
                    {
                        Preferences.Set("default_view", "topics");
                        StartActivity(typeof(AdminActivity));
                    }
                    else
                    {
                        Preferences.Set("user_view", "history");
                        StartActivity(typeof(MainActivity));
                    }
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}