﻿using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Repository.Interfaces
{
    interface IPurseRepository
    {
        Task<Purse> GetOrCreatePurse(int userId);
        void Commit();

    }
}