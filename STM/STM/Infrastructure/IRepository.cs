using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STM.Infrastructure
{
    interface IRepository
    {
        bool CreateTask();
        bool CreateUser();
        bool CreateActivity();
        bool CreateBoard();
        bool CreateList();
        bool Config();
        bool ActivityType();
    }
}
