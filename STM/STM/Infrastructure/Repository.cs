using STM.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STM.Infrastructure
{
    public class Repository
    {
        private STM_DBContext db;

        public Repository(STM_DBContext db)
        {
            this.db = db;
        }


    }
}
