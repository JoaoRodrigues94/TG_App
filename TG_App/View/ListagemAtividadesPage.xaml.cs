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
    public partial class ListagemAtividadesPage : ContentPage
    {
        public ListagemAtividadesPage()
        {
            InitializeComponent();

            var user = new Validacao().Listagem().SingleOrDefault();
            DBExercicios DB = new DBExercicios();

            var lista = DB.PesquisarAtividade().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            List<AtividadeFisicaViewModel> listagem = new List<AtividadeFisicaViewModel>();
            foreach (var item in lista)
            {
                AtividadeFisicaViewModel dados = new AtividadeFisicaViewModel
                {
                    Data = "Data de Registro: " + item.Data.ToString("dd/MM/yyyy"),
                    NomeAtividade = "Nome da Atividade: " + item.NomeAtividade.ToUpper(),
                    Fim = "Término: " + item.Fim,
                    Inicio = "Início: " + item.Inicio,
                    Observacao = "Observação: " + item.Observacao,
                    UsuarioID = user.UsuarioID,
                    AtividadeFisicaID = item.AtividadeFisicaID
                };
                listagem.Add(dados);
            }

            ListaAtividades.ItemsSource = listagem.OrderBy(c => c.NomeAtividade);
        }
        public void CadastrarAtividadeAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new AtividadeFisicaPage();
        }
        public void ExcluirAction(object sender, EventArgs args)
        {
            OnAlertYesNoClicked(sender, args);
        }
        public async void OnAlertYesNoClicked(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert("Excluir Atividade Fisica", "Deseja Realmente excluir esta atividade física?", "Sim", "Não");

            if (answer)
            {
                Button btn = (Button)sender;
                AtividadeFisicaViewModel lista = btn.CommandParameter as AtividadeFisicaViewModel;
                var user = new Validacao().Listagem().SingleOrDefault();
                DBExercicios DB = new DBExercicios();
                var encontrar = DB.PesquisarAtividade().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.AtividadeFisicaID == lista.AtividadeFisicaID);
                DB.DeleteAtividade(encontrar);
                App.Current.MainPage = new Master("AtividadesFisicas");
            }
            else
                App.Current.MainPage = new Master("AtividadesFisicas");
        }
        public void EditAction(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            AtividadeFisicaViewModel lista = btn.CommandParameter as AtividadeFisicaViewModel;

            DBExercicios DB = new DBExercicios();
            var user = new Validacao().Listagem().SingleOrDefault();
            var dados = DB.PesquisarAtividade().Where(x => x.AtividadeFisicaID == lista.AtividadeFisicaID && x.UsuarioID == user.UsuarioID).SingleOrDefault();

            App.Current.MainPage = new AtividadesEditPage(dados);
        }

        public void Pesquisar(object sender, EventArgs args)
        {
            DBExercicios DB = new DBExercicios();
            var user = new Validacao().Listagem().SingleOrDefault();

            var busca = DB.PesquisarAtividade().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            if (!String.IsNullOrEmpty(SearchAtividade.Text))
                busca = busca.Where(x => x.NomeAtividade.ToUpper().Contains(SearchAtividade.Text.ToUpper())).ToList();

            if (!String.IsNullOrEmpty(DataCadastro.Text))
            {
                int dia = Convert.ToInt32(DataCadastro.Text.Substring(0, 2));
                int mes = Convert.ToInt32(DataCadastro.Text.Substring(3, 2));
                int ano = Convert.ToInt32(DataCadastro.Text.Substring(6, 4));
                busca = busca.Where(x => x.Data == Convert.ToDateTime(mes + "/" + dia + "/" + ano)).ToList();
            }

            List<AtividadeFisicaViewModel> Lista = new List<AtividadeFisicaViewModel>();

            foreach (var item in busca)
            {
                AtividadeFisicaViewModel sql = new AtividadeFisicaViewModel
                {
                    Data = item.Data.ToString("dd/MM/yyyy"),
                    AtividadeFisicaID = item.AtividadeFisicaID,
                    Fim = item.Fim,
                    Inicio = item.Inicio,
                    NomeAtividade = item.NomeAtividade.ToUpper(),
                    Observacao = item.Observacao,
                    UsuarioID = item.UsuarioID
                };
                Lista.Add(sql);
            }


            ListaAtividades.ItemsSource = Lista.OrderBy(x => x.NomeAtividade);
            SearchAtividade.Text = null;
            DataCadastro.Text = null;
        }
    }
}