using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Post_Relationships.Data;
using Post_Relationships.Models;

namespace Post_Relationships.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext Context;
        public PostController(ApplicationDbContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            List<Post>AllPost = Context.Posts.ToList();
            return View(AllPost);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id, string title, string content, DateTime createdon)
        {
            
            Post Post = new Post();
            //Post.Id = id;
            Post.Title = title;
            Post.Content = content;
            Post.CreatedOn = DateTime.Now;
            Context.Posts.Add(Post);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var Post = Context.Posts.SingleOrDefault(x => x.Id == id);
            return View(Post);
        }
        [HttpPost]
        public IActionResult Edit(int id, string title, string content, DateTime createdon)
        {
            var Post = Context.Posts.SingleOrDefault(x => x.Id == id);
            
            Post.Title = title;
            Post.Content = content;
            Post.CreatedOn= DateTime.Now;
           
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var Post = Context.Posts.SingleOrDefault(x => x.Id == id);
            return View(Post);
        }
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var Post = Context.Posts.FirstOrDefault(x => x.Id == id);
            Context.Remove(Post);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
  /*       public  async Task<List<Post>> GetPost(int id)
        {
            var x= Context.Posts.Where(x => x.Id == id).ToList();
            return await Context.Posts.Where(x => x.Id == id).ToListAsync();
        }*/
        public IActionResult AllDetail(int? id)
        {
            ViewBag.post= Context.Posts.Where(x => x.Id == id).ToList();
            TempData["pId"]=id;
            var Comments = Context.Comments.Where(x => x.PostId==id).ToList();
            return View(Comments);
        }
        public IActionResult AllDetails(int? id)
        {
            ViewModelAllDetails vmAllDetails = new ViewModelAllDetails();
            TempData["pId"] = id;
            Post Post = Context.Posts.FirstOrDefault(x => x.Id == id);
            vmAllDetails.Post = Post;
            List<Comment> Comment = Context.Comments.Where(x => x.PostId == id).ToList() ;
            vmAllDetails.Comment = Comment;
            return View(vmAllDetails);
        }

    }
}
