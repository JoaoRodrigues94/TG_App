using System;
using Xamarin.Forms;
using System.IO;
using Windows.Storage;
using TG_App.UWP.Banco;
using TG_App.Banco;

[assembly: Dependency(typeof(Caminho))]
namespace TG_App.UWP.Banco
{
  public class Caminho: ICaminho
  {
    public string ObterCaminho(string NomeArquivoBanco)
    {
      string caminhoDaPasta = ApplicationData.Current.LocalFolder.Path;

      string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

      return caminhoBanco;
    }
  }
}
