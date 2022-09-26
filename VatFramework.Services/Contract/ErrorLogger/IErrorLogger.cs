using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Services.Contract.ErrorLogger
{
   public interface IErrorLogger
    {
        void LogError(Exception ex, string contollerOrMethodName);
    }
}
