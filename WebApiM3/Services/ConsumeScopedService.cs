using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiM3.Context;
using WebApiM3.Entities;

namespace WebApiM3.Services
{
    public class ConsumeScopedService : IHostedService, IDisposable
    {
        private Timer _Timer;
     
        public ConsumeScopedService(IServiceProvider service) //Interface para usar IhostedService y El DbContext.
        {
            Services = service;
        }

        public IServiceProvider Services { get; }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _Timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20)); //Inicia el Timer cuandi se inica el servidor.
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) //Funcion se ejecuta cuando se detiene el sevidor.
        {
             _Timer?.Change(Timeout.Infinite, 0); //Detienen el Timer cuando este no es nulo.
            return Task.CompletedTask;
        }

        private void DoWork(object state) 
        {
            using (var scope = Services.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                string message = "ConsumedService: Recived message at: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                HostedServiceLogs log = new HostedServiceLogs() { Message = message };
                context.HostedServiceLogs.Add(log);
                context.SaveChanges();
                
            }
        }

        public void Dispose() //Funcion para limpiar los recursos del Timer.
        {
            _Timer?.Dispose(); //? para que lo limpie solo cuando el Timer no sea nulo.
        }


      
    }
}
