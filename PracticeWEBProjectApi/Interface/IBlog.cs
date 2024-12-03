using PracticeWEBProjectApi.DTO;

namespace PracticeWEBProjectApi.Interface
{
    public interface IBlog
    {
        Task<List<BlogDTO>> Blog_All();
        Task<BlogDTO> Blog_ById(int Blogid);
        Task<CommonResponceDTO> Blog_Delete(int Blogid);

    }
}
