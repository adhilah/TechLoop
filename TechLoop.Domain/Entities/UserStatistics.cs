namespace TechLoop.Domain.Entities;
public class UserStatistics
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int ReputationPoints { get; set; }

    public int QuestionSolved { get; set; }

    public int CodingSolved { get; set; }

    public int McqSolved { get; set; }
    
    public int TotalSubmission { get; set; }

    public int AcceptedSubmission { get; set; }

    public int FailedSubmissions { get; set; }

    public int TotalTimeSpentMinutes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}