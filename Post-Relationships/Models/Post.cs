using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Post_Relationships.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        public List<Comment>Comments { get; set; }
    }
}
