namespace OfficeTechRepairSystem.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
    }
}
