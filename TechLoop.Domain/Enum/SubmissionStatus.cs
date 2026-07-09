namespace TechLoop.Domain.Enums;

public enum SubmissionStatus
{
    Pending = 1,
    Running = 2,
    Accepted = 3,
    WrongAnswer = 4,
    TimeLimitExceeded = 5,
    MemoryLimitExceeded = 6,
    CompilationError = 7,
    RuntimeError = 8
}