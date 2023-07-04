using Microsoft.AspNetCore.Mvc;
using PostViewer.Api.Services.CommentsService;
using PostViewer.Api.Services.ModelsApi;
using static PostViewer.Api.Common.Constants;

namespace PostViewer.Api.Controllers
{
    [ApiController]
    [Route(Comments.COMMENTS_CONTROLLER_ROUTE)]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService) 
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Route(Comments.ADD_COMMENT_ENDPOINT)]
        public async Task<bool> AddComment(AddCommentRequest request)
        {
            return await commentsService.AddComment(request);
        }

        [HttpDelete]
        [Route(Comments.DELETE_COMMENT_ENDPOINT)]
        public async Task<bool> Delete([FromRoute] int commentId)
        {
            return await commentsService.DeleteComment(commentId);
        }
    }
}
