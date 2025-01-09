using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZippingProjects_V1
{
    public  interface IFileOperations
    {
        List<ProjectRecord> ReadCsv(string filePath);
        string CreateZipFile(ProjectRecord record, string destinationPath, string corporateId);
        void CopyDirectory(string sourceDir, string destDir);
      //  void LogActivity(string activity, string details);
    }
}
