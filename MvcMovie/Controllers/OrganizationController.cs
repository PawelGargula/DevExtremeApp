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
    public class OrganizationController : Controller
    {
            private readonly MvcMovieContext _context;

            public OrganizationController(MvcMovieContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
            {
                var organizations = _context.Organization.Select(i => new {
                    i.Id,
                    i.Code,
                    i.Name,
                    i.Shared
                });

                // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
                // This can make SQL execution plans more efficient.
                // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
                // loadOptions.PrimaryKey = new[] { "Id" };
                // loadOptions.PaginateViaPrimaryKey = true;

                return Json(await DataSourceLoader.LoadAsync(organizations, loadOptions));
            }

            [HttpGet]
            public async Task<IActionResult> GetOrganizationBySearchingText(string searchingText)
            {
                var organization = await _context.Organization.FirstOrDefaultAsync(o => o.Code == searchingText);
                if (organization == null)
                {
                    return NotFound();
                }
                else
                {
                    return Json(organization);
                }
            }

        [HttpPut]
            public async Task<IActionResult> Put(int key, string values)
            {
                var model = await _context.Organization.FirstOrDefaultAsync(item => item.Id == key);
                if (model == null)
                    return StatusCode(409, "Object not found");

                var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
                PopulateModel(model, valuesDict);

                if (!TryValidateModel(model))
                    return BadRequest(GetFullErrorMessage(ModelState));

                await _context.SaveChangesAsync();
                return Ok();
            }

            private void PopulateModel(Organization model, IDictionary values)
            {
                string ID = nameof(Organization.Id);
                string CODE = nameof(Organization.Code);
                string NAME = nameof(Organization.Name);
                string SHARED = nameof(Organization.Shared);

                if (values.Contains(ID))
                {
                    model.Id = Convert.ToInt32(values[ID]);
                }

                if (values.Contains(CODE))
                {
                    model.Code = Convert.ToString(values[CODE]);
                }

                if (values.Contains(NAME))
                {
                    model.Name = Convert.ToString(values[NAME]);
                }

                if (values.Contains(SHARED))
                {
                    model.Shared = Convert.ToBoolean(values[SHARED]);
                }
            }

            private string GetFullErrorMessage(ModelStateDictionary modelState)
            {
                var messages = new List<string>();

                foreach (var entry in modelState)
                {
                    foreach (var error in entry.Value.Errors)
                        messages.Add(error.ErrorMessage);
                }

                return String.Join(" ", messages);
            }
    }
}
