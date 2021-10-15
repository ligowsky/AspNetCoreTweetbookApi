using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTweetbookApi.Contracts.V1;
using DotNetTweetbookApi.Contracts.V1.Requests;
using DotNetTweetbookApi.Contracts.V1.Responses;
using DotNetTweetbookApi.Domain;
using DotNetTweetbookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTweetbookApi.Controllers.V1
{
    public class PostsController : Controller
    {
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        private readonly IPostsService _postsService;

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            var posts = _postsService.GetPosts();

            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid id)
        {
            var post = _postsService.GetPostById(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id };

            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();

            _postsService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{id}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };

            return Created(locationUrl, response);
        }
    }
}