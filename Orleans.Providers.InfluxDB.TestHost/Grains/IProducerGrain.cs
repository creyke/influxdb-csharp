using System.Threading.Tasks;

namespace Orleans.Providers.InfluxDB.TestHost.Grains
{
    public interface IProducerGrain : IGrainWithGuidKey
    {
        Task Start();
    }
}
