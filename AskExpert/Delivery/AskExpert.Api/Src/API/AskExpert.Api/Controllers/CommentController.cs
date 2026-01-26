namespace AskExperts.API.Controllers
{
	public class CommentController:BaseController
	{
		private readonly IMediator _mediator;
		public CommentController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public Task<ResponseDTO> PostComment([FromForm] PostCommentDto commentDto)
		{
			return _mediator.Send(new PostCommentCommand { CommentDto = commentDto });
		}

		[HttpPut("{commentId}")]
		public Task<ResponseDTO> PutComment([FromForm] PutCommentDto commentDto,Guid commentId)
		{
			return _mediator.Send(new PutCommentCommand { CommentId = commentId,CommentDto = commentDto });
		}

		[HttpDelete("{commentId}")]
		public Task<ResponseDTO> DeleteComment(Guid commentId)
		{
			return _mediator.Send(new DeleteCommentCommand { CommentId = commentId });
		}
	}
}
