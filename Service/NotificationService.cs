using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Models;

namespace ATBM_HTTT_PH2.Services
{
    public class NotificationService : INotificationService
    {
        private readonly string _connectionString;

        public NotificationService()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
        }

        public List<Notification> GetNotificationsForUser(string username)
        {
            var notifications = new List<Notification>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();

                // Thiết lập session label cho người dùng
                using (var command = new OracleCommand("BEGIN SA_SESSION.SET_LABEL('THONGBAO_POLICY', :label); END;", connection))
                {
                    // Lấy label tương ứng với người dùng từ bảng NGUOIDUNG
                    string label = GetLabelForUser(username, connection);
                    command.Parameters.Add(new OracleParameter("label", label));
                    command.ExecuteNonQuery();
                }

                // Truy vấn thông báo dựa trên label
                using (var command = new OracleCommand("SELECT ID, NOIDUNG FROM THONGBAO", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new Notification
                            {
                                Id = reader.GetInt32(0),
                                NoiDung = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return notifications;
        }

        private string GetLabelForUser(string username, OracleConnection connection)
        {
            using (var command = new OracleCommand("SELECT CAPBAC, DONVI, COSO FROM NGUOIDUNG WHERE USERNAME = :username", connection))
            {
                command.Parameters.Add(new OracleParameter("username", username));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string capbac = reader.GetString(0);
                        string donvi = reader.GetString(1);
                        string coso = reader.GetString(2);
                        return $"{capbac}:{donvi}:{coso}";
                    }
                    else
                    {
                        throw new Exception("Người dùng không tồn tại.");
                    }
                }
            }
        }
    }
}
