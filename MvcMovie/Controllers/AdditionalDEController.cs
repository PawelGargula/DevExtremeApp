using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;

namespace MvcMovie.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AdditionalDEController : Controller
    {
        private readonly MvcMovieContext _context;

        public AdditionalDEController(MvcMovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var additional = _context.Additional.Select(i => new {
                i.Id,
                i.Detail1,
                i.Detail2,
                i.MovieId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(additional, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id, DataSourceLoadOptions loadOptions)
        {
            var additional = _context.Additional.Where(a => a.MovieId == id).Select(i => new {
                i.Id,
                i.Detail1,
                i.Detail2,
                i.MovieId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(additional, loadOptions));
        }
    }
}
