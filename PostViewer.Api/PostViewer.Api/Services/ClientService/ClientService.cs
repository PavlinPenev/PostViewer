using static PostViewer.Api.Common.Constants;

namespace PostViewer.Api.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly HttpClient httpClient;

        public ClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<string> GetCommentsAsync()
        {
            return httpClient.GetStringAsync(Client.GET_API_COMMENTS);
        }

        public Task<string> GetPostsAsync()
        {
            return httpClient.GetStringAsync(Client.GET_API_POSTS);
        }

        public Task<string> GetUsersAsync()
        {
            return httpClient.GetStringAsync(Client.GET_API_USERS);
        }
    }
}
