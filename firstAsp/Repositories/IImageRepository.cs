using firstAsp.Models.Domain;

namespace firstAsp.Repositories
{
    public interface IImageRepository
    {
        Task<Image>Upload (Image image);

    }
}
