using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiM3.Services
{
    public class IHotedServiceExample : IHostedService, IDisposable
    {
        [Obsolete]
        private readonly IHostingEnvironment environment;
        private readonly string fileName = "File1.txt";
        private Timer timer;

       [Obsolete]
        public IHotedServiceExample(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("Process Started puto");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        [Obsolete]
        private void DoWork(object state)
        {
            WriteToFile("Doing something at" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        [Obsolete]
        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("Process Stopped puto");
            timer?.Change(Timeout.Infinite, 0); //Detienen el Timer cuando este no es nulo.
            return Task.CompletedTask;
        }

        [Obsolete]
        private void WriteToFile(string message)
        {
            var path = $@"{environment.ContentRootPath}\{fileName}";
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }

        public void Dispose() //Funcion para limpiar los recursos del Timer.
        {
            timer?.Dispose(); //? para que lo limpie solo cuando el Timer no sea nulo.
        }
    }
}
