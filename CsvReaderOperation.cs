using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ZippingProjects_V1
{
    public class CsvReaderOperation : IFileOperations
    {
        public List<ProjectRecord> ReadCsv(string filePath)
        {
            //Console.WriteLine("Reading CSV file with header.");

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, // CSV contains a header row
            }))
            {
                csv.Context.RegisterClassMap<ProjectRecordMap>();
                var records = csv.GetRecords<ProjectRecord>().ToList();


                // Handle empty CSV file
                if (!records.Any())
                {
                    Console.WriteLine("The CSV file is empty or contains no valid records.");
                    Console.WriteLine("Please  select correct csv file");
                  Console.ReadKey();
                }

                // Validate records to ensure no null or missing values
                foreach (var record in records)
                {
                    if (string.IsNullOrEmpty(record.ProjectID) ||
                        string.IsNullOrEmpty(record.DocFilePath) ||
                        string.IsNullOrEmpty(record.DocumentID) ||
                        string.IsNullOrEmpty(record.ProjectName) ||
                        string.IsNullOrEmpty(record.DocumentName))
                    {
                        Console.WriteLine($"Record is missing required fields: {record.ProjectID}, {record.DocFilePath}, {record.DocumentID}, {record.ProjectName}, {record.DocumentName}");
                    }
                }

                return records;
            }
        
        }
        

        public string CreateZipFile(ProjectRecord record, string destinationPath, string projectName)
        {
            // Create a directory named after ProjectName
            string projectDir = Path.Combine(destinationPath, projectName);
            Directory.CreateDirectory(projectDir);

            // Get the document name and copy it to the project directory
            string documentName = record.DocumentName;
            string sourceFilePath = record.DocFilePath;
            string destinationFilePath = Path.Combine(projectDir, documentName);

            try
            {
                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying file {sourceFilePath} to {destinationFilePath}: {ex.Message}");
                return null;
            }

            // Create the zip file
            string zipFilePath = Path.Combine(destinationPath, $"{projectName}.zip");
            if (File.Exists(zipFilePath)) File.Delete(zipFilePath); // Overwrite if already exists
            ZipFile.CreateFromDirectory(projectDir, zipFilePath);

            // Clean up temporary project folder
            Directory.Delete(projectDir, true);

            return zipFilePath;
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            throw new NotImplementedException();
        }

        public class ProjectRecordMap : ClassMap<ProjectRecord>
        {
            public ProjectRecordMap()
            {
                Map(m => m.ProjectID).Name("ProjectID");
                Map(m => m.DocFilePath).Name("DocFilePath");
                Map(m => m.DocumentID).Name("DocumentID");
                Map(m => m.ProjectName).Name("ProjectName");
                Map(m => m.DocumentName).Name("DocumentName");
            }
        }
    }
}