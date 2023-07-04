using PostViewer.Api.Services.ModelsApi;

namespace PostViewer.Api.Services.PostsService
{
    public interface IPostsService
    {
        /// <summary>
        /// Get all the posts
        /// </summary>
        /// <returns>List of all the posts</returns>
        Task<List<PostApi>> GetPosts();

        /// <summary>
        /// Deletes a pos
        /// </summary>
        /// <param name="postId">The post's Id</param>
        /// <returns><see langword="true"/> for success, <see langword="false"/> for failure</returns>
        Task<bool> DeletePost(int postId);

        /// <summary>
        /// Gets the details for a given post
        /// </summary>
        /// <param name="postId">The given post's Id</param>
        /// <returns>The post's details</returns>
        Task<PostDetailsApi> GetPostDetails(int postId);

        /// <summary>
        /// Refreshes the db data with the original source and replaces the jsons
        /// </summary>
        /// <returns><see langword="true"/> for success, <see langword="false"/> for failure</returns>
        Task<bool> RefreshData();
    }
}
