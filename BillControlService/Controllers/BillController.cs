using BillBusiness.Interfaces;
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
    [Route("[controller]/[action]")]
    public class BillController : Controller
    {
        private readonly IServiceData<BillData> _serviceData;
        private readonly IBillHandler _billHandler;
        private readonly ILog<LogBill> _log;

        public BillController(IServiceData<BillData> serviceData,
            IBillHandler billHandler,
            ILog<LogBill> log)
        {
            _serviceData = serviceData;
            _billHandler = billHandler;
            _log = log;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public async Task<ActionResult<BillData>> GetBill(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = id,
                        IdUser = string.Empty,
                        Change = "N/A",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "Se solicita obtener factura de manera exitosa."
                    });
                    var bill = await _serviceData.GetById(id);
                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = id,
                        IdUser = bill.IdUser,
                        Change = "N/A",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "Se obtiene factura de manera exitosa."
                    });
                    return bill;
                }
                else
                {
                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = id,
                        IdUser = string.Empty,
                        Change = "N/A",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "Error en los datos enviados para obtener factura."
                    });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = id,
                    IdUser = string.Empty,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Error al obtener factura."
                });
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillData>>> GetBills()
        {
            try
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = string.Empty,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se solicita obtener facturas."
                });
                var bills = await _billHandler.GetBills();

                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = string.Empty,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se obtienen facturas de manera exitosa."
                });
                return bills.ToList();
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = string.Empty,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Error al obtener facturas: {e.Message}."
                });
                return NotFound(new { message = e.Message });
            }
        }


        [HttpGet]
        [Route("{id?}")]
        public async Task<ActionResult<IEnumerable<BillData>>> GetBillsByIdUser(string id)
        {
            try
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = id,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se solicita obtener facturas."
                });

                var bills = await _billHandler.GetBillsByIdUser(id);

                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = string.Empty,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se obtienen facturas de manera exitosa."
                });
                return bills.ToList();
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = id,
                    Change = "N/A",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Error al obtener facturas: {e.Message}."
                });
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertBill(BillControl bill)
        {
            try
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = bill.IdUser,
                    Change = "Nueva Factura",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se solicita insertar factura."
                });
                if (TryValidateModel(bill))
                {
                    string response = await _billHandler.InsertBill(bill);
                    if (string.IsNullOrEmpty(response))
                    {
                        await _log.InsertAsync(new LogBill
                        {
                            IdBill = response,
                            IdUser = bill.IdUser,
                            Change = "Nueva Factura",
                            Date = DateTime.UtcNow.AddHours(-5),
                            Description = "Se inserta factura de manera exitosa."
                        });
                    }
                    else
                    {
                        await _log.InsertAsync(new LogBill
                        {
                            IdBill = response,
                            IdUser = bill.IdUser,
                            Change = "Nueva Factura",
                            Date = DateTime.UtcNow.AddHours(-5),
                            Description = "Error al obtener factura a insertar."
                        });
                    }
                    return Ok(new { message = "Bill succesfully inserted" });
                }
                else
                {
                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = string.Empty,
                        IdUser = bill.IdUser,
                        Change = "Nueva Factura",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "Error en los datos suministrados para insertar factura."
                    });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = bill.IdUser,
                    Change = "Nueva Factura",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Error al insertar factura : {e.Message}."
                });
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBill(UpdateBill bill)
        {
            try
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = bill.Bill?.IdUser,
                    Change = "Actualización Factura",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = "Se solicita actualizar factura."
                });

                if (TryValidateModel(bill) && !string.IsNullOrWhiteSpace(bill.Bill?.Id))
                {
                    await _billHandler.UpdateBill(bill);
                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = string.Empty,
                        IdUser = bill.Bill?.IdUser,
                        Change = "Actualización Factura",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = "Se actualiza factura exitosamente."
                    });
                    return Ok(new { message = "Bill succesfully updated" });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = string.Empty,
                    IdUser = bill.Bill?.IdUser,
                    Change = "Actualización Factura",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Error al actualizar factura: {e.Message}."
                });
                return NotFound(new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(string id)
        {
            try
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = id,
                    IdUser = string.Empty,
                    Change = "Eliminar Factura",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Se solicita eliminar factura."
                });
                if (ModelState.IsValid)
                {
                    await _billHandler.DeleteBill(id);

                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = id,
                        IdUser = string.Empty,
                        Change = "Eliminar Factura",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = $"Se elimina factura exitosamente."
                    });
                    return Ok(new { message = "Bill succesfully deleted" });
                }
                else
                {
                    await _log.InsertAsync(new LogBill
                    {
                        IdBill = id,
                        IdUser = string.Empty,
                        Change = "Eliminar Factura",
                        Date = DateTime.UtcNow.AddHours(-5),
                        Description = $"Error en los datos enviados para eliminar factura."
                    });
                }

                return BadRequest(new { message = "validate request error" });
            }
            catch (Exception e)
            {
                await _log.InsertAsync(new LogBill
                {
                    IdBill = id,
                    IdUser = string.Empty,
                    Change = "Eliminar Factura",
                    Date = DateTime.UtcNow.AddHours(-5),
                    Description = $"Error al eliminar factura: {e.Message}."
                });
                return NotFound(new { message = e.Message });
            }
        }
    }
}
