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

      int mediaG = 0, i = 0;
      string res = "0", resM = "0", MaiorResultado = "", MenorResultado = "";

      int hora0 = 0, count0 = 0, hora1 = 0, count1 = 0, hora2 = 0, count2 = 0, hora3 = 0, count3 = 0, dos0 = 0, dos1 = 0, dos2 = 0, dos3 = 0;

      foreach (var item in listaE)
      {
        SugestaoView sql = new SugestaoView
        {
          Data = item.Data.ToString("dd/MM/yyyyy HH:mm"),
          DataHora = item.Data,
          Resultado = item.Resultado,
        };

        if (sql.Resultado == resM)
        {
          MenorResultado += "; " + sql.Data;
        }

        else if (sql.Resultado == "LO")
        {
          MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl em - " + sql.Data;
          resM = "LO";
        }
        else if (Convert.ToInt32(sql.Resultado) < Convert.ToInt32(resM) || i == 0)
        {
          MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl em - " + sql.Data;
          resM = sql.Resultado;
        }

        if (sql.Resultado == res)
        {
          MaiorResultado += "; " + sql.Data;
        }

        else if (sql.Resultado == "HI")
        {
          MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl em " + sql.Data;
          res = "HI";
        }

        else if (Convert.ToInt32(sql.Resultado) > Convert.ToInt32(res))
        {
          MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl em " + sql.Data;
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
          dos0 = Convert.ToInt32(sql.Dosagem);
          count0++;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
        {
          hora1 += Convert.ToInt32(sql.Resultado);
          dos1 += Convert.ToInt32(sql.Dosagem);
          count1++;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
        {
          hora2 += Convert.ToInt32(sql.Resultado);
          dos2 += Convert.ToInt32(sql.Dosagem);
          count2++;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
        {
          hora3 += Convert.ToInt32(sql.Resultado);
          dos3 += Convert.ToInt32(sql.Dosagem);
          count3++;
        }

        if (sql.Resultado == resM)
        {
          MenorResultado += "; " + sql.Data;
        }

        else if (sql.Resultado == "LO")
        {
          MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl em - " + sql.Data;
          resM = "LO";
        }
        else if (Convert.ToInt32(sql.Resultado) < Convert.ToInt32(resM) || i == 0)
        {
          MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl em - " + sql.Data;
          resM = sql.Resultado;
        }

        if (sql.Resultado == res)
        {
          MaiorResultado += "; " + sql.Data;
        }

        else if (sql.Resultado == "HI")
        {
          MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl em " + sql.Data;
          res = "HI";
        }

        else if (Convert.ToInt32(sql.Resultado) > Convert.ToInt32(res))
        {
          MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl em " + sql.Data;
          res = sql.Resultado;
        }

        aux++;
        mediaG += Convert.ToInt32(sql.Resultado);
        dosagem += Convert.ToInt32(sql.Dosagem);
        dados.Add(sql);

        lista.Add(sql);
      }


      List<RelatorioViewModel> periodos = new List<RelatorioViewModel>();
      RelatorioViewModel per = new RelatorioViewModel
      {
        h0 = count0 != 0 ? hora0 / count0 : 0,
        h1 = count1 != 0 ? hora1 / count1 : 0,
        h2 = count2 != 0 ? hora2 / count2 : 0,
        h3 = count3 != 0 ? hora3 / count3 : 0,
        TD0 = dos0,
        TD1 = dos1,
        TD2 = dos2,
        TD3 = dos3,
        MD0 = count0 != 0 ? dos0 / count0 : 0,
        MD1 = count1 != 0 ? dos1 / count1 : 0,
        MD2 = count2 != 0 ? dos2 / count2 : 0,
        MD3 = count3 != 0 ? dos3 / count3 : 0,
      };

      periodos.Add(per);

      int MediaGeral = mediaG / dados.Count;
      int MediaDosagem = dosagem / aux;

      GeraRelatorio(dados.Count, MediaGeral, dosagem, MediaDosagem, MaiorResultado, MenorResultado, periodos);
    }

    public void GeraRelatorio(int qtd, int mediaGeral, int dosagem, int MediaDosagem, string maior, string menor, List<RelatorioViewModel> periodo)
    {
      Label Titulo = new Label
      {
        TextColor = Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório de exames - " + Inicio.Text + " até " + Termino.Text
      };

      Label Titulo2 = new Label
      {
        TextColor = Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório por períodos diarios"
      };

      Label lblExames = new Label();
      Label lblMediaGeral = new Label();
      Label lblDosagem = new Label();
      Label lblMediaDosagem = new Label();
      Label lblMaior = new Label();
      Label lblPeriodo = new Label();
      Label lblMenor = new Label();

      lblExames.Text = "Total de registros: " + qtd + " Registros!";

      lblMediaGeral.Text = "Glicemia Média Estimada no Período Selecionado: " + mediaGeral + " mg / dl";

      lblDosagem.Text = "Total de Dosagens Aplicadas: " + dosagem + " Unidades";

      lblMediaDosagem.Text = "Dosagem Média Estimada: " + MediaDosagem + " Unidades";

      lblMaior.Text = maior;
      lblMenor.Text = menor;

      StackLayout sl = new StackLayout();
      sl.Children.Add(Titulo);
      sl.Children.Add(lblMediaGeral);
      sl.Children.Add(lblExames);
      sl.Children.Add(lblDosagem);
      sl.Children.Add(lblMediaDosagem);
      sl.Children.Add(lblMaior);
      sl.Children.Add(lblMenor);
      sl.Children.Add(Titulo2);

      int j = 0;
      string per = "";
      foreach (var item in periodo)
      {
        switch (j)
        {
          case 0:
            if (item.h0 != 0)
            {
              per += "Glicemia Média Estimada Entre as 00:00 e 6:00: " + item.h0 + " md / dl \n" + "Média Estimada Dosagem Aplicada - " + item.MD0 + " Unidades";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 00:00 ás 06:00\n";
            }
            break;
          case 1:
            if (item.h1 != 0)
            {
              per += "Glicemia Média Estimada Entre as 06:00 e 12:00: " + item.h1 + " md / dl \n" + "Média Estimada Dosagem Aplicada - " + item.MD1 + " Unidades";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 06:00 ás 12:00 \n";
            }
            break;
          case 2:
            if (item.h2 != 0)
            {
              per += "Glicemia Média Estimada Entre as 12:00 e 18:00: " + item.h2 + " md / dl \n" + "Média Estimada Dosagem Aplicada - " + item.MD2 + " Unidades";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 12:00 ás 18:00\n";
            }
            break;
          case 3:
            if (item.h3 != 0)
            {
              per += "Glicemia Média Estimada Entre as 18:00 e 23:59: " + item.h3 + " md / dl \n" +
                "Média de Dosagem Aplicada - " + item.MD3 + " Unidades";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 18:00 ás 23:00\n";
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