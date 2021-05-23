using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TableDefinitionBrowser.DataAccess;
using TableDefinitionBrowser.DataAccess.Data;
using TableDefinitionBrowser.DataAccess.Data.Repository;
using TableDefinitionBrowser.Models;

namespace TableDefinitionBrowser.Controllers
{

    [Area("TableDefinition")] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.TableDefinition.GetAll());
        }

        public IActionResult Upsert(string id)
        {
            if (string.IsNullOrEmpty(id)) return View(new TableDefinition());

            var td = _unitOfWork.TableDefinition.Get(id);
            if (td == null) return NotFound();

            return View(td);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TableDefinition td)
        {
            if (ModelState.IsValid)
            {
                td.UpdatedAt = DateTime.Now;
                td.UpdatedBy = "001";

                var table = _unitOfWork.TableDefinition.Get(td.PhysicalTableName);
                if (table == null)
                {
                    td.CreatedAt = DateTime.Now;
                    _unitOfWork.TableDefinition.Add(td);
                }
                else
                {
                    _unitOfWork.TableDefinition.Update(td);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(td);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
