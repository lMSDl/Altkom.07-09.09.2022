using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace WindowsServices.Topshelf
{
    public class CustomService : ServiceControl
    {
        private Timer _timer;
        public bool Start(HostControl hostControl)
        {
            WriteMessage("Strting..");
            _timer = new Timer(Work, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            WriteMessage("Stopping..");
            return true;
        }
        private readonly string? _filename = "c:\\CustomService\\TopshelfService.txt";
        private void WriteMessage(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filename));
            File.AppendAllText(_filename, message);
        }

        private void Work(object? state)
        {
            WriteMessage("Working...");
        }

    }
}
