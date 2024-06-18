using System;
using System.Diagnostics;
using System.Management;

class Program
{
    public static void Main()
    {
        while (true)
        {
            // Get the number of CPU cores
            int cpuCores = Environment.ProcessorCount;
            Console.WriteLine($"Your system has {cpuCores} CPU cores.");

            // Get the CPU usage
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1500);
            float cpuUsage = cpuCounter.NextValue();
            Console.WriteLine($"CPU usage: {cpuUsage:F2}%");

            // Get the number of physical processors
            int physicalProcessors = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                physicalProcessors = int.Parse(item["NumberOfProcessors"].ToString());
            }
            Console.WriteLine($"Number of physical processors: {physicalProcessors}");

            // Get the number of cores
            int coreCount = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }
            Console.WriteLine($"Number of cores: {coreCount}");

            // Get the number of logical processors
            int logicalProcessors = Environment.ProcessorCount;
            Console.WriteLine($"Number of logical processors: {logicalProcessors}");
        }
    }
}