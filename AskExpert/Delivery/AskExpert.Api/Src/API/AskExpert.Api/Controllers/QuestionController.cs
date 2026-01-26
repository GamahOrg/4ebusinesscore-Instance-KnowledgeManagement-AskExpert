namespace AskExperts.API.Controllers
{
	public class QuestionController:BaseController
	{
		private readonly IMediator _mediator;

		public QuestionController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public Task<ResponseDTO> PostQuestion([FromBody] PostQuestionDto questionDto)
		{
			return _mediator.Send(new PostQuestionCommand { QuestionDto = questionDto });
		}

		[HttpPut("{questionId}")]
		public Task<ResponseDTO> PutQuestion([FromBody] PutQuestionDto questionDto,Guid questionId)
		{
			return _mediator.Send(new PutQuestionCommand { QuestionId = questionId,QuestionDto = questionDto });
		}

		[HttpPut("PutRequestedQuestion/{questionId}")]
		public Task<ResponseDTO> PutRequestedQuestion(Guid questionId,[FromBody] PutRequestedQuestionDto requestedQuestionDto)
		{
			return _mediator.Send(new PutRequestedQuestionCommand { QuestionId = questionId,RequestedQuestionDto = requestedQuestionDto });
		}

		[HttpDelete("{questionId}")]
		public Task<ResponseDTO> DeleteQuestion(Guid questionId)
		{
			return _mediator.Send(new DeleteQuestionCommand() { QuestionId = questionId });
		}

	}
}
