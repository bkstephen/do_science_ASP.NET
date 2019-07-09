using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseAccess;
using System.Data.SqlClient;
using static DatabaseAccess.SQLDataAccess;


namespace do_sciences_org.Controllers
{
    
    public class PostsController : Controller
    {                       
        [HttpGet]
        public IActionResult Index(PostMapDB list)
        {
            IEnumerable<PostMapDB> dataOfPosts = list.getAllPosts().OrderBy(x => x.Date);
            return View(dataOfPosts);
        }

        [HttpPost]
        public IActionResult Index(string subButton)
        {            
            return RedirectToAction("seePost", new { submit = subButton });            
        }

        [HttpGet]
        public IActionResult seePost(int submit, CommentsDB list)
        {
            ViewBag.PostId = submit;
            IEnumerable<CommentsDB> dataofComments = list.getCommentsDB(submit).OrderBy(x => x.Date);
            return View(dataofComments);
        }

        [System.Web.Mvc.ValidateInput(false)]
        [HttpPost]        
        public IActionResult seePost(string textBox, CommentsDB list, int submit, string user, string btnSubmit, int commentId)
         {
            if (btnSubmit != null)
            {
                switch (btnSubmit) {
                    case "editPost":
                        return RedirectToAction("editPost", new {Id = submit});

                    case "deletePost":
                        list.deletePost(submit);
                        return RedirectToAction("seePost", new { submit = submit });

                    case "editComment":
                        return RedirectToAction("editComment", new { Id = commentId});                        

                    case "deleteComment":
                        list.deleteComment(commentId);
                        return RedirectToAction("seePost", new { submit = submit });
                }              

            }

            if (textBox != null && user != null)
            {
                list.addComment(submit, user, textBox);
            } else
            {
                Models.ErrorViewModel error = new Models.ErrorViewModel();
                error.RequestId = "Create comment error, one of the required values was empty";
                return View("Error", error);
            }           
            return RedirectToAction("seePost", new { submit = submit });
        }


        [HttpGet]
        public IActionResult createPost()
        {

            return View();
        }

        [HttpPost]
        public IActionResult createPost(PostMapDB list, string user, string textBox, string postName )
        {   
            if(user != null && textBox != null && postName != null) { 
                int post = list.addpost(user, textBox, postName);
                return RedirectToAction("seePost", new { submit = post });
            } else
            {
                Models.ErrorViewModel error = new Models.ErrorViewModel();
                error.RequestId = "Create post error, one of the required values was empty";
                return View("Error", error);
            }            
        }


        [HttpGet]
        public IActionResult editPost(int Id, PostMapDB post)
        {

                IEnumerable<PostMapDB> postObj = post.getPostMapById(Id);
                return View(postObj);
        }

        [HttpPost]
        public IActionResult editPost(int submit, string textBox, PostMapDB post)
        {
            post.updatePost(submit, textBox);
            return RedirectToAction("seePost", new { submit = submit }); ;
        }


        [HttpGet]
        public IActionResult editcomment(int Id, CommentsDB com)
        {
            IEnumerable<CommentsDB> comObj = com.getCommentById(Id);
            return View(comObj);
        }

        [HttpPost]
        public IActionResult editcomment(int submit, string textBox, int postId, CommentsDB com)
        {
            com.updateComment(submit, textBox);
            return RedirectToAction("seePost", new { submit = postId }); ;
        }
    }
}