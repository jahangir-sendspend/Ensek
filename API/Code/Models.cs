using System.Text.RegularExpressions;

namespace API.Code.Models
{
    public class Common
    {
        public enum ResultType
        {
            OK = 1,
            ERR_VALIDATION = 2,
            ERR = 3,
            AUTH = 4
        }

        public class Result
        {
            public Result(ResultType r)
            {
                Code = r.ToString();
            }
            public string Id { get; set; }
            public string Code { get; set; }
            public string Html { get; set; }
            public string Message { get; set; }
        }
    }
    public class Meter
    {
        public string AccountId { get; set; }
        public string MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
        public string DataStatus { get; set; }

        public bool IsValidReading
        {
            get
            {
                return Regex.IsMatch(MeterReadValue, @"^(?:\d{1}|\d{2}|\d{3}|\d{4}|\d{5})$");
            }
        }
    }

    public class MeterFileCsv
        : Interfaces.IMeterFile
    {
        public IList<Models.Meter> MapToObject(IFormFile file)
        {
            IList<Models.Meter> meters;
            try
            {
                var result = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.Add(reader.ReadLine());
                }
                meters = new List<Models.Meter>();
                var lines = result.Select(a => a.Split(';')).ToList();
                foreach (var line in lines)
                {
                    var lineData = line.FirstOrDefault().Split(',');
                    if (lineData.Count() >= 3
                        && int.TryParse(lineData[0], out int s))
                    {
                        meters.Add(new Models.Meter
                        {
                            AccountId = lineData[0].Trim(),
                            MeterReadingDateTime = lineData[1].Trim(),
                            MeterReadValue = lineData[2].Trim(),
                            DataStatus = String.Empty
                        });
                    }
                }
            }
            catch (Exception ex) { meters = null; Console.WriteLine(ex.Message); }
            return meters;
        }
    }
}
