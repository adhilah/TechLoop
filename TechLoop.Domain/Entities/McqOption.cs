namespace TechLoop.Domain.Entities;
public class McqOption
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; } = string.Empty;

    public bool isCorrect { get; set; }

    public int position { get; set; }

   public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public Guid UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }
}