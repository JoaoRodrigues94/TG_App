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

      List<SugestaoView> lista = new List<SugestaoView>();

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

      int mediaG = 0;
      int i = 0;
      string res = "";

      string MaiorResultado = "";


      int hora0 = 0, count0 = 0, hora1 = 0, count1 = 0, hora2 = 0, count2 = 0, hora3 = 0, count3 = 0;

      foreach (var item in listaE)
      {
        SugestaoView sql = new SugestaoView
        {
          Data = item.Data.ToString("dd/MM/yyyyy HH:mm"),
          DataHora = item.Data,
          Resultado = item.Resultado,
        };

        if (i == 0)
        {
          MaiorResultado = "Maior Resultado Registrado: " + sql.Resultado + " em " + sql.Data;
          res = sql.Resultado;
        }
        else if (Convert.ToInt32(sql.Resultado) > Convert.ToInt32(res) || sql.Resultado == "HI")
        {
          MaiorResultado = "Maior Resultado Registrado: " + sql.Resultado + " em " + sql.Data;
          res = sql.Resultado;
        }

        if (sql.Resultado == res)
        {
          MaiorResultado += ", " + sql.Data;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 00:00") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 06:00"))
        {
          hora0 += Convert.ToInt32(sql.Resultado);
          count0++;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
        {
          hora1 += Convert.ToInt32(sql.Resultado);
          count1++;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
        {
          hora2 += Convert.ToInt32(sql.Resultado);
          count2++;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
        {
          hora3 += Convert.ToInt32(sql.Resultado);
          count3++;
        }

        lista.Add(sql);
        i++;
        mediaG += Convert.ToInt32(sql.Resultado);
        dados.Add(sql);
      }

      int dosagem = 0;
      int aux = 0;
      int j = 0;

      foreach (var item in listaS)
      {
        SugestaoView sql = new SugestaoView
        {
          Data = item.Data.ToString("dd/MM/yyyy HH:mm"),
          Hora = item.Data.ToString("MM/dd/yyyy"),
          DataHora = item.Data,
          Resultado = item.Resultado,
          Dosagem = item.Dosagem.ToString()
        };

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 00:00") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 06:00"))
        {
          hora0 += Convert.ToInt32(sql.Resultado);
          count0++;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
        {
          hora1 += Convert.ToInt32(sql.Resultado);
          count1++;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
        {
          hora2 += Convert.ToInt32(sql.Resultado);
          count2++;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
        {
          hora3 += Convert.ToInt32(sql.Resultado);
          count3++;
        }

        if (Convert.ToInt32(sql.Resultado) > Convert.ToInt32(res) || sql.Resultado == "HI")
        {
          MaiorResultado = "Maior Resultado Registrado: " + sql.Resultado + " em " + sql.Data;
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

        lista.Add(sql);
      }


      List<int> periodos = new List<int>();
      periodos.Add(count0 != 0 ? hora0 / count0 : 0);
      periodos.Add(count1 != 0 ? hora1 / count1 : 0);
      periodos.Add(count2 != 0 ? hora2 / count2 : 0);
      periodos.Add(count3 != 0 ? hora3 / count3 : 0);

      int MediaGeral = mediaG / dados.Count;
      int MediaDosagem = dosagem / aux;

      GeraRelatorio(dados.Count, MediaGeral, dosagem, MediaDosagem, MaiorResultado, periodos);
    }

    public void GeraRelatorio(int qtd, int mediaGeral, int dosagem, int MediaDosagem, string maior, List<int> periodo)
    {
      Label lblExames = new Label();
      Label lblMediaGeral = new Label();
      Label lblDosagem = new Label();
      Label lblMediaDosagem = new Label();
      Label lblMaior = new Label();
      Label lblPeriodo = new Label();

      lblExames.Text = "Total de registros: " + qtd + " Registros!";

      lblMediaGeral.Text = "Glicemia Média Estimada no Período Selecionado: " + mediaGeral + " mg / dl";

      lblDosagem.Text = "Dosagem Estimada: " + dosagem + " Unidades";

      lblMediaDosagem.Text = "Dosagem Média Estimada: " + MediaDosagem + " Unidades";

      lblMaior.Text = maior;

      StackLayout sl = new StackLayout();
      sl.Children.Add(lblMediaGeral);
      sl.Children.Add(lblExames);
      sl.Children.Add(lblDosagem);
      sl.Children.Add(lblMediaDosagem);
      sl.Children.Add(lblMaior);

      int j = 0;
      string per = "";
      foreach (var item in periodo)
      {
        switch (j)
        {
          case 0:
            if (item != 0)
            {
              per += "Glicemia Média Estimada Entre as 00:00 e 6:00: " + item + " md / dl \n";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 00:00 ás 06:00";
            }
            break;
          case 1:
            if (item != 0)
            {
              per += "Glicemia Média Estimada Entre as 06:00 e 12:00: " + item + " md / dl \n";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 06:00 ás 12:00";
            }
            break;
          case 2:
            if (item != 0)
            {
              per += "Glicemia Média Estimada Entre as 12:00 e 18:00: " + item + " md / dl \n";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 12:00 ás 18:00";
            }
            break;
          case 3:
            if (item != 0)
            {
              per = "Glicemia Média Estimada Entre as 18:00 e 23:59: " + item + " md / dl \n";
            }
            else
            {
              per = "Nenhum Registro Encontrado das 18:00 ás 23:00";
            }
            break;
        }
        j++;
      }
      lblPeriodo.Text = per;
      sl.Children.Add(lblPeriodo);

      slListagem.Children.Add(sl);
    }
  }
}