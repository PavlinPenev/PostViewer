namespace PostViewer.Api.Services.ModelsApi
{
    public class AddCommentRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }

        public int PostId { get; set; }
    }
}
