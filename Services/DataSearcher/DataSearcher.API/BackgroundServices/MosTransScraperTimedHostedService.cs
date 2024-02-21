using MassTransit;

namespace DataSearcher.API.BackgroundServices;

public class MosTransScraperTimedHostedService: IHostedService, IDisposable
{
    private readonly ILogger<MosTransScraperTimedHostedService> _logger;
    private readonly IBusControl _busControl;
    private readonly IServiceProvider _serviceProvider;
    private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/transportsQueue");
    
    private Timer _timer;
    private const int TimerSeconds = 300;

    public MosTransScraperTimedHostedService(IBusControl busControl, IServiceProvider provider,
        ILogger<MosTransScraperTimedHostedService> logger)
    {
        _logger = logger;
        _busControl = busControl;
        _serviceProvider = provider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MosTrans scraper host service running");
        
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(TimerSeconds));
        return Task.CompletedTask;
    }

    private async void DoWork(object state) => _logger.LogInformation("i`m working");

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MosTrans scraper is stopping");
        
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}