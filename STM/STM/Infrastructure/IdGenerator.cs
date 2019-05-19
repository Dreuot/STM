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
            var current = context.CConfig.Where(c => c.KeyC == "CurrentId").FirstOrDefault();
            string id = "";
            if(current != null)
            {
                id = "1-" + current.ValueC;

                ulong numeric = Convert.ToUInt64(current.ValueC, 16);
                numeric++;
                current.ValueC = numeric.ToString("X");
                context.SaveChanges();
                
            }
            else
            {
                context.CConfig.Add(new Models.Data.CConfig() { KeyC = "CurrentId", ValueC = "1"});
                context.SaveChanges();
            }

            return id;
        }
    }
}
