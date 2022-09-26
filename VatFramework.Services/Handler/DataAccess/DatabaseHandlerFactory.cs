using Microsoft.EntityFrameworkCore;
using System.Configuration;
using VatFramework.Models;
using VatFramework.Services.Contract.DataAccess;

namespace VatFramework.Services.Handler.DataAccess
{
    public class DatabaseHandlerFactory
    {
      
          private readonly APPContext _context;
        public DatabaseHandlerFactory(APPContext context)
        {
            _context = context;
           // connectionStringSettings = context.Database.GetDbConnection().ConnectionString();
        }

        public IDataAccess CreateDatabase()
        {
            IDataAccess database = null;

            switch (_context.Database.ProviderName.ToLower())
            {
                case "microsoft.entityframeworkcore.sqlserver":
                    database = new SqlDataAccess(_context.Database.GetDbConnection().ConnectionString);
                        break;
                case "system.data.oracleclient":
                        database = new OracleDataAccess(_context.Database.GetDbConnection().ConnectionString);
                        break;
               
            }

            return database;
        }

        public string GetProviderName()
        {
            return _context.Database.ProviderName;
        }
    }
}
