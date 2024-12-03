namespace PracticeWEBProjectApi.DTO
{
    public class BlogDTO
    {
        public int Blogid { get; set; }

        public string BlogTitle { get; set; }

        public string BlogDescription { get; set; }

        public string BlogImage { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public int DeletedBy { get; set; }
        public string UserType { get; set; }
        public bool IsLock { get; set; }

        public bool IsDelete { get; set; }
    }
}
