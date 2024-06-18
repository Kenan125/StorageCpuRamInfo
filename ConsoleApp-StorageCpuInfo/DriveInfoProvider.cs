using Newtonsoft.Json.Linq;

namespace ConsoleApp_StorageCpuRamInfo
{
    public static class DriveInfoProvider
    {
        public static JObject GetDriveInfo()
        {

            

                DriveInfo[] drives = DriveInfo.GetDrives();
                JObject driveInfoJson = new JObject();

                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        double totalSpace = drive.TotalSize / (double)1024 / 1024 / 1024; // Convert to GB
                        double availableSpace = drive.AvailableFreeSpace / (double)1024 / 1024 / 1024; // Convert to GB
                        double usedSpace = totalSpace - availableSpace;
                        double percentage = (usedSpace / totalSpace) * 100;


                        JObject driveJson = new JObject
                        {
                            ["TotalSpaceGB"] = totalSpace.ToString("F2"),
                            ["AvailableSpaceGB"] = availableSpace.ToString("F2"),
                            ["UsedSpaceGB"] = usedSpace.ToString("F2"),
                            ["UsedPercentage"] = percentage.ToString("F2")
                        };

                        // Use drive name without trailing backslashes as key
                        string driveName = drive.Name.TrimEnd('\\');
                        driveInfoJson[driveName] = driveJson;
                    }
                }

                return driveInfoJson;
            
            
        }

    }
}
