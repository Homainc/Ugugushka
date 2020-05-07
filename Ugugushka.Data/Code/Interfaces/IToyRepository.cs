﻿using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Models;

namespace Ugugushka.Data.Code.Interfaces
{
    interface IToyRepository
    {
        Task<IPagedResult<Toy>> GetFilteredPagedAsync(IToyFilterInfo filter, IPageInfo pageInfo);
    }
}
