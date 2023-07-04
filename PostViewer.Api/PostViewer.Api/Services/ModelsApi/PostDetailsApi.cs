namespace PostViewer.Api.Services.ModelsApi
{
    public class PostDetailsApi
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public List<CommentApi> Comments { get; set; }
    }
}
