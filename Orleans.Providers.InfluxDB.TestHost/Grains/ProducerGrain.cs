using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.Providers.InfluxDB.TestHost.Grains
{
    public class ProducerGrain : Grain, IProducerGrain
    {
        public Task Start()
        {
            RegisterTimer(OnTimer, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
            return Task.CompletedTask;
        }

        private Task OnTimer(object state)
        {
            var random = new Random();
            var name = "Start";
            var commandName = $"{name}Command";
            var startTime = default(DateTimeOffset);
            var duration = default(TimeSpan);
            var success = false;
            var exception = new RankException("foo");
            var severityLevel = Severity.Error;
            var message = "bar";
            var responseCode = "bla";
            var properties = new Dictionary<string, string>
            {
                { "prop1", "prop1Val" },
                { "prop2", "prop2Val" },
            };
            var metrics = new Dictionary<string, double>
            {
                { "prop1", 1 },
                { "prop2", 2 },
            };

            var logger = this.GetLogger();

            logger.DecrementMetric($"Not{name}");
            logger.IncrementMetric(name);

            logger.DecrementMetric($"NotAgain{name}", random.Next(0, 10));
            logger.IncrementMetric($"Again{name}", random.Next(0, 10));

            //logger.TrackEvent(name, properties, metrics);
            //logger.Log(1, severityLevel, message, null, exception);
            //logger.TrackDependency(name, commandName, startTime, dura
            //logger.TrackException(exception, properties, metrics);
            //logger.TrackMetric(name, 1, properties);
            //logger.TrackRequest(name, startTime, duration, responseCode, success);
            //logger.TrackTrace(message, severityLevel, properties);

            return Task.CompletedTask;
        }
    }
}
