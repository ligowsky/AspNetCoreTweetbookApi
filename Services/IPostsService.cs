using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTweetbookApi.Domain;

namespace AspNetCoreTweetbookApi.Services
{
    public interface IPostsService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(Guid id);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(Guid id);
    }
}