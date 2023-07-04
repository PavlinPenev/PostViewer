using Newtonsoft.Json;
using PostViewer.Api.Data.Models.User;
using PostViewer.Api.Services.ModelsApi;
using static PostViewer.Api.Common.Constants;

namespace PostViewer.Api.Services.UsersService
{
    public class UsersService : IUsersService
    {
        public async Task<List<PostsUserApi>> GetPostsUsers(List<int> userIds)
        {
            var dbUsers = await GetUsersFromData();

            var users = dbUsers
                .Where(x => userIds.Contains(x.Id))
                .Select(x => new PostsUserApi
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            return users;
        }

        private async Task<List<User>> GetUsersFromData()
        {
            var json = Users.USERS_JSON_PATH;

            var jsonInput = File.ReadAllText(json);

            var users = JsonConvert.DeserializeObject<List<User>>(jsonInput);

            return users ?? new List<User>();
        }
    }
}
