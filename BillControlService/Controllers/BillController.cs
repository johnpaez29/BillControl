using BillBusiness.Handlers;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillControlService.Controllers
{
    [ApiController]
    [Route("Bill")]
    public class BillController : Controller
    {
        private readonly IServiceData<BillControl> _serviceData;
        private readonly IBillHandler _billHandler;

        public BillController(IServiceData<BillControl> serviceData, IBillHandler billHandler)
        {
            _serviceData = serviceData;
            _billHandler = billHandler;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillControl>> GetBill(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bill = await _serviceData.GetById(id);
                    return bill;
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillControl>>> GetBills()
        {
            try
            {
                var bills = await _billHandler.GetBills();
                return bills.ToList();
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult InsertBill(BillControl bill)
        {
            try
            {
                if (TryValidateModel(bill))
                {
                    _billHandler.InsertBill(bill);
                    return Ok(new { message = "Bill succesfully inserted" });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPut]
        public IActionResult UpdateBill(BillControl bill)
        {
            try
            {
                if (TryValidateModel(bill) && !string.IsNullOrWhiteSpace(bill.Id))
                {
                    _serviceData.Update(bill);
                    return Ok(new { message = "Bill succesfully updated" });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBill(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _serviceData.Delete(id);
                    return Ok(new { message = "Bill succesfully deleted" });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }
    }
}
