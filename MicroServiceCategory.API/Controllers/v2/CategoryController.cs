using MicroServiceCategory.API.Models;
using MicroServiceCategory.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;

namespace MicroServiceCategory.API.Controllers.v2
{
    [ApiExplorerSettings(GroupName = "category-v2")]
    [Route("v2/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Categories successfully returned", typeof(List<Category>))]
        public IActionResult List(string filter)
        {
            return Ok(_categoryService.List(filter));
        }
    }
}
