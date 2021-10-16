using System.IO;
using System.Threading.Tasks;

namespace TG_App.View
{
  public interface ISave
  {
    Task SaveAndView(string filename, string contentType, MemoryStream stream);
  }
}