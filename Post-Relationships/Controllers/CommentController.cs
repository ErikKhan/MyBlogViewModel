using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Post_Relationships.Data;
using Post_Relationships.Models;

namespace Post_Relationships.Controllers
{
    public class CommentController : Controller
    {

        ApplicationDbContext Context;
        public CommentController(ApplicationDbContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var commentList = Context.Comments.Include(x => x.Post).ToList();
            return View(commentList);
        }
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(Context.Posts, "Id", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id, string commenttext, DateTime created, int postid)
        {
            var Comment = new Comment();
            Comment.CommentText = commenttext;
            Comment.Created = DateTime.Now;
            Comment.PostId = postid;
            Context.Comments.Add(Comment);
            Context.SaveChanges();

            return RedirectToAction("Index", "Post");
        }
        public IActionResult Edit(int? id)
        {
            ViewData["PostId"] = new SelectList(Context.Posts, "Id", "Title");
            var Comment = Context.Comments.SingleOrDefault(x => x.CommentId == id);
            return View(Comment);
        }
        [HttpPost]
        public IActionResult Edit(int id, string commenttext, DateTime created, int postid)
        {
            var Comment = Context.Comments.SingleOrDefault(x => x.CommentId == id);
            Comment.CommentText = commenttext;
            Comment.Created = DateTime.Now;
            Comment.PostId = postid;
            Context.SaveChanges();
            return RedirectToAction("Index", "Post");
        }  
        public IActionResult AllDetail(int? Id)
        {
            List<Comment> comment=Context.Comments.Include(x=>x.PostId==Id).ToList();
            return View();
        }
    }
}
 /*
     < h3 > @Model.Title </ h3 >
    < h4 > @Model.Content </ h4 >
    < h5 > @Model.CreatedOn </ h5 >
     < a class= "btn btn-primary btn-lg mx-3 my-3" asp - action = "Edit" asp - route - id = "@Model.Id" > Edit </ a >
     < a class= "btn btn-danger btn-lg mx-3 my-3" asp - action = "Delete" asp - route - id = "@Model.Id" > Delete </ a >

 -->  */