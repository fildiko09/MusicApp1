using Android.App;
using SQLite;

namespace MusicApp1.Models.Entities
{
    class LoginTable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(50)]
        public string email { get; set; }
        [MaxLength(20)]
        public string password { get; set; }
        [MaxLength(20)]
        public string username { get; set; }
        public bool is_admin { get; set; }
    }
}