using Microsoft.AspNetCore.Mvc;
using AgGrid.InfiniteRowModel;
using AgGrid.InfiniteRowModel.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NazariTest.Application.Models;
using NazariTest.Application.Services;
using NazariTest.Wep.AgGrid;
using NazariTest.Application.Public;
using NazariTest.Application.Enums;
using NazariTest.Application.Extensions;


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

            return await financialYearService.CreateAsync(model);

        }
        [HttpPost]
        public async Task<IActionResult> GetData([FromBody] GridRequest request)
        {
            var query = await financialYearService.GetAllAsync();
            if (!string.IsNullOrEmpty(request.FilterModel?.Make))
            {
                query = query.Where(c => c.Title.Contains(request.FilterModel.Make));
            }

            // Sorting
            if (request.SortModel != null && request.SortModel.Any())
            {
                foreach (var sort in request.SortModel)
                {
                    query = sort.Sort == "asc"
                        ? query.OrderBy(c => EF.Property<object>(c, sort.ColId))
                        : query.OrderByDescending(c => EF.Property<object>(c, sort.ColId));
                }
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
    }


   
}
