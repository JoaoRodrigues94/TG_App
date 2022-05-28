using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using TG_App.Model;
using TG_App.View.Utils;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamesListPage : ContentPage
    {
        public ExamesListPage()
        {
            InitializeComponent();

            DBExame DB = new DBExame();
            DBSugestao DB2 = new DBSugestao();
            var user = new Validacao().Listagem().SingleOrDefault();
            var lista = DB.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();
            var lista2 = DB2.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            List<SugestaoView> dados = new List<SugestaoView>();
            foreach (var item in lista)
            {
                SugestaoView x = new SugestaoView
                {
                    SugestaoID = item.ExameID,
                    Data = "DATA: " + item.Data.ToString("dd/MM/yyyy HH:mm"),
                    DataHora = item.Data,
                    Resultado = "RESULTADO DO EXAME: " + item.Resultado,
                    Dosagem = "DOZAGEM UTILIZADA: " + item.Dosagem + " Unidades",
                    TipoSugestao = "E"
                };
                dados.Add(x);
            }
            foreach (var item in lista2)
            {
                SugestaoView x = new SugestaoView
                {
                    SugestaoID = item.SugestaoID,
                    Data = item.Data.ToString("dd/MM/yyyy HH:mm"),
                    DataHora = item.Data,
                    Resultado = "RESULTADO DO EXAME: " + item.Resultado,
                    Dosagem = "SUGESTÃO DE DOSAGEM: " + item.Dosagem.ToString() + " UNIDADES",
                    TipoSugestao = "S",
                    Aplicado = "DOSAGEM UTILIZADA: " + (item.Aplicado == 0 ? item.Dosagem : item.Aplicado).ToString() + " Unidades"
                };
                dados.Add(x);
            }

            if(lista2.Count() != 0 && lista2.LastOrDefault().Data <= DateTime.Now.AddHours(2))
            {
                btnSugestao.IsVisible = false;
                lblSugestao.Text = "Para previnir um possível caso de hipoglicemia,  a ação de sugestões de dosagens somente será exibida após 2 horas de seu ultimo registro! Ultimo registro realizado em: " + lista2.LastOrDefault().Data.ToString("dd/MM/yyyy HH:mm");
                lblSugestao.IsVisible = true;
            }
            else
            {
                btnSugestao.IsVisible = true;
                lblSugestao.IsVisible = false;
            }

            var ret = dados.OrderByDescending(c => c.DataHora);
            ListaExame.ItemsSource = ret;

        }

        public void Sugestao(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new ExamesPage(), true);
        }
        public void RegistrarAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new RegistrarExamePage();
        }

        public void Pesquisar(object sender, EventArgs args)
        {
            DBExame DB = new DBExame();
            DBSugestao DB2 = new DBSugestao();
            var user = new Validacao().Listagem().SingleOrDefault();

            var listaE = DB.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();
            var listaS = DB2.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            if (!String.IsNullOrEmpty(DataSearch.Text))
            {
                int dia = Convert.ToInt32(DataSearch.Text.Substring(0, 2));
                int mes = Convert.ToInt32(DataSearch.Text.Substring(3, 2));
                int ano = Convert.ToInt32(DataSearch.Text.Substring(6, 4));

                listaE = listaE.Where(x => x.Data >= Convert.ToDateTime(mes + "/" + dia + "/" + ano + " 00:00") && x.Data <= Convert.ToDateTime(mes + "/" + dia + "/" + ano + " 23:59")).ToList();
                listaS = listaS.Where(x => x.Data >= Convert.ToDateTime(mes + "/" + dia + "/" + ano + " 00:00") && x.Data <= Convert.ToDateTime(mes + "/" + dia + "/" + ano + " 23:59")).ToList();
            }

            if (!String.IsNullOrEmpty(ResultadoSearch.Text))
            {
                listaE = listaE.Where(x => x.Resultado == ResultadoSearch.Text).ToList();
                listaS = listaS.Where(x => x.Resultado == ResultadoSearch.Text).ToList();
            }

            if (!String.IsNullOrEmpty(SugestaoSearch.Text))
            {
                listaE = new List<Exame>();
                listaS = listaS.Where(x => x.Dosagem == Convert.ToInt32(SugestaoSearch.Text)).ToList();
            }

            List<SugestaoView> dados = new List<SugestaoView>();

            foreach (var item in listaE)
            {
                SugestaoView x = new SugestaoView
                {
                    SugestaoID = item.ExameID,
                    Data = "DATA: " + item.Data.ToString("dd/MM/yyyy HH:mm"),
                    DataHora = item.Data,
                    Resultado = "RESULTADO DO EXAME: " + item.Resultado,
                    Dosagem = "DOZAGEM UTILIZADA: " + item.Dosagem + " Unidades",
                    TipoSugestao = "E"
                };
                dados.Add(x);
            }
            foreach (var item in listaS)
            {
                SugestaoView x = new SugestaoView
                {
                    SugestaoID = item.SugestaoID,
                    Data = item.Data.ToString("dd/MM/yyyy HH:mm"),
                    DataHora = item.Data,
                    Resultado = "RESULTADO DO EXAME: " + item.Resultado,
                    Dosagem = "SUGESTÃO DE DOSAGEM: " + item.Dosagem.ToString() + " UNIDADES",
                    TipoSugestao = "S",
                    Aplicado = "DOSAGEM UTILIZADA: " + (item.Aplicado == 0 ? item.Dosagem : item.Aplicado).ToString() + " Unidades"
                };
                dados.Add(x);
            }

            ListaExame.ItemsSource = dados.OrderByDescending(x => x.SugestaoID);
            DataSearch.Text = null;
            ResultadoSearch.Text = null;
            SugestaoSearch.Text = null;
        }
        public void Excluir(object sender, EventArgs args)
        {
            DBExame DB = new DBExame();
            DBSugestao DB2 = new DBSugestao();

            Button btn = (Button)sender;
            var user = new Validacao().Listagem().SingleOrDefault();

            SugestaoView lista = btn.CommandParameter as SugestaoView;

            if (lista.TipoSugestao == "E")
            {
                var busca = DB.Pesquisar().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.ExameID == lista.SugestaoID);
                DB.Delete(busca);
                App.Current.MainPage = new Master("ExameList");
            }
            if (lista.TipoSugestao == "S")
            {
                var busca = DB2.Pesquisar().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.SugestaoID == lista.SugestaoID);
                DB2.Delete(busca);
                App.Current.MainPage = new Master("ExameList");
            }
        }
        public void Details(object sender, EventArgs args)
        {
            DBExame DB = new DBExame();
            DBSugestao DB2 = new DBSugestao();
            DBSugestaoAlimento DB3 = new DBSugestaoAlimento();

            var user = new Validacao().Listagem().SingleOrDefault();
            Button btn = (Button)sender;
            SugestaoView list = btn.CommandParameter as SugestaoView;

            if (list.TipoSugestao == "E")
            {
                var dados = DB.Pesquisar().Where(x => x.ExameID == list.SugestaoID && x.UsuarioID == user.UsuarioID).SingleOrDefault();
                App.Current.MainPage = new ExameDetailPage(dados);
            }

            if (list.TipoSugestao == "S")
            {
                var dados = DB2.Pesquisar().Where(x => x.SugestaoID == list.SugestaoID && x.UsuarioID == user.UsuarioID).SingleOrDefault();

                var lista = DB3.Pesquisar().Where(x => x.SugestaoID == list.SugestaoID && x.UsuarioID == user.UsuarioID).ToList();

                App.Current.MainPage = new ExameDetailPage(dados, lista);
            }
        }
    }
}