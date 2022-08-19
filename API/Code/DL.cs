using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Code.DL
{
    public class EnsekContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder p)
        {
            if (!p.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString("EnsekConnection");
                p.UseSqlServer(connectionString);

            }
        }
    }

    public class Meters
    {
        public string InsertReading(Models.Meter meter)
        {
            var dataStatus = string.Empty;
            using (var db = new EnsekContext())
            {
                using (var cmd = db.Database.GetDbConnection().CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "[dbo].InsertMeterReadings";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@accountId", meter.AccountId));
                        cmd.Parameters.Add(new SqlParameter("@reading", meter.MeterReadValue));
                        cmd.Parameters.Add(new SqlParameter("@readingDate", DateTime.Parse(meter.MeterReadingDateTime).ToString("yyyy-MM-dd HH:mm")));
                        cmd.Connection.Open();
                        dataStatus = cmd.ExecuteScalar().ToString();
                        cmd.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        dataStatus = "Internal Error Occurred.";
                        if (cmd.Connection.State == System.Data.ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
            return dataStatus;
        }
    }
}
