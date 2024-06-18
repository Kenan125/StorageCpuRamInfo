using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp_StorageCpuRamInfo
{
    public static class RamInfoProvider
    {
        public static JObject GetRamInfo()
        {
            try
            {
                // Get the total RAM capacity
                float totalRam = GetTotalRAM();

                // Get the available RAM
                PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");
                float availableRam = ramCounter.NextValue();

                // Calculate used RAM
                float usedRam = totalRam - availableRam;

                // Calculate usage percentage
                float usagePercentage = ((float)usedRam / totalRam) * 100;

                JObject ramJson = new JObject
                {
                    ["totalMemory"] = FormatBytes(totalRam).ToString(),
                    ["availableMemory"] = FormatBytes(availableRam).ToString(),
                    ["usedMemory"] = FormatBytes(usedRam).ToString(),
                    ["usagePercentage"] = usagePercentage.ToString("F2"),

                };
                return ramJson;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                return null; // or return an error message as a JObject
            }
        }
        static float GetTotalRAM()
        {
            float totalRam = 0;
            ManagementObjectSearcher searcher = new("SELECT Capacity FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                totalRam += (float)Convert.ToDouble(obj["Capacity"]);
            }
            return totalRam;
        }
        static string FormatBytes(float bytes)
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
