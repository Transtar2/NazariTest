using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NazariTest.Application.Models;
using NazariTest.Application.Services;
using NazariTest.Wep.AgGrid;
using NazariTest.Application.Public;
using NazariTest.Application.Enums;
using NazariTest.Application.Extensions;
using System.Linq.Dynamic.Core;


namespace NazariTest.Wep.Controllers
{
    public class FinancialYearController
        (
        IFinancialYearService financialYearService
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResponse> Create(FinancialYearCreateRequest model)
        {
            if (!ModelState.IsValid) 
            {
                return new JsonResponse
                {
                    IsSuccess = false,
                    Message= MessageType.BadData.ToDescription()

                };
            }
            if(model.Id is null)
                return await financialYearService.CreateAsync(model);

            return await financialYearService.UpdateAsync(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetData([FromBody] GridRequest request)
        {
            var query = await financialYearService.GetAllAsync();
            if (!string.IsNullOrEmpty(request.FilterModel?.Title?.Filter))
            {
                query = query.Where(c => c.Title.Contains(request.FilterModel.Title.Filter));
            }

            // Sorting
            if (request.SortModel != null && request.SortModel.Any())
            {
                string dynamicLinqOrder = string.Empty;
                foreach (var sort in request.SortModel)
                {
                    dynamicLinqOrder += sort.ColId + " " + sort.Sort;
                    query = query.AsQueryable().OrderBy(dynamicLinqOrder);
                    //query = sort.Sort == "asc"
                    //    ? query.AsQueryable().OrderBy(dynamicLinqOrder)
                    //    : query.OrderByDescending(c => EF.Property<object>(c, sort.ColId));
                }
                
            }
            else
            {
                query= query.OrderBy(x => x.StartDate);
            }

            // Pagination
            var totalRecords = query.Count();
            var rows = query.Skip(request.startRow).Take(request.endRow - request.startRow).ToList();

            return Ok(new
            {
                rows = rows,
                totalCount = totalRecords
            });
        }

        public async Task<JsonResponse> Delete(Guid id)
        {
            var result = await financialYearService.DeleteAsync(id);

            return result;
        }
    }

}
