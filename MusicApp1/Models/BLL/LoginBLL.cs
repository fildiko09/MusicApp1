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
    class LoginBLL
    {
        LoginDAL loginDAL = new LoginDAL();

        public void CreateLoginTable()
        {
            loginDAL.CreateLoginTable();
        }

        public void AddLogin(LoginTable login)
        {
            loginDAL.AddLogin(login);
        }

        public void DeleteLogin(LoginTable login)
        {
            loginDAL.DeleteLogin(login);
        }

        public void UpdateLogin(LoginTable login)
        {
            loginDAL.UpdateLogin(login);
        }

        public LoginTable GetLoginByEmailAndPassword(string email, string password)
        {
            var data = loginDAL.GetAllLogin();
            var data11 = data.Where(x => x.email == email && x.password == password).FirstOrDefault();
            return data11;
        }

        public LoginTable GetLoginById(int id)
        {
            var data = loginDAL.GetAllLogin();
            var data11 = data.Where(x => x.id == id).FirstOrDefault();
            return data11;
        }
    }
}