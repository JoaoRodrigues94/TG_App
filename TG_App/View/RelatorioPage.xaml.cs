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

using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.IO;

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

      int hora0 = 0, count0 = 0, hora1 = 0, count1 = 0, hora2 = 0, count2 = 0, hora3 = 0, count3 = 0, dos0 = 0, dos1 = 0, dos2 = 0, dos3 = 0, menor0 = 600, menor1 = 600, menor2 = 600, menor3 = 600, maior0 = 0, maior1 = 0, maior2 = 0, maior3 = 0;

      int dosagem = 0;
      int aux = 0;

      foreach (var item in listaE)
      {
        SugestaoView sql = new SugestaoView
        {
          Data = item.Data.ToString("dd/MM/yyyy HH:mm"),
          DataHora = item.Data,
          Hora = item.Data.ToString("MM/dd/yyyy"),
          Resultado = item.Resultado,
          Dosagem = item.Dosagem.ToString()
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
          dos0 += Convert.ToInt16(sql.Dosagem);

          // TODO - modificar a lógica implementando LO
          menor0 = Convert.ToInt32(sql.Resultado) < menor0 ? Convert.ToInt32(sql.Resultado) :  menor0;
          maior0 = Convert.ToInt32(sql.Resultado) > maior0 ? Convert.ToInt32(sql.Resultado) : maior0;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
        {
          hora1 += Convert.ToInt32(sql.Resultado);
          count1++;
          dos1 += Convert.ToInt16(sql.Dosagem);
          aux++;

          menor1 = Convert.ToInt32(sql.Resultado) < menor1 ? Convert.ToInt32(sql.Resultado) : menor1;
          maior1 = Convert.ToInt32(sql.Resultado) > maior1 ? Convert.ToInt32(sql.Resultado) : maior1;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
        {
          hora2 += Convert.ToInt32(sql.Resultado);
          count2++;
          dos2 += Convert.ToInt16(sql.Dosagem);
          aux++;

          menor2 = Convert.ToInt32(sql.Resultado) < menor2 ? Convert.ToInt32(sql.Resultado) : menor2;
          maior2 = Convert.ToInt32(sql.Resultado) > maior2 ? Convert.ToInt32(sql.Resultado) : maior2;
        }

        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
        {
          hora3 += Convert.ToInt32(sql.Resultado);
          count3++;
          dos3 += Convert.ToInt16(sql.Dosagem);
          aux++;

          menor3 = Convert.ToInt32(sql.Resultado) < menor3 ? Convert.ToInt32(sql.Resultado) : menor3;
          maior3 = Convert.ToInt32(sql.Resultado) > maior1 ? Convert.ToInt32(sql.Resultado) : maior3;
        }

        lista.Add(sql);
        i++;
        mediaG += Convert.ToInt32(sql.Resultado);
        dados.Add(sql);
      }

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
          menor0 = Convert.ToInt32(sql.Resultado) < menor0 ? Convert.ToInt32(sql.Resultado) : menor0;
          maior0 = Convert.ToInt32(sql.Resultado) > maior0 ? Convert.ToInt32(sql.Resultado) : maior0;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
        {
          hora1 += Convert.ToInt32(sql.Resultado);
          dos1 += Convert.ToInt32(sql.Dosagem);
          count1++;
          menor1 = Convert.ToInt32(sql.Resultado) < menor1 ? Convert.ToInt32(sql.Resultado) : menor1;
          maior1 = Convert.ToInt32(sql.Resultado) > maior1 ? Convert.ToInt32(sql.Resultado) : maior1;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
        {
          hora2 += Convert.ToInt32(sql.Resultado);
          dos2 += Convert.ToInt32(sql.Dosagem);
          count2++;
          menor2 = Convert.ToInt32(sql.Resultado) < menor2 ? Convert.ToInt32(sql.Resultado) : menor2;
          maior2 = Convert.ToInt32(sql.Resultado) > maior2 ? Convert.ToInt32(sql.Resultado) : maior2;
        }
        if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
        {
          hora3 += Convert.ToInt32(sql.Resultado);
          dos3 += Convert.ToInt32(sql.Dosagem);
          count3++;
          menor3 = Convert.ToInt32(sql.Resultado) < menor3 ? Convert.ToInt32(sql.Resultado) : menor3;
          maior3 = Convert.ToInt32(sql.Resultado) > maior3 ? Convert.ToInt32(sql.Resultado) : maior3;
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
        TR0 = count0,
        TR1 = count1,
        TR2 = count2,
        TR3 = count3,
        Maior0 = maior0,
        Maior1 = maior1,
        Maior2 = maior2,
        Maior3 = maior3,
        Menor0 = menor0, 
        Menor1 = menor1,
        Menor2 = menor2,
        Menor3 = menor3
      };

      int MediaGeral = mediaG / dados.Count;
      int MediaDosagem = dosagem / aux;

      GeraRelatorio(dados.Count, MediaGeral, dosagem, MediaDosagem, MaiorResultado, MenorResultado, per);
    }

    public void GeraRelatorio(int qtd, int mediaGeral, int dosagem, int MediaDosagem, string maior, string menor, RelatorioViewModel periodo)
    {
      Label Titulo = new Label
      {
        TextColor = System.Drawing.Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório de exames - " + Inicio.Text + " até " + Termino.Text
      };

      Label Titulo2 = new Label
      {
        TextColor = System.Drawing.Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório - Período 00:00 ás 06:00"
      };

      Label Titulo3 = new Label
      {
        TextColor = System.Drawing.Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório - Período 06:01 ás 12:00"
      };

      Label Titulo4 = new Label
      {
        TextColor = System.Drawing.Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório - Período 12:01 ás 18:00"
      };

      Label Titulo5 = new Label
      {
        TextColor = System.Drawing.Color.Blue,
        FontAttributes = FontAttributes.Bold,
        Text = "Relatório - Período 18:01 ás 23:59"
      };

      Label lblExames = new Label();
      Label lblMediaGeral = new Label();
      Label lblDosagem = new Label();
      Label lblMediaDosagem = new Label();
      Label lblMaior = new Label();
      Label lblPeriodo = new Label();
      Label lblPeriodo1 = new Label();
      Label lblPeriodo2 = new Label();
      Label lblPeriodo3 = new Label();
      Label lblMenor = new Label();

      lblExames.Text = "Total de registros: " + qtd + " Registros!";

      lblMediaGeral.Text = "Glicemia Média Estimada no Período Selecionado: " + mediaGeral + " mg / dl";

      lblDosagem.Text = "Total de Dosagens Aplicadas: " + dosagem + " Unidades";

      lblMediaDosagem.Text = "Dosagem Média Estimada: " + MediaDosagem + " Unidades";

      lblMaior.Text = maior;
      lblMenor.Text = menor;

      StackLayout sl = new StackLayout();
      sl.Children.Add(Titulo);
      Titulo_.Text = Titulo.Text;
      sl.Children.Add(lblMediaGeral);
      MediaGeral.Text = lblMediaGeral.Text;
      sl.Children.Add(lblExames);
      Exames.Text = lblExames.Text;
      sl.Children.Add(lblDosagem);
      Dosagem.Text = lblDosagem.Text;
      sl.Children.Add(lblMediaDosagem);
      MediaDosagem_.Text = lblMediaDosagem.Text;
      sl.Children.Add(lblMaior);
      blMaior.Text = lblMaior.Text;
      sl.Children.Add(lblMenor);
      blMenor.Text = lblMenor.Text;
      sl.Children.Add(Titulo2);
      Titulo_2.Text = Titulo2.Text;

      int j = 0;
      string per = "", per1 = "", per2 = "", per3 = "";
      for (var i = 0; i < 4; i++)
      {
        switch (j)
        {
          case 0:
            if (periodo.h0 != 0)
            {
              per += "Glicemia Média Estimada: " + periodo.h0 + " md / dl \n" + "Total de Registros - " + periodo.TR0 + "Registros\nTotal de Dosagens Aplicadas - " + periodo.TD0 + " Unidades \nDosagem Média Estimada - " + periodo.MD0 + " Unidades \nMaior Índice de Glicemia Registrado -" + periodo.Maior0+ " md / dl \n" +
                "Menor Índice de Glicemia Registrado - " + periodo.Menor0 +" md / dl";
            }
            else
            {
              per += "Nenhum Registro Encontrado das 00:00 ás 06:00\n";
            }
            break;
          case 1:
            if (periodo.h1 != 0)
            {
              per1 += "Glicemia Média Estimada: " + periodo.h1 + " md / dl \n" + "Total de Registros - " + periodo.TR1 + "Registros\nTotal de Dosagens Aplicadas - " + periodo.TD1 + " Unidades \nDosagem Média Estimada - " + periodo.MD1 + " Unidades \nMaior Índice de Glicemia Registrado -" + periodo.Maior1 + " md / dl \n" +
                "Menor Índice de Glicemia Registrado - " + periodo.Menor1 + " md / dl";
            }
            else
            {
              per1 += "Nenhum Registro Encontrado das 06:00 ás 12:00 \n";
            }
            break;
          case 2:
            if (periodo.h2 != 0)
            {
              per2 += "Glicemia Média Estimada: " + periodo.h2 + " md / dl \n" + "Total de Registros - " + periodo.TR2 + "Registros\nTotal de Dosagens Aplicadas - " + periodo.TD2 + " Unidades \nDosagem Média Estimada - " + periodo.MD0 + " Unidades \nMaior Índice de Glicemia Registrado -" + periodo.Maior2 + " md / dl \n" +
                "Menor Índice de Glicemia Registrado - " + periodo.Menor2 + " md / dl";
            }
            else
            {
              per2 += "Nenhum Registro Encontrado das 12:00 ás 18:00\n";
            }
            break;
          case 3:
            if (periodo.h3 != 0)
            {
              per3 += "Glicemia Média Estimada: " + periodo.h3 + " md / dl \n" + "Total de Registros - " + periodo.TR3 + "Registros\nTotal de Dosagens Aplicadas - " + periodo.TD3 + " Unidades \nDosagem Média Estimada - " + periodo.MD3 + " Unidades /n\nMaior Índice de Glicemia Registrado -" + periodo.Maior3 + " md / dl \n" +
                "Menor Índice de Glicemia Registrado - " + periodo.Menor3 + " md / dl";
            }
            else
            {
              per3 += "Nenhum Registro Encontrado das 18:00 ás 23:00\n";
            }
            break;
        }
        j++;
      }
      lblPeriodo.Text = per;
      lblPeriodo1.Text = per1;
      lblPeriodo2.Text = per2;
      lblPeriodo3.Text = per3;

      sl.Children.Add(lblPeriodo);
      Periodo.Text = lblPeriodo.Text;
      sl.Children.Add(Titulo3);
      Titulo_1.Text = Titulo3.Text;
      sl.Children.Add(lblPeriodo1);
      Periodo1.Text = lblPeriodo1.Text;
      sl.Children.Add(Titulo4);
      Titulo_4.Text = Titulo4.Text;
      sl.Children.Add(lblPeriodo2);
      Periodo2.Text = lblPeriodo2.Text;
      sl.Children.Add(Titulo5);
      Titulo_5.Text = Titulo5.Text;
      sl.Children.Add(lblPeriodo3);
      Periodo3.Text = lblPeriodo3.Text;

      slListagem.Children.Add(sl);
    }
    public void OnButtonClicked(object sender, EventArgs e)
    {
      string pdf = "";
      pdf += "Período: " + Inicio.Text + " até " + Termino.Text + " \n";
      pdf += Titulo_.Text + "\n";
      pdf += MediaGeral.Text + "\n";
      pdf += Exames.Text + "\n";
      pdf += Dosagem.Text + "\n";
      pdf += MediaDosagem_.Text + "\n";
      pdf += blMaior.Text + "\n";
      pdf += blMenor.Text + "\n";
      pdf += Titulo_2.Text + "\n";
      pdf += Periodo.Text + "\n";
      pdf += Titulo_1.Text + "\n";
      pdf += Periodo1.Text + "\n";
      pdf += Titulo_4.Text + "\n";
      pdf += Periodo2.Text + "\n";
      pdf += Titulo_5.Text + "\n";
      pdf += Periodo3.Text;
      // Create a new PDF document
      PdfDocument document = new PdfDocument();

      //Add a page to the document
      PdfPage page = document.Pages.Add();

      //Create PDF graphics for the page
      PdfGraphics graphics = page.Graphics;

      //Set the standard font
      PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

      //Draw the text
      graphics.DrawString(pdf, font, PdfBrushes.Black, new PointF(0, 0));

      //Save the document to the stream
      MemoryStream stream = new MemoryStream();
      document.Save(stream);

      //Close the document
      document.Close(true);

      //Save the stream as a file in the device and invoke it for viewing
      Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("RelatorioGlicemia.pdf", "application / pdf", stream);
    }
  }
}