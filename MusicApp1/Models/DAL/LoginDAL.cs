using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicApp1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.Models.DAL
{
    class LoginDAL
    {
        public void CreateLoginTable()
        {
            var dbs = DALHelper.Connection;
            dbs.CreateTable<LoginTable>();
        }
        public void AddLogin(LoginTable login)
        {
            var dbs = DALHelper.Connection;
            dbs.Insert(login);
        }

        public void DeleteLogin(LoginTable login)
        {
            var dbs = DALHelper.Connection;
            dbs.Delete(login);
        }

        public void UpdateLogin(LoginTable login)
        {
            var dbs = DALHelper.Connection;
            dbs.Update(login);
        }

        public SQLite.TableQuery<LoginTable> GetAllLogin()
        {
            var dbs = DALHelper.Connection;
            var data = dbs.Table<LoginTable>();
            return data;
        }
    }
}