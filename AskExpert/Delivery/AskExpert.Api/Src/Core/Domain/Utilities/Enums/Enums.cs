namespace Domain.Enums
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }
    public enum ActionNameEnum
    {
        Get,
        Post,
        Put,
        Delete,
    }
    public enum KnowledgeType
    {
        Private,
        Public
    }
    public enum LikeStatus
    {
        Like,
        Dislike
    }
    public enum RequestStage
    {
        Pending = 1,
        Accept = 2,
        Rejected = 3
    }
	public enum QuestionStatus
	{
        New,
        Answered
	}
    public enum QuestionType
    {
        Public,
        Private
        
    }
}