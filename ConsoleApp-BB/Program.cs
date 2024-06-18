using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Xml;
using Newtonsoft.Json;

namespace SystemMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            string dateFolder = DateTime.Now.ToString("yyyyMMdd");
            string folderPath = Path.Combine(Environment.CurrentDirectory, dateFolder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            for (int i = 0; i < 5; i++)
            {
                var stats = GetSystemStats();
                string jsonFile = Path.Combine(folderPath, $"system_stats_{i}.json");
                File.WriteAllText(jsonFile, JsonConvert.SerializeObject(stats, Formatting.Indented));
                Console.WriteLine($"System stats saved to {jsonFile}");
                Console.WriteLine("Waiting for 12 seconds...");
                System.Threading.Thread.Sleep(12000);
            }
        }

        static dynamic GetSystemStats()
        {
            dynamic stats = new System.Dynamic.ExpandoObject();

            // Get CPU usage percentage
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            stats.cpu_percent = cpuCounter.NextValue();

            // Get RAM usage percentage
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Committed Bytes");
            long totalMemory = ramCounter.RawValue;
            long availableMemory = new PerformanceCounter("Memory", "Available Bytes").RawValue;
            stats.memory_percent = (1 - (double)availableMemory / totalMemory) * 100;

            // Get storage filled percentage
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    stats.disk_percent = (drive.TotalSize - drive.AvailableFreeSpace) / (double)drive.TotalSize * 100;
                }
            }

            return stats;
        }
    }
}
