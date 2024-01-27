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
    class LoginVM
    {
        LoginBLL loginBLL = new LoginBLL();

        public void CreateTable()
        {
            loginBLL.CreateLoginTable();
        }

        public void AddLogin(LoginTable login)
        {
            loginBLL.AddLogin(login);
        }

        public void UpdateLogin(LoginTable login)
        {
            loginBLL.UpdateLogin(login);
        }

        public void DeleteLogin(LoginTable login)
        {
            loginBLL.DeleteLogin(login);
        }

        public LoginTable GetLoginByEmailAndPassword(string email, string password)
        {
            return loginBLL.GetLoginByEmailAndPassword(email, password);
        }

        public LoginTable GetLoginById(int id)
        {
            return loginBLL.GetLoginById(id);
        }
    }
}