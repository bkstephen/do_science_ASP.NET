using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class PostMapDB : SQLDataAccess
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public Byte[] Time { get; set; }
        public string InitText { get; set; }
        //public int PostID { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
