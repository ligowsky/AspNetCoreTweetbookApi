using System;
using System.Collections.Generic;
using DotNetTweetbookApi.Domain;

namespace DotNetTweetbookApi.Services
{
    public interface IPostsService
    {
        List<Post> GetPosts();
        Post GetPostById(Guid id);
    }
}