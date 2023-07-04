using PostViewer.Api.Data.Models.Comment;
using PostViewer.Api.Services.ModelsApi;

namespace PostViewer.Api.Services.CommentsService
{
    public interface ICommentsService
    {
        /// <summary>
        /// Adds a comment to the db
        /// </summary>
        /// <param name="request">Request model with the new comment data</param>
        /// <returns><see langword="true"/> if addition was successful or <see langword="false"/> if not</returns>
        Task<bool> AddComment(AddCommentRequest request);

        /// <summary>
        /// Deletes a comment from the db
        /// </summary>
        /// <param name="commentId">The comment's Id</param>
        /// <returns><see langword="true"/> if deletion was successful or <see langword="false"/> if not</returns>
        Task<bool> DeleteComment(int commentId);

        /// <summary>
        /// Gets all the comments for a post
        /// </summary>
        /// <param name="postId">The post's Id</param>
        /// <returns>List of comments</returns>
        Task<List<CommentApi>> GetPostComments(int postId);

        /// <summary>
        /// Gets the comments for their corresponding posts
        /// </summary>
        /// <param name="postsIds">List of posts Ids</param>
        /// <returns>Dictionary with all the comments for their corresponding posts</returns>
        Task<Dictionary<int, int>> GetPostsComments(List<int> postsIds);
    }
}
