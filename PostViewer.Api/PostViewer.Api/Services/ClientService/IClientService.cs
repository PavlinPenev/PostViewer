namespace PostViewer.Api.Services.ClientService
{
    public interface IClientService
    {
        Task<string> GetUsersAsync();

        Task<string> GetCommentsAsync();

        Task<string> GetPostsAsync();
    }
}
