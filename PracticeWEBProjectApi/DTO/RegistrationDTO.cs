namespace PracticeWEBProjectApi.DTO
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public int DeletedBy { get; set; }
        public string UserType { get; set; }
        public bool IsLock { get; set; }

        public string UserName { get; set; }
    }
}
