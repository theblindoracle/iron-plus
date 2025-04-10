using IronPlus.ViewModels.Base;
using SQLite;

namespace IronPlus.Models
{
    public class Barbell : ExtendedBindableObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int WeightInPounds { get; set; }
        public int WeightInKilograms { get; set; }
    }
}
