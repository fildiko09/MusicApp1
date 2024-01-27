using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;

namespace MusicApp1.Models.DAL
{
    static class DALHelper
    {
        private static readonly string connectionString = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data4.db3");

        public static SQLiteConnection Connection
        {
            get
            {
                return new SQLiteConnection(connectionString);
            }
        }
    }
}