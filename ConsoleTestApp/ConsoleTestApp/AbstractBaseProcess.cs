using System.Threading;

namespace ConsoleTestApp
{
    public abstract class AbstractBaseProcess
    {
        protected int NumberOfProcesses { get; set; }
        protected abstract void Run();
    }

    public class ProcessImageFiles : AbstractBaseProcess
    {
        public ProcessImageFiles()
        {
            NumberOfProcesses = 10;
        }

        protected override void Run()
        {
            Thread.Sleep(2000);
        }
    }
}
