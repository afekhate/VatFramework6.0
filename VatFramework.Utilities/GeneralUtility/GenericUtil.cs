using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Utilities.GeneralUtility
{
  public  class GenericUtil
    {
		

        public static string generatePrimaryId()
        {
			try
			{
				Guid guid = Guid.NewGuid();
				return guid.ToString("N");
			}
			catch (Exception ex)
			{
				return null;
				throw;
			}
        }
    }
}
