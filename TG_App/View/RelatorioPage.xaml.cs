using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using TG_App.Model;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class RelatorioPage : ContentPage
  {
    public RelatorioPage()
    {
      InitializeComponent();
    }

    public void Pesquisar(object sender, EventArgs args)
    {
      DBExame DBE = new DBExame();
      DBSugestao DBS = new DBSugestao();

      var user = new Validacao().Listagem().SingleOrDefault();


      var listaE = DBE.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();
      var listaS = DBS.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();

      int mesInicio = Convert.ToInt32(Inicio.Text.Substring(3, 2));
      int mesTermino = Convert.ToInt32(Termino.Text.Substring(3, 2));
      string anoInicio = Inicio.Text.Substring(6, 4);
      string anoTermino = Termino.Text.Substring(6, 4);

      List<SugestaoView> dados = new List<SugestaoView>();
      for(var i = mesInicio; i <= mesTermino; i++)
      {
        var mes = i;
        string m = "";

        if (mes.ToString().Length == 1) m = "0" + mes.ToString();
        else m = mes.ToString();

        for(var j = 1; j <= 31; j++)
        {
          var d = j.ToString().Length == 1 ? "0" + j.ToString() : j.ToString();

          //var e = listaE.Where(x => x.Data.Substring(0, 7).Contains(d + "/" + mes.ToString() + "/" + anoInicio)).ToList();
          //var s = listaS.Where(x => x.Data.Substring(0, 7).Contains(d + "/" + mes.ToString() + "/" + anoTermino)).ToList();

          foreach(var item in listaE)
          {
            SugestaoView y = new SugestaoView
            {
              //Data = item.Data
            };
            dados.Add(y);
          }
          foreach(var item in listaS)
          {
            SugestaoView y = new SugestaoView
            {
             // Data = item.Data
            };
            dados.Add(y);
          }
        }
      }
      var res = dados;
    }
  }
}