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
    class RequestDAL
    {
        public void CreateRequestTable()
        {
            var dbs = DALHelper.Connection;
            dbs.CreateTable<RequestTable>();
        }
        public void AddRequest(RequestTable request)
        {
            var dbs = DALHelper.Connection;
            dbs.Insert(request);
        }

        public void DeleteRequest(RequestTable request)
        {
            var dbs = DALHelper.Connection;
            dbs.Delete(request);
        }

        public void UpdateRequest(RequestTable request)
        {
            var dbs = DALHelper.Connection;
            dbs.Update(request);
        }

        public SQLite.TableQuery<RequestTable> GetAllRequests()
        {
            var dbs = DALHelper.Connection;
            var data = dbs.Table<RequestTable>();
            return data;
        }
    }
}