namespace AskExperts.API.Controllers
{
    public class SpecializationController : BaseController
    {
        private readonly IMediator _mediator;

        public SpecializationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PostSpecialization")]
        public async Task<ResponseDTO> PostSpecialization(PostSpecializationCommand command) => await _mediator.Send(command);

        [HttpGet]
        [Route("GetAllSpecializations")]
        public async Task<ResponseDTO> GetAllSpecializations()
        {
            var query = new GetAllSpecializationsQuery();
            return await _mediator.Send(query);
        }

        [HttpPut]
        [Route("PutSpecialization/{id}")]
        public async Task<ResponseDTO> PutSpecialization([FromRoute] Guid id, PutSpecializationCommand command)
        {
            command.SpecializationId = id;
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("DeleteSpecialization/{id}")]
        public async Task<ResponseDTO> DeleteSpecialization(Guid id) => await _mediator.Send(new DeleteSpecializationCommand(id));

    }
}
