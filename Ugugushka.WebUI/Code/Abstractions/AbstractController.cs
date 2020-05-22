using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ugugushka.WebUI.Code.Abstractions
{
    public class AbstractController : Controller
    {
        protected readonly IMapper Mapper;
        public AbstractController(IMapper mapper) => Mapper = mapper;
    }
}
