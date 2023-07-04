using PostViewer.Api.Services.ModelsApi;

namespace PostViewer.Api.Services.UsersService
{
    public interface IUsersService
    {
        /// <summary>
        /// Gets all the users for the posts
        /// </summary>
        /// <param name="userIds">List of user Ids</param>
        /// <returns>List of users with their Id and name</returns>
        Task<List<PostsUserApi>> GetPostsUsers(List<int> userIds);
    }
}
