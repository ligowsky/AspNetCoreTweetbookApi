using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreTweetbookApi.Contracts.V1;
using AspNetCoreTweetbookApi.Contracts.V1.Requests;
using AspNetCoreTweetbookApi.Contracts.V1.Responses;
using AspNetCoreTweetbookApi.Domain;
using AspNetCoreTweetbookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTweetbookApi.Controllers.V1
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
        public IActionResult Create([FromBody] CreatePostRequest request)
        {
            var post = new Post { Id = Guid.NewGuid(), Name = request.Name };

            _postsService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{id}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };

            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = id,
                Name = request.Name
            };

            var updated = _postsService.UpdatePost(post);

            if (!updated)
                return NotFound();

            return Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var deleted = _postsService.DeletePost(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}