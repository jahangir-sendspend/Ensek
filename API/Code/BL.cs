namespace API.Code.BL
{
    public class Meters
    {
        public bool UploadBatchReadings(IList<Models.Meter> data)
        {
            var ret = true;
            try
            {
                var dlObj = new DL.Meters();
                data.ToList().ForEach(
                    x =>
                    {
                        if (x.IsValidReading)
                        {
                            x.DataStatus = dlObj.InsertReading(x);
                        }
                        else
                        {
                            x.DataStatus = "Invalid meter reading.";
                        }


                    }
                    );
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
}
