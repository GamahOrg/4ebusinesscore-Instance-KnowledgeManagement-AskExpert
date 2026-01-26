namespace AskExperts.API.Controllers
{
    public class ExpertController : BaseController
    {
        private readonly IMediator _mediator;

        public ExpertController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PostExpert")]
        public async Task<ResponseDTO> PostExpert([FromForm] PostExpertCommand command) => await _mediator.Send(command);

        [HttpGet]
        [Route("GetAllExperts")]
        public async Task<ResponseDTO> GetAllExperts()
        {
            var query = new GetAllExpertsQuery();
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Route("GetAllExpertsPaginated")]
        public async Task<ResponseDTO> GetAllExpertsPaginated(int pageNumber, int pageSize, string? searchTerm)
        {
            var query = new GetAllExpertsPaginatedQuery(pageNumber, pageSize, searchTerm);
            return await _mediator.Send(query);
        }

        [HttpGet, Route("GetExpertById/{expertId}")]
        public async Task<ResponseDTO> GetExpertById(Guid expertId) => await _mediator.Send(new GetExpertByIdQuery(expertId));


        [HttpPut("PutExpert/{expertId}")]
        public Task<ResponseDTO> PutExpert([FromForm] PutExpertDto putExpertDto, Guid expertId)
        {
            return _mediator.Send(new PutExpertCommand { ExpertId = expertId, ExpertDto = putExpertDto });
        }

        [HttpPatch("PatchExpert/{expertId}")]
        public Task<ResponseDTO> PatchExpert([FromForm] PatchExpertDto patchExpertDto, Guid expertId)
        {
            return _mediator.Send(new PatchExpertCommand { ExpertId = expertId, ExpertDto = patchExpertDto });
        }

        [HttpDelete]
        [Route("DeleteExpert/{expertId}")]
        public async Task<ResponseDTO> DeleteExpert(Guid expertId) => await _mediator.Send(new DeleteExpertCommand(expertId));

        [HttpGet, Route("GetExpertsBySpecializationId/{specializationId}")]
        public async Task<ResponseDTO> GetExpertsBySpecializationId(Guid specializationId) => await _mediator.Send(new GetExpertsBySpecializationIdQuery(specializationId));

        #region Experience
        [HttpPost]
        [Route("PostExperience")]
        public async Task<ResponseDTO> PostExperience(PostExperienceCommand command) => await _mediator.Send(command);

        [HttpGet]
        [Route("GetAllExperiencesPaginated")]
        public async Task<ResponseDTO> GetAllExperiencesPaginated(int pageNumber, int pageSize)
        {
            var query = new GetAllExperiencesPaginatedQuery(pageNumber, pageSize);
            return await _mediator.Send(query);
        }

        [HttpGet, Route("GetExperienceByExpertId/{expertId}")]
        public async Task<ResponseDTO> GetExperienceByExpertId(Guid expertId) => await _mediator.Send(new GetExperiencesByExpertIdQuery(expertId));

        [HttpPut("PutExperience/{experienceId}")]
        public async Task<ResponseDTO> PutExperience(PutExperienceCommand putExperienceCommand, Guid experienceId)
        {
            putExperienceCommand.ExperienceId = experienceId;
            return await _mediator.Send(putExperienceCommand);
        }

        [HttpDelete]
        [Route("DeleteExperience/{experienceId}")]
        public async Task<ResponseDTO> DeleteExperience(Guid experienceId) => await _mediator.Send(new DeleteExperienceCommand(experienceId));
        #endregion
    }
}
