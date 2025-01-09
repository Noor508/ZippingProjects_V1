using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZippingProjects_V1
{
    public class DirectoryCopyOperation :IFileOperations
    {
        public List<ProjectRecord> ReadCsv(string filePath)
        {
            throw new NotImplementedException("ReadCsv method is not implemented in DirectoryCopyOperation.");
        }

        public string CreateZipFile(ProjectRecord record, string destinationPath, string corporateId)
        {
            throw new NotImplementedException("CreateZipFile method is not implemented in DirectoryCopyOperation.");
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
              

                if (File.Exists(destFile))
                {
                   Console.WriteLine($"File '{Path.GetFileName(file)}' already exists in the destination directory.");
                }

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
        //    throw new NotImplementedException("LogActivity method is not implemented in DirectoryCopyOperation.");
        //}
    }
}
