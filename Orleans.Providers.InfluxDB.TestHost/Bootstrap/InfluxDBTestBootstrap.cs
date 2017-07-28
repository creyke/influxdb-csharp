using Orleans.Providers.InfluxDB.TestHost.Grains;
using System;
using System.Threading.Tasks;

namespace Orleans.Providers.InfluxDB.TestHost.Bootstrap
{
    public class InfluxDBTestBootstrap : IBootstrapProvider
    {
        public string Name => typeof(InfluxDBTestBootstrap).Name;

        public Task Close()
        {
            return Task.CompletedTask;
        }

        public async Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            var producer = providerRuntime.GrainFactory.GetGrain<IProducerGrain>(Guid.Empty);
            for (int i = 0; i < 10; i++)
            {
                await producer.Start();
            }
        }
    }
}
