using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private ISender _mediator;

        public ISender Mediator 
			=> _mediator ??= HttpContext.RequestServices.GetService<ISender>();   
    }
}
