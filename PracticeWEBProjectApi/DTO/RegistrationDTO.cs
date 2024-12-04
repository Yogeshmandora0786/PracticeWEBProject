namespace PracticeWEBProjectApi.DTO
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UserType { get; set; }
        public bool? IsLock { get; set; }
         public string UserName { get; set; }

    }
}
