using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postsService.GetPostsAsync();

            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await _postsService.GetPostByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = new Post { Name = request.Name };

            await _postsService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{id}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };

            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = id,
                Name = request.Name
            };

            var updated = await _postsService.UpdatePostAsync(post);

            if (!updated)
                return NotFound();

            return Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _postsService.DeletePostAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}