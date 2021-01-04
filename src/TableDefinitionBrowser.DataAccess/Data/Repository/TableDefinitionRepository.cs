using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TableDefinitionBrowser.DataAccess.Data;
using TableDefinitionBrowser.DataAccess.Data.Repository;
using TableDefinitionBrowser.Models;

namespace TableDefinitionBrowser.DataAccess
{
    public class TableDefinitionRepository : Repository<TableDefinition>
    {
        private readonly ApplicationDbContext _db;

        public TableDefinitionRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetTableDefinitionListForDropDown()
        {
            var tableDefinitions = _db.TableDefinition.Select(i => new SelectListItem()
            {
                Text = i.LogicalTableName,
                Value = i.PhysicalTableName
            });
            return tableDefinitions;
        }

        public void Update(TableDefinition tableDefinition)
        {
            var objFromDb = _db.TableDefinition.FirstOrDefault(td => td.PhysicalTableName == tableDefinition.PhysicalTableName);

            objFromDb.PhysicalTableName = tableDefinition.PhysicalTableName;
            objFromDb.LogicalTableName = tableDefinition.LogicalTableName;
            objFromDb.Category = tableDefinition.Category;
            objFromDb.Owner = tableDefinition.Owner;
            objFromDb.Remarks = tableDefinition.Remarks;
            objFromDb.CreatedBy = tableDefinition.CreatedBy;
            objFromDb.CreatedAt = tableDefinition.CreatedAt;
            objFromDb.UpdatedBy = tableDefinition.UpdatedBy;
            objFromDb.UpdatedAt = tableDefinition.UpdatedAt;

            _db.SaveChanges();
        }
    }
}
