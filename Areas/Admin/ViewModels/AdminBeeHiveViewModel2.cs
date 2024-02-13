namespace AspNetCoreIdentityApp.Areas.Admin.ViewModels
{
    public class AdminBeeHiveViewModel2
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; } // Add the location property
        public DateTime ProductionDate { get; set; } // Add the production date property
        public int Capacity { get; set; } // Add the capacity property
    }
}
