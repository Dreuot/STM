﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STM.Services
{
    public interface ICryptography
    {
        string GetHash(string input);
    }
}
