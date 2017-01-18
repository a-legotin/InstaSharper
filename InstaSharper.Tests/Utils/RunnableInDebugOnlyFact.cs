using System.Diagnostics;
using Xunit;

namespace InstaSharper.Tests.Utils
{
    public sealed class RunnableInDebugOnlyFact : FactAttribute
    {
        public RunnableInDebugOnlyFact()
        {
            if (!Debugger.IsAttached)
                Skip = "This fact only running in interactive mode.";
        }
    }
}