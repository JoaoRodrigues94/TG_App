using Xamarin.Forms;
using System.IO;
using TG_App.Droid.Banco;
using TG_App.Banco;

[assembly: Dependency(typeof(Caminho))]
namespace TG_App.Droid.Banco
{
  public class Caminho : ICaminho
  {
    public string ObterCaminho(string NomeArquivoBanco)
    {
      string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); //retorna o caminho até uma determinada pasta

      string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

      return caminhoBanco;
    }
  }
}