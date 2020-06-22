﻿using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminOrdersViewModel
    {
        public IPagedResult<OrderDto> PagedOrders { get; set; }
    }
}
