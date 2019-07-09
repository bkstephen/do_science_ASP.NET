using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseAccess;

namespace DatabaseAccess
{
    public class SQLDataAccess
    {

        /* Gets a post's all relevant info by passing the post ID*/
        public IEnumerable<PostMapDB> getPostMapById(int Id)
        {

            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionstring);
            cnn.Open();

            string sql = "SELECT * FROM dbo.PostMapDB WHERE Id='" + Id + "'";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = command.ExecuteReader(); //ok
            
            List<PostMapDB> Output = new List<PostMapDB>();

            while (dataReader.Read())
            {
                Output.Add(new PostMapDB {
                    Id = dataReader.GetInt32(0),
                    PostName = dataReader.GetString(1),
                    User = dataReader.GetString(2),
                    Date = dataReader.GetDateTime(3),                    
                    InitText = dataReader.GetString(5),                    
                    Likes = dataReader.GetInt32(7),
                    Dislikes = dataReader.GetInt32(8)});

                // add the values of variable declared at "PostMapDB" class into a list
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            return Output;
        }

        public IEnumerable<CommentsDB> getCommentById(int Id)
        {

            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionstring);
            cnn.Open();

            string sql = "SELECT * FROM dbo.CommentsDB WHERE Id='" + Id + "'";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = command.ExecuteReader(); //ok

            List<CommentsDB> Output = new List<CommentsDB>();

            while (dataReader.Read())
            {
                Output.Add(new CommentsDB
                {
                    Id = dataReader.GetInt32(0),
                    User = dataReader.GetString(1),
                    Text = dataReader.GetString(2),
                    Date = dataReader.GetDateTime(3),
                    PostID = dataReader.GetInt32(5),
                    Likes = dataReader.GetInt32(6),
                    Dislikes = dataReader.GetInt32(7)
                });

                // add the values of variable declared at "PostMapDB" class into a list
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            return Output;
        }


        /* Gets all comments of a post by passing the post ID*/
        public IEnumerable<CommentsDB> getCommentsDB(int PostId)
        {

            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            string sql = "SELECT * FROM dbo.CommentsDB WHERE PostID ='"+PostId+"'";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = command.ExecuteReader(); //ok

            List<CommentsDB> Output = new List<CommentsDB>();
            while (dataReader.Read())
            {
                Output.Add(new CommentsDB
                {   Id = dataReader.GetInt32(0),
                    User = dataReader.GetString(1),
                    Text = dataReader.GetString(2),
                    Date = dataReader.GetDateTime(3),
                    PostID = dataReader.GetInt32(5),
                    Likes = dataReader.GetInt32(6),
                    Dislikes = dataReader.GetInt32(7)
                }); ;

                // add the values of variable declared at "CommentsDB" class into a list
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            return Output;
        }

        public IEnumerable<PostMapDB> getAllPosts()
        {

            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionstring);
            cnn.Open();

            string sql = "SELECT * FROM dbo.PostMapDB";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = command.ExecuteReader(); //ok

            List<PostMapDB> Output = new List<PostMapDB>();

            while (dataReader.Read())
            {
                Output.Add(new PostMapDB
                {
                    Id = dataReader.GetInt32(0),
                    PostName = dataReader.GetString(1),
                    User = dataReader.GetString(2),
                    Date = dataReader.GetDateTime(3),                    
                    InitText = dataReader.GetString(5),                    
                    Likes = dataReader.GetInt32(7),
                    Dislikes = dataReader.GetInt32(8)
                });

                // add the values of variable declared at "PostMapDB" class into a list
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            return Output;
        }



        public string addComment(int postID, string user, string text)
        {
            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;

            cnn = new SqlConnection(connectionstring);            

            string sql, Output = "";
            SqlCommand cmd = new SqlCommand();

            sql = " INSERT INTO dbo.CommentsDB ([User], Text, PostID, Date, Likes, Dislikes) VALUES (@User, @Text, @PostID, @Date, 1, 1)";  //this method works for adding data
            cmd.Parameters.AddWithValue("@User", user); //adding custom variables using the reference @testName
            cmd.Parameters.AddWithValue("@Text", text);
            cmd.Parameters.AddWithValue("@PostID", postID);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow);
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

            return Output;

        }


        public int addpost(string user, string text, string postName)
        {
            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;

            cnn = new SqlConnection(connectionstring);

            string sql= "";
            int Output = 0;
            SqlCommand cmd = new SqlCommand();
                      
            sql = " INSERT INTO dbo.PostMapDB ([User], PostName, InitText, Date, Likes, Dislikes) VALUES (@User, @PostName, @InitText, @Date, 1, 1)";  //this method works for adding data
            cmd.Parameters.AddWithValue("@User", user); //adding custom variables using the reference @testName
            cmd.Parameters.AddWithValue("@PostName", postName);
            cmd.Parameters.AddWithValue("@InitText", text);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow); ;                    
            
            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            cmd.ExecuteNonQuery();

            sql = "SELECT Id FROM dbo.PostMapDB WHERE [User]='"+ user + "'AND InitText='"+text+"'";
            cmd.CommandText = sql;
            SqlDataReader dataReader = cmd.ExecuteReader(); //ok

            while (dataReader.Read()) { 
                Output = dataReader.GetInt32(0);
            }

            cmd.Dispose();
            cnn.Close();

            return Output;

        }

        public string deletePost(int postID)
        {
            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;

            cnn = new SqlConnection(connectionstring);

            string sql, Output = "";
            SqlCommand cmd = new SqlCommand();

            sql = " DELETE FROM dbo.PostMapDB WHERE Id='"+postID+"'";  //this method works for adding data

            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

            return Output;

        }

        public string deleteComment(int commentID)
        {
            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;

            cnn = new SqlConnection(connectionstring);

            string sql, Output = "";
            SqlCommand cmd = new SqlCommand();

            sql = " DELETE FROM dbo.CommentsDB WHERE Id='" + commentID + "'";  //this method works for adding data

            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

            return Output;

        }

        public string updatePost(int postID, string text)
        {
            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;

            cnn = new SqlConnection(connectionstring);

            string sql, Output = "";
            SqlCommand cmd = new SqlCommand();

            sql = " UPDATE dbo.PostMapDB SET InitText='"+text+"'  WHERE Id='" + postID + "'";  //this method works for adding data

            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

            return Output;

        }

        public string updateComment(int comId, string text)
        {
            string connectionstring = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-do_sciences_org-620709FD-3B92-41A2-8DF3-4B4700A3A9B6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection cnn;

            cnn = new SqlConnection(connectionstring);

            string sql, Output = "";
            SqlCommand cmd = new SqlCommand();

            sql = " UPDATE dbo.CommentsDB SET Text='" + text + "'  WHERE Id='" + comId + "'";  //this method works for adding data

            cmd.Connection = cnn;
            cmd.CommandText = sql;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

            return Output;

        }


    }


}
