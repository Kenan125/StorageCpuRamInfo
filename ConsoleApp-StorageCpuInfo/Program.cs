using Newtonsoft.Json.Linq;
namespace ConsoleApp_StorageCpuRamInfo
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string filePath = "../../../PcSpecs.json";

            while (true)
            {

                // Read the existing JSON content
                JArray driveInfoArray;
                JArray cpuInfoArray;
                JArray ramInfoArray;
                JObject jsonRoot;

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    jsonRoot = JObject.Parse(json);
                }
                else
                {

                    jsonRoot = new JObject();

                }

                driveInfoArray = jsonRoot["DriveInfo"] as JArray ?? new JArray();
                cpuInfoArray = jsonRoot["CpuInfo"] as JArray ?? new JArray();
                ramInfoArray = jsonRoot["RamInfo"] as JArray ?? new JArray();



                // Get the new drive information and add a timestamp
                JObject newDriveInfo = DriveInfoProvider.GetDriveInfo();

                JObject newCpuInfo = CpuInfoProvider.GetCpuInfo();

                JObject newRamInfo = RamInfoProvider.GetRamInfo();

                newDriveInfo["Timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                newCpuInfo["Timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                newRamInfo["Timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                driveInfoArray.Add(newDriveInfo);
                cpuInfoArray.Add(newCpuInfo);
                ramInfoArray.Add(newRamInfo);

                jsonRoot["DriveInfo"] = driveInfoArray;
                jsonRoot["CpuInfo"] = cpuInfoArray;
                jsonRoot["RamInfo"] = ramInfoArray;


                // Write the updated array back to the file
                File.WriteAllText(filePath, jsonRoot.ToString(Newtonsoft.Json.Formatting.Indented));



                // Print the latest entry to the console
                Console.WriteLine("Latest JSON entry:");

                // Wait for 12 seconds before the next update
                Thread.Sleep(12000);

            }
        }




    }

}

