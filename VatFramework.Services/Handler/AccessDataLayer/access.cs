using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VatFramework.Models;
using VatFramework.Services.Handler.DataAccess;

namespace VatFramework.Services.Handler.AccessDataLayer
{
    public  class access
    {
        private readonly APPContext _context;
        public access(APPContext context)
        {
            _context = context;
        }

        
        public void DeletePermissionByRoleID(IDbDataParameter[]  parameters, string StoreProcedureName)
        {
            DBManager dBManager = new DBManager(_context);
           dBManager.Delete(StoreProcedureName, CommandType.StoredProcedure, parameters);
        }


        public DataTable FetchRolePermissionsByRoleId(IDbDataParameter[] parameters, string StoreProcedureName)
        {
            DBManager dBManager = new DBManager(_context);
           return  dBManager.GetDataTable(StoreProcedureName, CommandType.StoredProcedure, parameters);
        }


        public void UpdateTables(IDbDataParameter[] parameters, string StoreProcedureName)
        {
            DBManager dBManager = new DBManager(_context);
             dBManager.Update(StoreProcedureName, CommandType.StoredProcedure, parameters);
        }

    }
}
