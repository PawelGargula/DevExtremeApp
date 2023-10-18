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
    [Route("[controller]/[action]")]
    public class ToolsHistoriesController : Controller
    {
        private MvcMovieContext _context;

        public ToolsHistoriesController(MvcMovieContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var toolshistory = _context.ToolsHistory.Select(i => new {
                i.Id,
                i.ObjectId,
                i.RentDate,
                i.ReturnDate
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(toolshistory, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ToolsHistory();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ToolsHistory.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.ToolsHistory.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.ToolsHistory.FirstOrDefaultAsync(item => item.Id == key);

            _context.ToolsHistory.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ToolsHistory model, IDictionary values) {
            string ID = nameof(ToolsHistory.Id);
            string OBJECT_ID = nameof(ToolsHistory.ObjectId);
            string RENT_DATE = nameof(ToolsHistory.RentDate);
            string RETURN_DATE = nameof(ToolsHistory.ReturnDate);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(OBJECT_ID)) {
                model.ObjectId = Convert.ToInt32(values[OBJECT_ID]);
            }

            if(values.Contains(RENT_DATE)) {
                model.RentDate = Convert.ToDateTime(values[RENT_DATE]);
            }

            if(values.Contains(RETURN_DATE)) {
                model.ReturnDate = Convert.ToDateTime(values[RETURN_DATE]);
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