using Microsoft.AspNetCore.Mvc;
using PostViewer.Api.Services.ModelsApi;
using PostViewer.Api.Services.PostsService;
using static PostViewer.Api.Common.Constants;

namespace PostViewer.Api.Controllers
{
    [ApiController]
    [Route(Posts.POSTS_CONTROLLER_ROUTE)]
    public class PostsController : Controller
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpDelete]
        [Route(Posts.DELETE_POST_ENDPOINT)]
        public async Task<bool> DeletePost([FromRoute] int postId) 
        {
            return await postsService.DeletePost(postId);
        }

        [HttpGet]
        [Route(Posts.GET_POSTS_ENDPOINT)]
        public async Task<List<PostApi>> GetPosts()
        {
            return await postsService.GetPosts();
        }

        [HttpGet]
        [Route(Posts.GET_POST_DETAILS_ENDPOINT)]
        public async Task<PostDetailsApi> GetPostDetails([FromRoute] int postId)
        {
            return await postsService.GetPostDetails(postId);
        }

        [HttpGet]
        [Route(Posts.REFRESH_ALL_DATA_ENDPOINT)]
        public async Task<bool> RefreshData()
        {
            return await postsService.RefreshData();
        }
    }
}
