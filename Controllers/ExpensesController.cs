using CostApp.Data;
using CostApp.Domain;
using CostApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpensesController : ControllerBase
    {
        public readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost(ApiRoutes.Expense.Create)]
        public async Task<IActionResult> Create([FromBody] List<Expense> createExpenseListRequest)
        {
            foreach(var currentExpense in createExpenseListRequest)
            {
                //currentExpense.Date = DateTime.Now;
                await _expenseService.CreateExpenseAsync(currentExpense);
            }

            return Ok();
        }

        [HttpGet(ApiRoutes.Expense.GetAllWithDatesAndCategories)]
        public async Task<IActionResult> Get([FromQuery] DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            //if (categoryId == 0)
                //return BadRequest();

            List<Expense> reportList = await _expenseService.GetExpensesByDateAndCategoryAsync(dateFrom, dateTo, categoryId);

            foreach(var item in reportList)
            {
                item.Total = Math.Round(item.Price * item.Quantity,2);
            }

            return Ok(reportList);
        }
    }
}
