using System;
using System.Collections.Generic;
using DotNetTweetbookApi.Contracts.V1;
using DotNetTweetbookApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTweetbookApi.Controllers.V1
{
    public class PostsController : Controller
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }
    }
}