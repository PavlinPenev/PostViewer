namespace PostViewer.Api.Common
{
    public static class Constants
    {
        public static class Comments
        {
            public const string COMMENTS_CONTROLLER_ROUTE = "api/comments";

            public const string GET_COMMENTS_ENDPOINT = "get";

            public const string COMMENTS_JSON_PATH = $"{JSON_PATH}/comments.json";

            public const string ADD_COMMENT_ENDPOINT = "add";

            public const string DELETE_COMMENT_ENDPOINT = "delete/{commentId}";
        }

        public static class Posts
        {
            public const string POSTS_CONTROLLER_ROUTE = "api/posts";

            public const string GET_POSTS_ENDPOINT = "get";

            public const string POSTS_JSON_PATH = $"{JSON_PATH}/posts.json";

            public const string GET_POST_DETAILS_ENDPOINT = "get/{postId}";

            public const string REFRESH_ALL_DATA_ENDPOINT = "refresh";

            public const string DELETE_POST_ENDPOINT = "delete/{postId}";
        }

        public static class Users
        {
            public const string USERS_CONTROLLER_ROUTE = "api/users";

            public const string USERS_JSON_PATH = $"{JSON_PATH}/users.json";
        }

        public static class Client
        {
            public const string GET_API_USERS = $"{JSON_API}/users";
            public const string GET_API_POSTS = $"{JSON_API}/posts";
            public const string GET_API_COMMENTS = $"{JSON_API}/comments";
        }

        private const string JSON_PATH = "./Data/Json";
        private const string JSON_API = "https://jsonplaceholder.typicode.com";
    }
}
