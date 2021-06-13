using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TableDefinitionBrowser.DataAccess.Data;
using TableDefinitionBrowser.DataAccess.Data.Repository;
using TableDefinitionBrowser.Models;

namespace TableDefinitionBrowser.DataAccess
{
    public class ColumnDefinitionRepository : Repository<ColumnDefinition>
    {
        private readonly ApplicationDbContext _db;

        public ColumnDefinitionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ColumnDefinition> GetByPhysicalTableName(string id)
        {
            var columnDefinitions = _db.ColumnDefinition.Where(cd => cd.TableName == id);
            return columnDefinitions;
        }

        public IEnumerable<SelectListItem> GetColumnDefinitionListForDropDown()
        {
            var columnDefinition = _db.ColumnDefinition.Select(i => new SelectListItem()
            {
                Text = i.LogicalColumnName,
                Value = string.Format("{0}.{1}", i.TableName, i.PhysicalColumnName)
            });
            return columnDefinition;
        }

        public void Update(ColumnDefinition columnDefinition)
        {
            var objFromDb = _db.ColumnDefinition.FirstOrDefault(cd =>
                (cd.TableName == columnDefinition.TableName)
                && (cd.PhysicalColumnName == columnDefinition.PhysicalColumnName));

            objFromDb.TableName = columnDefinition.TableName;
            objFromDb.PhysicalColumnName = columnDefinition.PhysicalColumnName;
            objFromDb.LogicalColumnName = columnDefinition.LogicalColumnName;
            objFromDb.DataType = columnDefinition.DataType;
            objFromDb.Size = columnDefinition.Size;
            objFromDb.IsNotNull = columnDefinition.IsNotNull;
            objFromDb.Default = columnDefinition.Default ?? "";
            objFromDb.Remarks = columnDefinition.Remarks;
            objFromDb.UpdatedBy = columnDefinition.UpdatedBy;
            objFromDb.UpdatedAt = columnDefinition.UpdatedAt;

            _db.SaveChanges();
        }
    }
}
