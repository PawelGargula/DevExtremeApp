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
    public class MoviesDEController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesDEController(MvcMovieContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var movie = _context.Movie
                .Join(
                    _context.Dictionary,
                    m => new { Id1 = m.Id, Id2 = m.Id, Id3 = m.Id },
                    d => new { Id1 = d.Id, Id2 = d.Id, Id3 = d.Id },
                    (m, d) => m
                )
                .Select(i => new {
                    i.Id,
                    i.Title,
                    i.ReleaseDate,
                    i.Genre,
                    i.Price,
                    i.Rating
                });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(movie, loadOptions));
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(string values) {
        //    var model = new Movie();
        //    var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
        //    PopulateModel(model, valuesDict);

        //    if(!TryValidateModel(model))
        //        return BadRequest(GetFullErrorMessage(ModelState));

        //    var result = _context.Movie.Add(model);
        //    await _context.SaveChangesAsync();

        //    return Json(new { result.Entity.Id });
        //}

        //[HttpPut]
        //public async Task<IActionResult> Put(int key, string values) {
        //    var model = await _context.Movie.FirstOrDefaultAsync(item => item.Id == key);
        //    if(model == null)
        //        return StatusCode(409, "Object not found");

        //    var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
        //    PopulateModel(model, valuesDict);

        //    if(!TryValidateModel(model))
        //        return BadRequest(GetFullErrorMessage(ModelState));

        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}

        //[HttpDelete]
        //public async Task Delete(int key) {
        //    var model = await _context.Movie.FirstOrDefaultAsync(item => item.Id == key);

        //    _context.Movie.Remove(model);
        //    await _context.SaveChangesAsync();
        //}


        private void PopulateModel(Movie model, IDictionary values) {
            string ID = nameof(Movie.Id);
            string TITLE = nameof(Movie.Title);
            string RELEASE_DATE = nameof(Movie.ReleaseDate);
            string GENRE = nameof(Movie.Genre);
            string PRICE = nameof(Movie.Price);
            string RATING = nameof(Movie.Rating);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(TITLE)) {
                model.Title = Convert.ToString(values[TITLE]);
            }

            if(values.Contains(RELEASE_DATE)) {
                model.ReleaseDate = Convert.ToDateTime(values[RELEASE_DATE]);
            }

            if(values.Contains(GENRE)) {
                model.Genre = Convert.ToString(values[GENRE]);
            }

            if(values.Contains(PRICE)) {
                model.Price = Convert.ToDecimal(values[PRICE], CultureInfo.InvariantCulture);
            }

            if(values.Contains(RATING)) {
                model.Rating = Convert.ToString(values[RATING]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}