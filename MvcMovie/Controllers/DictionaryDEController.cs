using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DictionaryDEController : Controller
    {
        private readonly MvcMovieContext _context;

        public DictionaryDEController(MvcMovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var dictionary = _context.Dictionary.Select(i => new {
                i.Id,
                i.Code,
                i.Name,
                i.ParentId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(dictionary, loadOptions));
        }
    }
}
