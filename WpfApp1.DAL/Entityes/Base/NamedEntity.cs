using System.ComponentModel.DataAnnotations;

namespace WpfApp1.DAL.Entityes.Base
{
    public abstract class NamedEntity : Entity
    {
        
        // public string Name { get; set; }

        private string _name;
        
        [Required]
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                OnPropertyChanged(nameof(Name));   
            }
        }
    }
}