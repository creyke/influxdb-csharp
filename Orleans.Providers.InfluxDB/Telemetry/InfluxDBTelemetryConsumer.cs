using InfluxDB.Collector;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;

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

        public InfluxDBTelemetryConsumer(XmlAttribute serverXml, XmlAttribute databaseXml)
        {
            var process = Process.GetCurrentProcess();

            Metrics.Collector = new CollectorConfiguration()
                .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                .Tag.With("os", Environment.GetEnvironmentVariable("OS"))
                .Tag.With("process", Path.GetFileName(process.MainModule.FileName))
                .Batch.AtInterval(TimeSpan.FromSeconds(2))
                .WriteTo.InfluxDB($"http://{serverXml.Value}", databaseXml.Value)
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
            // not required - handled by pipelined connector thread.
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
            // TODO.
        }

        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            // TODO.
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            // TODO.
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            // TODO.
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            Collector.Measure(name, Convert.ToInt64(value), (IReadOnlyDictionary<string,string>)properties);
        }

        public void TrackMetric(string name, TimeSpan value, IDictionary<string, string> properties = null)
        {
            Collector.Measure(name, value.TotalMilliseconds, (IReadOnlyDictionary<string, string>)properties);
        }

        public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            // TODO.
        }
    }
}
