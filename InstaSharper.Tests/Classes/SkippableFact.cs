using Xunit;

namespace InstaSharper.Tests.Classes
{
    public sealed class SkippableFact : FactAttribute
    {
        public SkippableFact()
        {
            Skip = "This fact marked as skippable.";
        }
    }
}