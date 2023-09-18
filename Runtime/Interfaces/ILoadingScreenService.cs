using System.Threading.Tasks;

namespace DJM.CoreServices
{
    public interface ILoadingScreenService
    {
        public Task Show();
        public Task Hide();
    }
}