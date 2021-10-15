using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTweetbookApi.Domain;

namespace DotNetTweetbookApi.Services
{
    public class PostsService : IPostsService
    {
        public PostsService()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid() });
            }
        }

        private readonly List<Post> _posts;

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public Post GetPostById(Guid id)
        {
            return _posts.SingleOrDefault(x => x.Id == id);
        }
    }
}