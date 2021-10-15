using System;
using System.Collections.Generic;
using AspNetCoreTweetbookApi.Domain;

namespace AspNetCoreTweetbookApi.Services
{
    public interface IPostsService
    {
        List<Post> GetPosts();
        Post GetPostById(Guid id);
        bool UpdatePost(Post post);
    }
}