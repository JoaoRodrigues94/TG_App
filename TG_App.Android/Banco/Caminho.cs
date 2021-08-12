using Xamarin.Forms;
using System.IO;
using TG_App.Droid.Banco;

[assembly: Dependency(typeof(Caminho))]
namespace TG_App.Droid.Banco
{
  class Caminho
  {
    public string ObterCaminho(string NomeArquivoBanco)
    {
      string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); //retorna o caminho até uma determinada pasta

      string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

      return caminhoBanco;
    }
  }
}