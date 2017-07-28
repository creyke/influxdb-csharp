using InfluxDB.Collector;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Orleans.Providers.InfluxDB.Telemetry
{
    public class InfluxDBTelemetryConsumer :
        IMetricTelemetryConsumer,
        IEventTelemetryConsumer,
        IExceptionTelemetryConsumer,
        IDependencyTelemetryConsumer,
        IRequestTelemetryConsumer,
        IFlushableLogConsumer
    {
        public static MetricsCollector Collector { get { return Metrics.Collector; } }

        public InfluxDBTelemetryConsumer()
        {
            var process = Process.GetCurrentProcess();

            Metrics.Collector = new CollectorConfiguration()
                .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                .Tag.With("os", Environment.GetEnvironmentVariable("OS"))
                .Tag.With("process", Path.GetFileName(process.MainModule.FileName))
                .Batch.AtInterval(TimeSpan.FromSeconds(2))
                .WriteTo.InfluxDB("http://localhost:8086", "data")
                .CreateCollector();
        }

        public void Close()
        {
            Collector.Dispose();
        }

        public void DecrementMetric(string name)
        {
            Collector.Increment(name, -1);
        }

        public void DecrementMetric(string name, double value)
        {
            Collector.Increment(name, -Convert.ToInt64(value));
        }

        public void Flush()
        {
        }

        public void IncrementMetric(string name)
        {
            Collector.Increment(name);
        }

        public void IncrementMetric(string name, double value)
        {
            Collector.Increment(name, Convert.ToInt64(value));
        }

        public void Log(Severity severity, LoggerType loggerType, string caller, string message, IPEndPoint myIPEndPoint, Exception exception, int eventCode = 0)
        {
            throw new NotImplementedException();
        }

        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            throw new NotImplementedException();
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            throw new NotImplementedException();
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            throw new NotImplementedException();
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void TrackMetric(string name, TimeSpan value, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            throw new NotImplementedException();
        }
    }
}
