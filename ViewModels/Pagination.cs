namespace PcPartsManager.ViewModels
{
    public class Pagination<T>
    {
        public int Length { get; set; }
        public IEnumerable<T> Data { get; set;}
    }
}