using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STM.Infrastructure
{
    public static class IdGenerator
    {
        private static STM.Models.Data.STM_DBContext context = new Models.Data.STM_DBContext();

        public static string Next()
        {
            var current = context.CConfig.FirstOrDefault();
            if(current != null)
            {
                return current.ValueC;
            }

            return "0-1";
        }
    }
}
