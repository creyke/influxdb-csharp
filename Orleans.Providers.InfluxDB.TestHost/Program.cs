﻿using Orleans.Providers.InfluxDB.Telemetry;
using Orleans.Runtime.Configuration;
using Orleans.Storage;
using System;

namespace Orleans.Providers.InfluxDB.TestHost
{
    class Program
    {
        private static InfluxDBTelemetryConsumer influx;

        private static MemoryStorage storage;

        private static OrleansWrapper hostWrapper;

        public static int Main(string[] args)
        {
            int exitCode = StartSilo(args);

            Console.WriteLine("Press Enter to terminate...");
            Console.ReadLine();

            exitCode += ShutdownSilo();

            //either StartSilo or ShutdownSilo failed would result on a non-zero exit code. 
            return exitCode;
        }

        private static int StartSilo(string[] args)
        {
            // define the cluster configuration
            var config = ClusterConfiguration.LocalhostPrimarySilo();

            hostWrapper = new OrleansWrapper(config, args);
            return hostWrapper.Run();
        }

        private static int ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                return hostWrapper.Stop();
            }
            return 0;
        }
    }
}