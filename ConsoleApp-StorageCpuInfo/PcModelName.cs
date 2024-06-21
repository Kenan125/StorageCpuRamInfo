using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace ConsoleApp_StorageCpuInfo
{
    public static class PcModelName
    {
        static string GetPCModel()
        {
            string model = string.Empty;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject share in searcher.Get())
            {
                model = share["Model"].ToString();
            }

            return model;
        }
    }

}
