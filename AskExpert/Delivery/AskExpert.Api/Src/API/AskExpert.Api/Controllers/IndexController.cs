

using AskExpert.Application.Features.IndexFeature.Query.Get;

namespace AskExperts.API.Controllers
{
    public class IndexController : BaseController
    {
        private readonly IMediator _mediator;

        public IndexController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        public async Task<ResponseDTO> Get()
        {
            return await _mediator.Send(new IndexQuery());
        }

    }
}
