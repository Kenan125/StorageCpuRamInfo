using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Management;

namespace ConsoleApp_StorageCpuRamInfo
{
    public static class CpuInfoProvider
    {
        public static JObject GetCpuInfo()
        {
            // Get the number of CPU cores
            int cpuCores = Environment.ProcessorCount;

            // Get the CPU name
            string cpuName = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject share in searcher.Get())
            {
                cpuName = share["Name"].ToString();
            }

            // Get the CPU usage
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(2000);
            float cpuUsage = cpuCounter.NextValue();


            

            JObject cpuJson = new JObject
            {
                ["cpuName"] = cpuName.ToString(),
                ["cpuCoreNumber"] = cpuCores.ToString(),
                ["usedCpuPercentage"] = cpuUsage.ToString("F2"),
               
            };


            return cpuJson;
        }
    }
}
