using System.IO;
using Xamarin.Forms;
using TG_App.iOS.Banco;
using TG_App.Banco;

[assembly: Dependency(typeof(Caminho))]
namespace TG_App.iOS.Banco
{
  public class Caminho: ICaminho
  {
    public string ObterCaminho(string NomeArquivoBanco)
    {
      string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); //retorna o caminho até uma determinada pasta

      string caminhoBiblioteca = Path.Combine(caminhoDaPasta, "..", "Library");

      string caminhoBanco = Path.Combine(caminhoBiblioteca, NomeArquivoBanco);

      return caminhoBanco;
    }
  }
}