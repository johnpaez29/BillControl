using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillControlService.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IServiceData<User> _data;
        private readonly ILogData<Log> _log;

        public UserController(
            IServiceData<User> serviceData,
            ILogData<Log> log)
        {
            _data = serviceData;
            _log = log;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                await _log.InsertAsync(new LogBill
                {
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se solicitan todos los usuarios"
                });

                IEnumerable<User> result = await _data.GetAllAsync();

                if (result?.Any() ?? false)
                {
                    await _log.InsertAsync(new LogBill
                    {
                        Change = "N/A",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "Se obtienen los usuarios de manera exitosa."
                    });
                }
                else
                {
                    await _log.InsertAsync(new LogBill
                    {
                        Change = "N/A",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "No se obtienen resultados en la consulta de usuarios."
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Error al obtener los usuarios: {e.Message}."
                });
                return NotFound(new { data = e.StackTrace, error = e.Message });
            }
        }
    }
}
