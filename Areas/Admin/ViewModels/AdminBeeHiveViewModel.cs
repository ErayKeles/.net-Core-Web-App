// Areas/Admin/ViewModels/AdminBeeHiveViewModel.cs
namespace AspNetCoreIdentityApp.Areas.Admin.ViewModels
{
    public class AdminBeeHiveViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Capacity { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
