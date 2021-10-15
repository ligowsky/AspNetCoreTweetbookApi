using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreTweetbookApi.Domain;

namespace AspNetCoreTweetbookApi.Services
{
    public class PostsService : IPostsService
    {
        public PostsService()
        {
            _posts = new List<Post>();
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

        public bool UpdatePost(Post postToUpdate)
        {
            var exist = GetPostById(postToUpdate.Id) != null;

            if (!exist)
                return false;

            var index = _posts.FindIndex(x => x.Id == postToUpdate.Id);
            _posts[index] = postToUpdate;

            return true;
        }
    }
}