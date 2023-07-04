using Microsoft.VisualBasic;
using Newtonsoft.Json;
using PostViewer.Api.Data.Models.Comment;
using PostViewer.Api.Services.Helpers;
using PostViewer.Api.Services.ModelsApi;
using static PostViewer.Api.Common.Constants;

namespace PostViewer.Api.Services.CommentsService
{
    public class CommentsService : ICommentsService
    {
        public async Task<bool> AddComment(AddCommentRequest request)
        {
            var dbComments = await GetCommentsFromData();

            //In real scenario ORM deals with the indexation by itself
            var indexOfNewComment = dbComments.Max(x => x.Id) + 1;

            var addComment = new Comment
            {
                Id = indexOfNewComment,
                Body = request.Body,
                Email = request.Email,
                Name = request.Name,
                PostId = request.PostId
            };

            dbComments.Add(addComment);
            try
            {
                SaveChanges(dbComments);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var dbComments = await GetCommentsFromData();

            var comment = dbComments.FirstOrDefault(x => x.Id == commentId);

            if (comment != null)
            {
                dbComments.Remove(comment);
            }

            try
            {
                SaveChanges(dbComments);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CommentApi>> GetPostComments(int postId)
        {
            var dbComments = await GetCommentsFromData();

            var result = dbComments
                .Where(x => x.PostId == postId)
                .Select(x => new CommentApi
                {
                    Id= x.Id,
                    Body= x.Body,
                    Email= x.Email,
                    Name = x.Name   
                })
                .ToList();

            return result;
        }

        public async Task<Dictionary<int, int>> GetPostsComments(List<int> postsIds)
        {
            var dbComments = await GetCommentsFromData();

            var result = dbComments
                .Where(x => postsIds.Contains(x.PostId))
                .GroupBy(x => x.PostId)
                .ToDictionary(x => x.Key, y => y.Count());

            return result;
        }

        private static void SaveChanges(List<Comment> dbComments)
        {
            var json = JsonConvert.SerializeObject(dbComments, JsonHelper.GetJsonSettings());
            File.WriteAllText(Comments.COMMENTS_JSON_PATH, json);
        }

        private async Task<List<Comment>> GetCommentsFromData()
        {
            var json = Comments.COMMENTS_JSON_PATH;

            var jsonInput = File.ReadAllText(json);

            var comments = JsonConvert.DeserializeObject<List<Comment>>(jsonInput);

            return comments ?? new List<Comment>();
        }
    }
}
