using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZippingProjects_V1
{
    public class DeleteTempFolder
    {
        public void DeletionTempFolder (string tempFolderPath)
        {
            // Ask user if they want to delete subdirectories inside tempFolderPath
            Console.WriteLine("\nDo you want to delete the subdirectories inside the temp folder? (y/n)");
            string userResponse = Console.ReadLine()?.Trim().ToLower();

            if (userResponse == "y" || userResponse == "yes")
            {
                if (Directory.Exists(tempFolderPath))
                {
                    try
                    {
                        string[] subdirectories = Directory.GetDirectories(tempFolderPath);
                        foreach (string subdirectory in subdirectories)
                        {
                            try
                            {
                                Console.WriteLine($"Deleting subdirectory: {subdirectory}");
                                Directory.Delete(subdirectory, true);  // true means delete contents recursively
                                Console.WriteLine($"Subdirectory deleted: {subdirectory}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting subdirectory {subdirectory}: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error accessing the temp folder: {ex.Message}");
                    }
                }
            }
        }
    }
}
