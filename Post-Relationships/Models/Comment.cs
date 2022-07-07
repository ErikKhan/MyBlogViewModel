namespace Post_Relationships.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime Created { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
