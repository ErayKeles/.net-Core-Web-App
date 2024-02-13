
using AspNetCoreIdentityApp.Models;

namespace AspNetCoreIdentityApp.Models
{
    public class BeeHive
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Capacity { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }

}
