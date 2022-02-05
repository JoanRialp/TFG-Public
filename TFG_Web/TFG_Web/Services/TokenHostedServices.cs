using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.Services;

namespace TFG_Web.Services
{
    //Borrar token recuperacion BDD
    public class TokenHostedServices : BackgroundService, IHostedService, IDisposable
    {
        #region Dependencias Interfaces
        private int executionCount = 0;
        private readonly ILogger<TokenHostedServices> _logger;
        private Timer _timer;
        public IServiceScopeFactory _serviceScopeFactory;
        #endregion

        #region Constructor
        public TokenHostedServices(ILogger<TokenHostedServices> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this._logger = logger;
            this._serviceScopeFactory = serviceScopeFactory;
        }
        #endregion

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("Token Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        //public Task StartAsync(CancellationToken stoppingToken)
        //{
        //    return Task.CompletedTask;
        //}

        /// <summary>
        /// Eliminamos todos los tokens guardados en la BD (Token para recuperar la password)
        /// </summary>
        /// <param name="state"></param>
        private void DoWork(object state)
        {
            using (var scope = this._serviceScopeFactory.CreateScope())
            {
                //IScoped scoped = scope.ServiceProvider.GetRequiredService();

                var count = Interlocked.Increment(ref executionCount);

                this._logger.LogInformation(
                    "Token Hosted Service is working. Count: {Count}", count);

                var _tokenContrasenaServices = scope.ServiceProvider.GetRequiredService<ITokenContrasenaServices>();

                _tokenContrasenaServices.DeleteAll();
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("Token Hosted Service is stopping.");

            this._timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}