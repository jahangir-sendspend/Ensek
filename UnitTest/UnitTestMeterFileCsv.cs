
using Microsoft.AspNetCore.Http;

namespace Jahangir_Esnek_Test_UnitTest
{
    [TestClass]
    public class UnitTestMeterFileCsv
    {
        [TestMethod]
        public void MapToObject_FileIsNull()
        {
            var mappedObject = new API.Code.Models.MeterFileCsv().MapToObject(null);
            Assert.IsNull(mappedObject);
        }

        [TestMethod]
        public void MapToObject_FileIsValid()
        {
            /* Mocking */
            IFormFile formFile;
            var stream = System.IO.File.OpenRead(@"Meter_Reading.csv");
            formFile = new FormFile(stream, 0, stream.Length, "file", Path.GetFileName(stream.Name));

            var expectedMappedObject = Newtonsoft.Json.JsonConvert.SerializeObject(
                new List<API.Code.Models.Meter> {
                    new API.Code.Models.Meter
                    {
                        AccountId = "2344",
                        MeterReadingDateTime = "22/04/2019 09:24",
                        MeterReadValue = "1002",
                        DataStatus = String.Empty
                    }
                });
            
            var mappedObject = Newtonsoft.Json.JsonConvert.SerializeObject(new API.Code.Models.MeterFileCsv().MapToObject(formFile));

            Assert.AreEqual(expectedMappedObject,mappedObject);
        }
    }
}
