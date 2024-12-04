namespace PracticeWEBProjectApi.DTO
{
    public class BlogDTO
    {
        public int Blogid { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogDescription { get; set; }
        public string? BlogImage { get; set; }
        public IFormFile? ImagePath { get; set; }
 //       public bool? IsActive { get; set; }
       public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
 //       public int? DeletedBy { get; set; } 
  //     public bool? IsDeleted { get; set; }

    
    }
}
