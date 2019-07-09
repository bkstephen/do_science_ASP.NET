using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class CommentsDB : SQLDataAccess
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Byte[] Time { get; set; }
        public int PostID { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        

    }
}
