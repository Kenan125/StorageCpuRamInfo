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


            // Get the CPU usage
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(2000);
            float cpuUsage = cpuCounter.NextValue();


            // Get the number of physical processors
            int physicalProcessors = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                physicalProcessors = int.Parse(item["NumberOfProcessors"].ToString());
            }
            

            // Get the number of cores
            int coreCount = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }


            // Get the number of logical processors
            int logicalProcessors = Environment.ProcessorCount;

            JObject cpuJson = new JObject
            {
                ["cpuCoreNumber"] = cpuCores.ToString(),
                ["usedCpuPercentage"] = cpuUsage.ToString("F2"),
                ["physicalProcessors"] = physicalProcessors.ToString(),
                ["numberOfCores"] = coreCount.ToString(),
                ["numberOfLogicalProcessors"] = logicalProcessors.ToString(),
            };


            return cpuJson;
        }
    }
}
