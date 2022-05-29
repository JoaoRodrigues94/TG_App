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
using Plugin.LocalNotifications;
using Microcharts;
using SkiaSharp;
using TG.Model;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelatorioPage : ContentPage
    {
        public static SKColor TextColor { get; private set; }

        public RelatorioPage()
        {
            InitializeComponent();

            Inicio.Placeholder = DateTime.Now.ToString("dd/MM/yyyy");
            Termino.Placeholder = DateTime.Now.ToString("dd/MM/yyyy");
            Grafico.IsVisible = false;
        }

        public void Pesquisar(object sender, EventArgs args)
        {
            DBExame DBE = new DBExame();
            DBSugestao DBS = new DBSugestao();

            List<SugestaoView> lista = new List<SugestaoView>();

            var user = new Validacao().Listagem().SingleOrDefault();

            string start = "";
            string end = "";
            if (String.IsNullOrEmpty(Inicio.Text))
                start = Inicio.Placeholder;
            else
                start = Inicio.Text;

            if (String.IsNullOrEmpty(Termino.Text))
                end = Termino.Placeholder;
            else
                end = Termino.Text;

            int dia = Convert.ToInt32(start.Substring(0, 2));
            int mes = Convert.ToInt32(start.Substring(3, 2));
            int ano = Convert.ToInt32(start.Substring(6, 4));

            DateTime dtInicio = Convert.ToDateTime(mes + "/" + dia + "/" + ano + " 00:00:00");

            int diaTermino = Convert.ToInt32(end.Substring(0, 2));
            int mesTermino = Convert.ToInt32(end.Substring(3, 2));
            int anoTermino = Convert.ToInt32(end.Substring(6, 4));

            DateTime dtTermino = Convert.ToDateTime(mesTermino + "/" + diaTermino + "/" + anoTermino + " 23:59:59");


            var listaE = DBE.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID && c.Data >= dtInicio && c.Data <= dtTermino).ToList();
            var listaS = DBS.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID && c.Data >= dtInicio && c.Data <= dtTermino).ToList();

            List<SugestaoView> dados = new List<SugestaoView>();

            int mediaG = 0, i = 0;
            string res = "0", resM = "0", MaiorResultado = "", MenorResultado = "";

            int hora0 = 0, count0 = 0, hora1 = 0, count1 = 0, hora2 = 0, count2 = 0, hora3 = 0, count3 = 0, dos0 = 0, dos1 = 0, dos2 = 0, dos3 = 0, menor0 = 600, menor1 = 600, menor2 = 600, menor3 = 600, maior0 = 0, maior1 = 0, maior2 = 0, maior3 = 0;

            int dosagem = 0, dosInsulina = 0;
            int aux = 0, auxInsulina = 0;

            string dataRegistro = "";

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
                dosInsulina += item.Dosagem;

                string valRes = sql.Resultado == "HI" ? "600" : (sql.Resultado == "LO" ? "20" : sql.Resultado);

                if (sql.Resultado == resM)
                {
                    MenorResultado += "\n" + sql.Data;
                }

                else if (sql.Resultado == "LO")
                {
                    MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl";
                    resM = "LO";
                }
                else if (Convert.ToInt32((sql.Resultado == "HI" ? "600" : (item.Resultado == "LO" ? "20" : item.Resultado))) < Convert.ToInt32(resM) || i == 0)
                {
                    MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl";
                    resM = sql.Resultado;
                }

                if (sql.Resultado == res)
                {
                    MaiorResultado = MaiorResultado;
                }

                if (sql.Resultado == "HI")
                {
                    MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl";
                    res = "HI";
                }

                else if (Convert.ToInt32((sql.Resultado == "HI" ? "600" : (item.Resultado == "LO" ? "20" : item.Resultado))) > Convert.ToInt32(res == "HI" ? "600" : (res == "LO" ? "20" : res)))
                {
                    MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl";
                    res = sql.Resultado;
                }

                if (sql.Resultado == res)
                {
                    MaiorResultado += ", " + sql.Data;
                }

                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 00:00") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 06:00"))
                {
                    hora0 += Convert.ToInt32(valRes);
                    count0++;
                    dos0 += Convert.ToInt16(sql.Dosagem);

                    menor0 = Convert.ToInt32(valRes) < menor0 ? Convert.ToInt32(valRes) : menor0;
                    maior0 = Convert.ToInt32(valRes) > maior0 ? Convert.ToInt32(valRes) : maior0;
                }

                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
                {
                    hora1 += Convert.ToInt32(valRes);
                    count1++;
                    dos1 += Convert.ToInt16(sql.Dosagem);
                    aux++;

                    menor1 = Convert.ToInt32(valRes) < menor1 ? Convert.ToInt32(valRes) : menor1;
                    maior1 = Convert.ToInt32(valRes) > maior1 ? Convert.ToInt32(valRes) : maior1;
                }

                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
                {
                    hora2 += Convert.ToInt32(valRes);
                    count2++;
                    dos2 += Convert.ToInt16(sql.Dosagem);
                    aux++;

                    menor2 = Convert.ToInt32(valRes) < menor2 ? Convert.ToInt32(valRes) : menor2;
                    maior2 = Convert.ToInt32(valRes) > maior2 ? Convert.ToInt32(valRes) : maior2;
                }

                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
                {
                    hora3 += Convert.ToInt32(valRes);
                    count3++;
                    dos3 += Convert.ToInt16(sql.Dosagem);
                    aux++;

                    menor3 = Convert.ToInt32(valRes) < menor3 ? Convert.ToInt32(valRes) : menor3;
                    maior3 = Convert.ToInt32(valRes) > maior1 ? Convert.ToInt32(valRes) : maior3;
                }

                if (dataRegistro != sql.DataHora.ToShortDateString())
                {
                    auxInsulina++;
                    dataRegistro = sql.DataHora.ToShortDateString();
                }
                    

                lista.Add(sql);
                i++;
                mediaG += Convert.ToInt32(valRes);
                dados.Add(sql);
            }

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

                dosInsulina += item.Dosagem;

                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 00:00") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 06:00"))
                {
                    string valRes = sql.Resultado == "HI" ? "600" : (sql.Resultado == "LO" ? "20" : sql.Resultado);
                    hora0 += Convert.ToInt32(valRes);
                    dos0 = Convert.ToInt32(sql.Dosagem);
                    count0++;
                    menor0 = Convert.ToInt32(valRes) < menor0 ? Convert.ToInt32(valRes) : menor0;
                    maior0 = Convert.ToInt32(valRes) > maior0 ? Convert.ToInt32(valRes) : maior0;
                }
                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 06:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 12:00"))
                {
                    string valRes = sql.Resultado == "HI" ? "600" : (sql.Resultado == "LO" ? "20" : sql.Resultado);
                    hora1 += Convert.ToInt32(valRes);
                    dos1 += Convert.ToInt32(sql.Dosagem);
                    count1++;
                    menor1 = Convert.ToInt32(valRes) < menor1 ? Convert.ToInt32(valRes) : menor1;
                    maior1 = Convert.ToInt32(valRes) > maior1 ? Convert.ToInt32(valRes) : maior1;
                }
                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 12:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 18:00"))
                {
                    string valRes = sql.Resultado == "HI" ? "600" : (sql.Resultado == "LO" ? "20" : sql.Resultado);
                    hora2 += Convert.ToInt32(valRes);
                    dos2 += Convert.ToInt32(sql.Dosagem);
                    count2++;
                    menor2 = Convert.ToInt32(valRes) < menor2 ? Convert.ToInt32(valRes) : menor2;
                    maior2 = Convert.ToInt32(valRes) > maior2 ? Convert.ToInt32(valRes) : maior2;
                }
                if (sql.DataHora >= Convert.ToDateTime(sql.Hora + " 18:01") && sql.DataHora <= Convert.ToDateTime(sql.Hora + " 23:59"))
                {
                    string valRes = sql.Resultado == "HI" ? "600" : (sql.Resultado == "LO" ? "20" : sql.Resultado);
                    hora3 += Convert.ToInt32(valRes);
                    dos3 += Convert.ToInt32(sql.Dosagem);
                    count3++;
                    menor3 = Convert.ToInt32(valRes) < menor3 ? Convert.ToInt32(valRes) : menor3;
                    maior3 = Convert.ToInt32(valRes) > maior3 ? Convert.ToInt32(valRes) : maior3;
                }

                if (sql.Resultado == resM)
                {
                    MenorResultado = MenorResultado;
                }

                else if (sql.Resultado == "LO")
                {
                    MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl\n";
                    resM = "LO";
                }
                else if (Convert.ToInt32(sql.Resultado == "HI" ? "600" : sql.Resultado) < Convert.ToInt32(resM == "HI" ? "600" : (resM == "LO" ? "20" : resM)) || i == 0)
                {
                    MenorResultado = "Menor Índice de Glícemia Registrado: " + sql.Resultado + " mg / dl em - \n";
                    resM = sql.Resultado;
                }

                else if (sql.Resultado == "HI")
                {
                    MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl \n";
                    res = "HI";
                }

                else if (Convert.ToInt32(sql.Resultado == "LO" ? "20" : (sql.Resultado == "HI" ? "600" : sql.Resultado)) > Convert.ToInt32(res == "HI" ? "600" : (res == "LO" ? "20" : res)))
                {
                    MaiorResultado = "Maior Índice de Glicemia Registrado: " + sql.Resultado + " mg / dl";
                    res = sql.Resultado;
                }

                if (dataRegistro != sql.DataHora.ToShortDateString())
                {
                    auxInsulina++;
                    dataRegistro = sql.DataHora.ToShortDateString();
                }

                aux++;
                mediaG += Convert.ToInt32(sql.Resultado == "HI" ? "600" : (sql.Resultado == "LO" ? "20" : sql.Resultado));
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

            int MediaGeral = mediaG / (dados.Count == 0 ? 1 : dados.Count);
            int MediaDosagem = dosInsulina / (auxInsulina == 0 ? 1 : auxInsulina);

            GeraRelatorio(dados.Count, MediaGeral, dosagem, MediaDosagem, MaiorResultado, MenorResultado, per);
        }

        public void GeraRelatorio(int qtd, int mediaGeral, int dosagem, int MediaDosagem, string maior, string menor, RelatorioViewModel periodo)
        {
            Label Titulo = new Label
            {
                TextColor = System.Drawing.Color.Blue,
                FontAttributes = FontAttributes.Bold,
                Text = "Relatório de exames - " + (String.IsNullOrEmpty(Inicio.Text) ? Inicio.Placeholder : Inicio.Text) + " até " + (String.IsNullOrEmpty(Termino.Text) ? Termino.Placeholder : Termino.Text)
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
            Label lblInulinaLenta= new Label();
            Label lblMaior = new Label();
            Label lblPeriodo = new Label();
            Label lblPeriodo1 = new Label();
            Label lblPeriodo2 = new Label();
            Label lblPeriodo3 = new Label();
            Label lblMenor = new Label();

            lblExames.Text = "Total de registros: " + qtd + " Registros!";
            lblExames.TextColor = Xamarin.Forms.Color.Black;

            lblMediaGeral.Text = "Glicemia Média Estimada no Período Selecionado: " + mediaGeral + " mg / dl";
            lblMediaGeral.TextColor = Xamarin.Forms.Color.Black;

            DataBase Db = new DataBase();
            var user = new Validacao().Listagem().SingleOrDefault();
            Usuario lista = Db.GetUsuarios().Where(c => c.UsuarioID == user.UsuarioID).ToList().SingleOrDefault(); ;

            lblInulinaLenta.Text = "Unidades de Insulina Lenta: " + lista.UnidadesLenta + " Un.";
            lblInulinaLenta.TextColor = Xamarin.Forms.Color.Black;

            int media = periodo.MD0 + periodo.MD1 + periodo.MD2 + periodo.MD3;

            lblMediaDosagem.Text = "Dosagem Média Estimada: " + media + " Unidades";
            lblMediaDosagem.TextColor = Xamarin.Forms.Color.Black;

            lblMaior.Text = maior;
            lblMaior.TextColor = Xamarin.Forms.Color.Black;
            lblMenor.Text = menor;
            lblMenor.TextColor = Xamarin.Forms.Color.Black;

            StackLayout sl = new StackLayout();
            sl.Children.Clear();
            sl.Children.Add(Titulo);
            Titulo_.Text = Titulo.Text;
            sl.Children.Add(lblMediaGeral);
            MediaGeral.Text = lblMediaGeral.Text;
            sl.Children.Add(lblExames);
            Exames.Text = lblExames.Text;
            sl.Children.Add(lblInulinaLenta);
            Dosagem.Text = lblInulinaLenta.Text;
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
                            
                            per += "Glicemia Média Estimada: " + periodo.h0 + " md / dl \n" + "Total de Registros - " + periodo.TR0 + " Registros\nTotal de Dosagens Aplicadas - " + periodo.TD0 + " Unidades \nDosagem Média Estimada - " + periodo.MD0 + " Unidades \nMaior Índice de Glicemia Registrado -" + (periodo.Maior0 >= 600 ? "HI" : periodo.Maior0.ToString()) + " md / dl \n" +
                              "Menor Índice de Glicemia Registrado - " + (periodo.Menor0 <= 20 ? "LO" : periodo.Menor0.ToString()) + " md / dl";
                        }
                        else
                        {
                            per += "Nenhum Registro Encontrado das 00:00 ás 06:00\n";
                        }
                        break;
                    case 1:
                        if (periodo.h1 != 0)
                        {
                            per1 += "Glicemia Média Estimada: " + periodo.h1 + " md / dl \n" + "Total de Registros - " + periodo.TR1 + " Registros\nTotal de Dosagens Aplicadas - " + periodo.TD1 + " Unidades \nDosagem Média Estimada - " + periodo.MD1 + " Unidades \nMaior Índice de Glicemia Registrado -" + (periodo.Maior1  >= 600 ? "HI" : periodo.Maior1.ToString()) + " md / dl \n" +
                              "Menor Índice de Glicemia Registrado - " + (periodo.Menor1 <= 20 ? "LO" : periodo.Menor1.ToString()) + " md / dl";
                        }
                        else
                        {
                            per1 += "Nenhum Registro Encontrado das 06:00 ás 12:00 \n";
                        }
                        break;
                    case 2:
                        if (periodo.h2 != 0)
                        {
                            per2 += "Glicemia Média Estimada: " + periodo.h2 + " md / dl \n" + "Total de Registros - " + periodo.TR2 + " Registros\nTotal de Dosagens Aplicadas - " + periodo.TD2 + " Unidades \nDosagem Média Estimada - " + periodo.MD0 + " Unidades \nMaior Índice de Glicemia Registrado -" + (periodo.Maior2 >= 600 ? "HI" : periodo.Menor2.ToString()) + " md / dl \n" +
                              "Menor Índice de Glicemia Registrado - " + (periodo.Menor2 <= 20  ? "LO" : periodo.Menor2.ToString()) + " md / dl";
                        }
                        else
                        {
                            per2 += "Nenhum Registro Encontrado das 12:00 ás 18:00\n";
                        }
                        break;
                    case 3:
                        if (periodo.h3 != 0)
                        {
                            per3 += "Glicemia Média Estimada: " + periodo.h3 + " md / dl \n" + "Total de Registros - " + periodo.TR3 + " Registros\nTotal de Dosagens Aplicadas - " + periodo.TD3 + " Unidades \nDosagem Média Estimada - " + periodo.MD3 + " Unidades /n\nMaior Índice de Glicemia Registrado -" + (periodo.Maior3 >= 600 ? "HI" : periodo.Maior3.ToString()) + " md / dl \n" +
                              "Menor Índice de Glicemia Registrado - " + (periodo.Menor3 <= 20 ? "LO" : periodo.Menor3.ToString()) + " md / dl";
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
            lblPeriodo.TextColor = Xamarin.Forms.Color.Black;
            lblPeriodo1.Text = per1;
            lblPeriodo1.TextColor = Xamarin.Forms.Color.Black;
            lblPeriodo2.Text = per2;
            lblPeriodo2.TextColor = Xamarin.Forms.Color.Black;
            lblPeriodo3.Text = per3;
            lblPeriodo3.TextColor = Xamarin.Forms.Color.Black;

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

            List<int> lstMedias = new List<int>();
            lstMedias.Add(mediaGeral);
            lstMedias.Add(periodo.h0);
            lstMedias.Add(periodo.h1);
            lstMedias.Add(periodo.h2);
            lstMedias.Add(periodo.h3);

            List<int> lstInsulina = new List<int>();
            lstInsulina.Add((int)lista.UnidadesLenta);
            lstInsulina.Add(media);
            lstInsulina.Add(periodo.MD0);
            lstInsulina.Add(periodo.MD1);
            lstInsulina.Add(periodo.MD2);
            lstInsulina.Add(periodo.MD3);

            OnAppearing(lstMedias, lstInsulina);

            Grafico.IsVisible = true;
            slListagem.Children.Clear();
            slListagem.Children.Add(sl);
        }
        public void OnButtonClicked(object sender, EventArgs e)
        {
            string pdf = "";
            pdf += Titulo_.Text + "\n";
            pdf += MediaGeral.Text + "\n";
            pdf += Exames.Text + "\n";
            pdf += Dosagem.Text + "\n";
            pdf += MediaDosagem_.Text + "\n";
            pdf += blMaior.Text + "\n";
            pdf += blMenor.Text + "\n\n";
            pdf += Titulo_2.Text + "\n";
            pdf += Periodo.Text + "\n\n";
            pdf += Titulo_1.Text + "\n";
            pdf += Periodo1.Text + "\n\n";
            pdf += Titulo_4.Text + "\n";
            pdf += Periodo2.Text + "\n\n";
            pdf += Titulo_5.Text + "\n";
            pdf += Periodo3.Text;
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

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

        public static Chart[] CreateXamarinSample(List<int> medias, List<int> insulinas)
        {
            var entries = new[]
            {
                new ChartEntry(medias[0])
                {
                    Label = "Média Geral",
                    ValueLabel = medias[0].ToString() + " Mg/dl",
                    Color = SKColor.Parse("#E52510"),
                    TextColor = TextColor
                },
                new ChartEntry(medias[1])
                {
                    Label = "00:00 - 06:00",
                    ValueLabel = medias[1].ToString() + " Mg/dl",
                    Color = SKColor.Parse("#003791"),
                      TextColor = TextColor
                },
                new ChartEntry(medias[2])
                {
                    Label = "06:01 - 12:00",
                    ValueLabel = medias[2].ToString() + " Mg/dl",
                    Color = SKColor.Parse("#107b10"),
                      TextColor = TextColor
                },
                new ChartEntry(medias[3])
                {
                    Label = "12:01 - 18:00",
                    ValueLabel = medias[3].ToString() + " Mg/dl",
                    Color = SKColor.Parse("#f0dc82"),
                    TextColor = TextColor
                },
                new ChartEntry(medias[4])
                {
                    Label = "18:01 - 23:59",
                    ValueLabel = medias[4].ToString() + " Mg/dl",
                    Color = SKColor.Parse("#e5791d"),
                    TextColor = TextColor
                }
            };

            var insulina = new[]
            {
                new ChartEntry(insulinas[0])
                {
                    Label = "Ação Lenta",
                    ValueLabel = insulinas[0] + " Un.",
                    Color = SKColor.Parse("#E52510"),
                    TextColor = TextColor
                },
                new ChartEntry(insulinas[1])
                {
                    Label = "24h",
                    ValueLabel = insulinas[1] + " Un.",
                    Color = SKColor.Parse("#E52510"),
                    TextColor = TextColor
                },
                new ChartEntry(insulinas[2])
                {
                    Label = "00:00 - 06:00",
                    ValueLabel = insulinas[2] + " Un.",
                    Color = SKColor.Parse("#003791"),
                      TextColor = TextColor
                },
                new ChartEntry(insulinas[3])
                {
                    Label = "06:01 - 12:00",
                    ValueLabel = insulinas[3] + " Un.",
                    Color = SKColor.Parse("#107b10"),
                      TextColor = TextColor
                },
                new ChartEntry(insulinas[4])
                {
                    Label = "12:01 - 18:00",
                    ValueLabel = insulinas[4] + " Un",
                    Color = SKColor.Parse("#f0dc82"),
                    TextColor = TextColor
                },
                new ChartEntry(insulinas[5])
                {
                    Label = "18:01 - 23:59",
                    ValueLabel =  insulinas[5] + " Un",
                    Color = SKColor.Parse("#e5791d"),
                    TextColor = TextColor
                }
            };

            return new Chart[]
            {
                new BarChart()
                {
                  Entries = entries ,
                  LabelTextSize = 25,
                  LabelOrientation = Orientation.Horizontal
                },
                new LineChart()
                {
                    Entries = insulina,
                    LineMode = LineMode.Straight,
                    LineSize = 6,
                    PointMode = PointMode.Square,
                    PointSize = 15,
                    LabelTextSize = 25,
                    LabelOrientation = Orientation.Horizontal
                }
            };


        }

        protected void OnAppearing(List<int> medias, List<int> insulinas)
        {
            var charts = CreateXamarinSample(medias, insulinas);
            this.chart1.Chart = charts[0];
            this.chart2.Chart = charts[1];
        }
    }
}