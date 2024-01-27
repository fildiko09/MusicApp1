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
    class RegisterVM
    {
        LoginBLL loginBLL = new LoginBLL();
        public void AddLogin(LoginTable login)
        {
            loginBLL.AddLogin(login);
        }
    }
}