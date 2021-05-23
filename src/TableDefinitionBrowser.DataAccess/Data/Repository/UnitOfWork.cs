using System;
using System.Collections.Generic;
using System.Text;
using TableDefinitionBrowser.DataAccess.Data.Repository.IRepository;

namespace TableDefinitionBrowser.DataAccess.Data.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            TableDefinition = new TableDefinitionRepository(_db);
        }

        public TableDefinitionRepository TableDefinition { get; private set; }
        
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}