using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritedUtility
{
    public class LogActivityOperations : IFileOperations
    {
        public List<ProjectRecord> ReadCsv(string filePath)
        {
            throw new NotImplementedException("ReadCsv method is not implemented in LogActivityOperation.");
        }

        public string CreateZipFile(ProjectRecord record, string destinationPath, string corporateId)
        {
            throw new NotImplementedException("CreateZipFile method is not implemented in LogActivityOperation.");
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            throw new NotImplementedException("CopyDirectory method is not implemented in LogActivityOperation.");
        }

        public void LogActivity(string activity, string details)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SureCloseDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ActivityLog (Activity, Details) VALUES (@Activity, @Details)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Activity", activity);
                    command.Parameters.AddWithValue("@Details", details);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
