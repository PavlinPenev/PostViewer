using Newtonsoft.Json;
using PostViewer.Api.Data.Models.Comment;
using PostViewer.Api.Data.Models.Post;
using PostViewer.Api.Services.ClientService;
using PostViewer.Api.Services.CommentsService;
using PostViewer.Api.Services.Helpers;
using PostViewer.Api.Services.ModelsApi;
using PostViewer.Api.Services.UsersService;
using System.Runtime.CompilerServices;
using static PostViewer.Api.Common.Constants;

namespace PostViewer.Api.Services.PostsService
{
    public class PostsService : IPostsService
    {
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IClientService clientService;

        public PostsService(
            IUsersService usersService, 
            ICommentsService commentsService, 
            IClientService clientService) 
        {
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.clientService = clientService;
        }

        public async Task<bool> DeletePost(int postId)
        {
            var dbPosts = await GetPostsFromData();

            var dbPost = dbPosts.FirstOrDefault(x => x.Id == postId);

            dbPosts.Remove(dbPost);

            try
            {
                SaveChanges(dbPosts);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PostDetailsApi> GetPostDetails(int postId)
        {
            var dbPosts = await GetPostsFromData();

            var dbPost = dbPosts.FirstOrDefault(x => x.Id == postId);

            var postComments = await commentsService.GetPostComments(postId);

            var post = new PostDetailsApi
            {
                Id = dbPost.Id,
                Body = dbPost.Body,
                Title = dbPost.Title,
                Comments = postComments
            };

            return post;
        }

        public async Task<List<PostApi>> GetPosts()
        {
            var dbPosts = await GetPostsFromData();

            var userIds = dbPosts.Select(x => x.UserId).ToList();

            var postsIds = dbPosts.Select(x => x.Id).ToList();

            var postsUsers = await usersService.GetPostsUsers(userIds);

            var posts = dbPosts
                .Select(x => new PostApi
                {
                    Id = x.Id,
                    Title= x.Title,
                    Author = postsUsers.FirstOrDefault(y => y.Id == x.UserId)?.Name
                })
                .OrderBy(x => x.Author)
                .ThenBy(x => x.Title)
                .ToList();

            var postsCommentsDictionary = await commentsService.GetPostsComments(postsIds);

            foreach (var post in posts)
            {
                post.NumberOfComments = postsCommentsDictionary.FirstOrDefault(x => x.Key == post.Id).Value;
            }

            return posts;
        }

        public async Task<bool> RefreshData()
        {
            try
            {
                var postApiData = await clientService.GetPostsAsync();
                var commentsApiData = await clientService.GetCommentsAsync();
                var usersApiData = await clientService.GetUsersAsync();

                OnRefreshSaveChanges(postApiData, commentsApiData, usersApiData);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void SaveChanges(List<Post> dbPosts)
        {
            var json = JsonConvert.SerializeObject(dbPosts, JsonHelper.GetJsonSettings());
            File.WriteAllText(Posts.POSTS_JSON_PATH, json);
        }

        private static void OnRefreshSaveChanges(string postData, string commentsData, string usersData)
        {
            File.WriteAllText(Posts.POSTS_JSON_PATH, postData);
            File.WriteAllText(Comments.COMMENTS_JSON_PATH, commentsData);
            File.WriteAllText(Users.USERS_JSON_PATH, usersData);
        }

        private async Task<List<Post>> GetPostsFromData()
        {
            var json = Posts.POSTS_JSON_PATH;

            var jsonInput = File.ReadAllText(json);

            var posts = JsonConvert.DeserializeObject<List<Post>>(jsonInput);

            return posts ?? new List<Post>();
        }
    }
}
