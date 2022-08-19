using API.Code.BL;
using API.Code.Interfaces;
using API.Code.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MeterController : Controller
    {
        [HttpPost]
        [Route("meter-reading-uploads")]
        public async Task<JsonResult> UploadBatchReadings(IFormFile file)
        {
            try
            {

                IMeterFile csvMeterFile = new MeterFileCsv();
                var mappedObject = csvMeterFile.MapToObject(file);
                var bl = new Meters().UploadBatchReadings(mappedObject);
                return Json(new { d = new Common.Result(Common.ResultType.OK) { Html = Newtonsoft.Json.JsonConvert.SerializeObject(mappedObject), Message = "" } });

            }
            catch (Exception ex)
            {
                return Json(new { d = new Common.Result(Common.ResultType.ERR) { Html = "", Message = "Internal error occurred." } });
            }
        }
    }
}
