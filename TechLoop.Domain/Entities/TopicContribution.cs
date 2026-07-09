using TechLoop.Domain.Enums;

namespace TechLoop.Domain.Entities;
public class TopicContribution
{
    public int Id { get; set; }
    public Guid LearnerId { get; set; }
    public int TechnologyId { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } =  string.Empty;
    public ContributionStatus Status { get; set; }
    public string ReviewNote { get; set; }  = string.Empty;
    public Guid? ReviewedBy { get; set; }
    public DateTime? PublishedAt { get; set; }
    public Guid? PublishedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

}