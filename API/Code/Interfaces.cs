namespace API.Code.Interfaces
{
    public interface IMeterFile
    {
        IList<Models.Meter> MapToObject(IFormFile file);
    }
}
