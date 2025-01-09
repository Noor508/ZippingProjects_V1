using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ZippingProjects_V1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Start the stopwatch to measure execution time
            Stopwatch stopwatch = Stopwatch.StartNew();

            Console.WriteLine("Execution started");
            IFileOperations csvReader = new CsvReaderOperation();
            IFileOperations zipFileOperation = new ZipFileOperation();

            string zipFolderPath = ConfigurationManager.AppSettings["archivePath"];
            string tempFolderPath = ConfigurationManager.AppSettings["tempFolderPath"];
            string csvsourcePath = ConfigurationManager.AppSettings["csvsourcePath"];

            // Validate CSV file path
            if (!File.Exists(csvsourcePath) || Path.GetExtension(csvsourcePath).ToLower() != ".csv")
            {
                Console.WriteLine($"The specified file does not exist or is not a CSV file: {csvsourcePath}");
                Console.ReadKey();
                return;
            }

            // Validate destination path
            if (!Directory.Exists(tempFolderPath))
            {
                Console.WriteLine($"The specified destination path does not exist: {tempFolderPath}");
                return;
            }

            string corporateId = Path.GetFileNameWithoutExtension(csvsourcePath);
            Console.WriteLine($"Reading CSV file {csvsourcePath}");

            var records = csvReader.ReadCsv(csvsourcePath);  // Using the CsvReaderOperation
            //List<Task> tasks = new List<Task>();
            int processedCount = 0;

            Console.WriteLine("Processing records");
            foreach (var record in records)
            {
                if (record != null)
                {
                    //tasks.Add(Task.Factory.StartNew(() => {
                    //    string originalFolderPath = zipFileOperation.CreateZipFile(record, tempFolderPath, corporateId);  // Using ZipFileOperation
                    //    Console.WriteLine($"Zip created at {originalFolderPath}");

                    //    Console.WriteLine("----------\n");
                    //}));

                    string originalFolderPath = zipFileOperation.CreateZipFile(record, tempFolderPath, corporateId);  // Using ZipFileOperation
                    processedCount++;

                    if (processedCount % 100 == 0)
                    {
                        Console.WriteLine($"Successfully processed {processedCount} records ");
                    }

                }
               
            }
            // Stop the stopwatch
            stopwatch.Stop();
            Console.WriteLine($"Total records : {processedCount}");
            // Display total execution time
            Console.WriteLine("\nProcess completed");
          //  Console.WriteLine();
            Console.WriteLine($"Total Execution Time: {stopwatch.Elapsed}");

          DeleteTempFolder deleteTempFolder = new DeleteTempFolder();
          deleteTempFolder.DeletionTempFolder(tempFolderPath);


            Console.ReadLine();
        }
    }
}