using System;
using System.Threading.Tasks;

namespace Orleans.Providers.InfluxDB.TestHost.Grains
{
    public class ProducerGrain : Grain, IProducerGrain
    {
        public async Task Start()
        {
            this.GetLogger().IncrementMetric("Starts");
        }
    }
}
