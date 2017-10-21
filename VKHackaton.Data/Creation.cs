namespace VkHackathon.Models
{
    public class Creation
    {
        public long Id { get; set; }
        public Source Source { get; set; }
        public EntityType EntityType { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public string Photo { get; set; }
    }
}