namespace Shared.Configurations
{
    public class QueueOptions
    {
        public const string SectionName = "QueueOptions";

        public required string Connection { get; init; }
        public required string QueueName { get; init; }
    }
}
