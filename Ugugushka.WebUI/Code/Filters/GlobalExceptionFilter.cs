﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ugugushka.Domain.Code.Exceptions;

namespace Ugugushka.WebUI.Code.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException notFoundEx:
                    context.Result = new NotFoundResult();
                    break;
            }
        }
    }
}
