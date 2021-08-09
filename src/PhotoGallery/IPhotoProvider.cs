using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoGallery
{
    public interface IPhotoProvider
    {
        Task<List<Image>> GetImages();
    }
}
