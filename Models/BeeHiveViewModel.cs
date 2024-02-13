using System.ComponentModel.DataAnnotations;


namespace AspNetCoreIdentityApp.Models
{
    public class BeeHiveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Konum zorunludur.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Üretim Tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required(ErrorMessage = "Kapasite zorunludur.")]
        public int Capacity { get; set; }
      
    }

}
