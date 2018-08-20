using MicroServiceCategory.API.Models;
using MicroServiceCategory.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;

namespace MicroServiceCategory.API.Controllers.v1
{
    [ApiExplorerSettings(GroupName = "category-v1")]
    [Route("v1/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// List all categories
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Categories successfully returned", typeof(List<Category>))]
        public IActionResult List()
        {
            return Ok(_categoryService.List());
        }

        /// <summary>
        /// Get single category by identifier
        /// </summary>
        /// <param name="id">Category identifier</param>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid identifier")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category not found")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category successfully returned")]
        public IActionResult Get(long id)
        {
            if (id <= 0)
                return BadRequest();

            return Ok(_categoryService.Get(id));
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="name">Category name</param>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "The category was created", typeof(long))]
        public IActionResult Post([FromBody]string name)
        {
            var id = _categoryService.Post(name);

            return StatusCode((int)HttpStatusCode.Created, id);
        }


        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <param name="name">Category name</param>
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid identifier")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Category not found")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "The category was updated")]
        public IActionResult Put(long id, [FromBody]string name)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                _categoryService.Put(id, name);
            }
            catch(ArgumentOutOfRangeException)
            {
                return NotFound();
            }

            return NoContent();
        }
        

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id">Category identifier</param>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid identifier")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "The category was deleted")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
                return BadRequest();

            _categoryService.Delete(id);

            return NoContent();
        }
    }
}
