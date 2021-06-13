using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TableDefinitionBrowser.DataAccess.Data.Repository;
using TableDefinitionBrowser.Models;

namespace TableDefinitionBrowser.Controllers
{
	[Area("TableDefinition")]
	public class ColumnDefinitionController : Controller
	{
		private readonly ILogger<ColumnDefinitionController> _logger;
		private readonly UnitOfWork _unitOfWork;

		public ColumnDefinitionController(ILogger<ColumnDefinitionController> logger, UnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index(string tableName)
		{
			TempData["TableName"] = tableName;
			return View(_unitOfWork.ColumnDefinition.GetByPhysicalTableName(tableName));
		}

		public IActionResult Upsert(string tableName, string physicalColumnName)
		{
			if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(physicalColumnName))
			{
				var columnDefinition = new ColumnDefinition
				{
					TableName = tableName,
				};
				return View(columnDefinition);
			}

			var cd = _unitOfWork.ColumnDefinition.Get(new[] { tableName, physicalColumnName });
			if (cd == null) return NotFound();

			return View(cd);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ColumnDefinition cd)
		{
			if (ModelState.IsValid)
			{
				cd.UpdatedAt = DateTime.Now;
				cd.UpdatedBy = "001";

				var table = _unitOfWork.ColumnDefinition.Get(new[] { cd.TableName, cd.PhysicalColumnName });
				if (table == null)
				{
					cd.CreatedBy = "001";
					cd.CreatedAt = DateTime.Now;
					_unitOfWork.ColumnDefinition.Add(cd);
				}
				else
				{
					_unitOfWork.ColumnDefinition.Update(cd);
				}
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index), nameof(ColumnDefinition), new { tableName = cd.TableName });
			}
			return RedirectToAction(nameof(Index), nameof(ColumnDefinition), new { tableName = cd.TableName });
		}

		public string TableName { get; set; }
	}
}
