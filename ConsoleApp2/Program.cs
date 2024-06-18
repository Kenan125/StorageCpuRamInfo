using System;
using System.Diagnostics;
using System.Management;

namespace RamUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    // Get the total RAM capacity
                    ulong totalRam = GetTotalRAM();
                    Console.WriteLine("Total RAM: " + FormatBytes(totalRam));

                    // Get the available RAM
                    PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");
                    float availableRam = ramCounter.NextValue();

                    // Calculate used RAM
                    ulong usedRam = totalRam - (ulong)availableRam;
                    Console.WriteLine("Used RAM: " + FormatBytes(usedRam));

                    // Calculate usage percentage
                    float usagePercentage = ((float)usedRam / totalRam) * 100;
                    Console.WriteLine("RAM Usage Percentage: " + usagePercentage.ToString("0.00") + "%");

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                Thread.Sleep(12000);
            }

            static ulong GetTotalRAM()
            {
                ulong totalRam = 0;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");
                foreach (ManagementObject obj in searcher.Get())
                {
                    totalRam += (ulong)obj["Capacity"];
                }
                return totalRam;
            }

            static string FormatBytes(ulong bytes)
            {
                string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB" };
                int counter = 0;
                decimal number = (decimal)bytes;
                while (Math.Round(number / 1024) >= 1)
                {
                    number /= 1024;
                    counter++;
                }
                return string.Format("{0:n1} {1}", number, suffixes[counter]);
            }
        }
    }
}
