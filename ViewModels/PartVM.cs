using System;

namespace PcPartsManager.ViewModels
{
    public class PartVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubCategoryVM SubCategory { get; set; }
    }
}
