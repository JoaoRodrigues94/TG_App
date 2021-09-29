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

      Inicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
      Termino.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    public void Pesquisar(object sender, EventArgs args)
    {
      DBExame DBE = new DBExame();
      DBSugestao DBS = new DBSugestao();

      var user = new Validacao().Listagem().SingleOrDefault();

      int dia = Convert.ToInt32(Inicio.Text.Substring(0, 2));
      int mes = Convert.ToInt32(Inicio.Text.Substring(3, 2));
      int ano = Convert.ToInt32(Inicio.Text.Substring(6, 4));

      DateTime dtInicio = Convert.ToDateTime(mes + "/" + dia + "/" + ano + " 00:00:00");

      int diaTermino = Convert.ToInt32(Termino.Text.Substring(0, 2));
      int mesTermino = Convert.ToInt32(Termino.Text.Substring(3, 2));
      int anoTermino = Convert.ToInt32(Termino.Text.Substring(6, 4));

      DateTime dtTermino = Convert.ToDateTime(mesTermino + "/" + diaTermino + "/" + anoTermino + " 23:59:59");


      var listaE = DBE.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID && c.Data >= dtInicio && c.Data <= dtTermino).ToList();
      var listaS = DBS.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID && c.Data >= dtInicio && c.Data <= dtTermino).ToList();

      List<SugestaoView> dados = new List<SugestaoView>();

      int mediaG= 0;
      int i = 0;
      string res = "";

      string MaiorResultado = "";

      foreach(var item in listaE)
      {
        SugestaoView sql = new SugestaoView
        {
          Data = item.Data.ToString("dd/MM/yyyyy HH:mm"),
          Resultado = item.Resultado,
        };

        if(i == 0)
        {
          MaiorResultado = "Maior Resultado Registrado: " + sql.Resultado + " em " + sql.Data;
          res = sql.Resultado;
        } 
        else if(Convert.ToInt32(sql.Resultado) > Convert.ToInt32(res) || sql.Resultado == "HI")
        {
          MaiorResultado = "Maior Resultado Registrado: " + sql.Resultado + " em " + sql.Data;
          res = sql.Resultado;
        }

        if(sql.Resultado == res)
        {
          MaiorResultado += ", " + sql.Data;
        }

        i++;
        mediaG += Convert.ToInt32(sql.Resultado);
        dados.Add(sql);
      }

      int dosagem = 0;
      int aux = 0;
      int j = 0;
      foreach(var item in listaS)
      {
        SugestaoView sql = new SugestaoView
        {
          Data = item.Data.ToString("dd/MM/yyyyy HH:mm"),
          Resultado = item.Resultado,
          Dosagem = item.Dosagem.ToString()
        };

        if (Convert.ToInt32(sql.Resultado) > Convert.ToInt32(res) || sql.Resultado == "HI")
        {
          MaiorResultado = "Maior Resultado Registrado: " + sql.Resultado + " em " + sql.Data ;
          res = sql.Resultado;
        }

        if (sql.Resultado == res)
        {
          MaiorResultado += ", " + sql.Data;
        }

        aux++;
        mediaG += Convert.ToInt32(sql.Resultado);
        dosagem += Convert.ToInt32(sql.Dosagem);
        dados.Add(sql);
      }

      int MediaGeral = mediaG / dados.Count;
      int MediaDosagem = dosagem / aux;

      GeraRelatorio(dados.Count, MediaGeral, dosagem, MediaDosagem, MaiorResultado);
    }

    public void GeraRelatorio(int qtd, int mediaGeral, int dosagem, int MediaDosagem, string maior)
    {
      Label lblExames = new Label();
      Label lblMediaGeral = new Label();
      Label lblDosagem = new Label();
      Label lblMediaDosagem = new Label();
      Label lblMaior= new Label();

      lblExames.Text = "Total de registros no período selecionado: " + qtd + " Registros!";

      lblMediaGeral.Text = "Média da glicemia: " + mediaGeral;

      lblDosagem.Text = "Total de medicamentos consumidos: " + dosagem + " Unidades";

      lblMediaDosagem.Text = "Média de medicamentos consumidos: " + MediaDosagem + " Unidades";

      lblMaior.Text = maior;

      StackLayout sl = new StackLayout();
      sl.Children.Add(lblExames);
      sl.Children.Add(lblMediaGeral);
      sl.Children.Add(lblDosagem);
      sl.Children.Add(lblMediaDosagem);
      sl.Children.Add(lblMaior);

      slListagem.Children.Add(sl);
    }
  }
}