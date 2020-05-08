namespace Ugugushka.WebUI.ViewModels
{
    public class ToyItemViewModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string PartitionName { get; set; }
        public decimal Price { get; set; }
        public bool IsOnStock { get; set; }
    }
}
