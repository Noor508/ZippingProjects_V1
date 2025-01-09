using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Compression;
using System.IO;
using System.Net;

namespace ZippingProjects_V1
{
    public class ZipFileOperation : IFileOperations
    {
        public List<ProjectRecord> ReadCsv(string filePath)
        {
            throw new NotImplementedException("ReadCsv method is not implemented in ZipFileOperation.");
        }

        public string CreateZipFile(ProjectRecord record, string destinationPath, string corporateId)
        {
            string filePath = record.DocFilePath;
            string projectId = record.ProjectID;
            string sourceFile = File.Exists(filePath) ? filePath : null;

            if (sourceFile == null)
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            string zipFolderPath = ConfigurationManager.AppSettings["archievePath"];
            if (!Directory.Exists(zipFolderPath))
            {
                Directory.CreateDirectory(zipFolderPath);
            }

            string corporateFolderPath = Path.Combine(zipFolderPath, corporateId);

            if (!Directory.Exists(corporateFolderPath))
            {
                Directory.CreateDirectory(corporateFolderPath);

            }

        //    Console.WriteLine($"Creating zip for project: {projectId}");

          
                string zipFileName = $"{record.ProjectName}.zip";
                string zipFilePath = Path.Combine(corporateFolderPath, zipFileName);

            try
            {
                string userCopyPath = Path.Combine(destinationPath, corporateId, record.ProjectName);
               // string userCopyPath = Path.Combine(destinationPath, corporateId, projectId);
                Directory.CreateDirectory(userCopyPath);

                // Copy only specified file to the temp directory
           //     File.Copy(sourceFile, Path.Combine(userCopyPath, Path.GetFileName(sourceFile)), true);

                File.Copy(sourceFile, Path.Combine(userCopyPath, record.DocumentName), true);


                // Zip only the copied file
                ZipProjectFolder(userCopyPath, zipFilePath);
            //  Console.WriteLine($"Zipped {sourceFile} to {zipFilePath}");

                return zipFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while zipping: {ex.Message}");
                return null;
            }
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string newDestDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, newDestDir);
            }
        }

        //public void LogActivity(string activity, string details)
        //{
        //    throw new NotImplementedException("LogActivity method is not implemented in ZipFileOperation.");
        //}

        private void ZipProjectFolder(string sourcePath, string zipFilePath)
        {
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }
            ZipFile.CreateFromDirectory(sourcePath, zipFilePath, CompressionLevel.Fastest, includeBaseDirectory: false);
        }
    }
}