using System.Management;



namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Get the PC name
        string pcName = Environment.MachineName;
        Console.WriteLine("PC Name: " + pcName);

        // Get the PC model
        string pcModel = GetPCModel();
        Console.WriteLine("PC Model: " + pcModel);

       
    
        }
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
