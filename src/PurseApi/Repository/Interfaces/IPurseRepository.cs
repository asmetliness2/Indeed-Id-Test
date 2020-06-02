using PurseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Repository.Interfaces
{
    public interface IPurseRepository
    {
        Task<Purse> GetOrCreatePurse(int userId);
        Task Commit();

    }
}
