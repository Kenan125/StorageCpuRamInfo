using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PcInfoDatabase
{
    public class PcInfoRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public PcInfoRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void InsertDriveInfo(int pcId, string driveInfo, DateTime timestamp)
        {
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO DriveInfo (PcId, DriveInfo, Timestamp) VALUES (@PcId, @DriveInfo, @Timestamp)", connection);

                command.Parameters.AddWithValue("@PcId", pcId);
                command.Parameters.AddWithValue("@DriveInfo", driveInfo);
                command.Parameters.AddWithValue("@Timestamp", timestamp);

                command.ExecuteNonQuery();
            }
        }
    }
}
