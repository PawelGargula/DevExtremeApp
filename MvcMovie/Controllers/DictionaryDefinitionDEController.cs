//using DevExtreme.AspNet.Data;
//using DevExtreme.AspNet.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using MvcMovie.Data;
//using MvcMovie.Models;
//using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DictionaryDefinitionDEController : Controller
    {
        private readonly MvcMovieContext _context;

        public DictionaryDefinitionDEController(MvcMovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var dictionaryDefinition = _context.DictionaryDefinition.Select(i => new {
                i.Id,
                i.Code,
                i.Name,
                i.IsSystemic
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(dictionaryDefinition, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post([Bind("Code, Name")] DictionaryDefinition dictionaryDefinition)
        {
            dictionaryDefinition.IsSystemic = false;
            if (ModelState.IsValid)
            {
                _context.Add(dictionaryDefinition);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPost]
        //public IActionResult Post([Bind("Code, Name")] DictionaryDefinition dictionaryDefinition)
        //{
        //    return Ok();
        //}
    }
}
