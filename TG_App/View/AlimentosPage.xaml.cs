using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using TG_App.View.Utils;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlimentosPage : ContentPage
    {
        List<ListaAlimentosViewModel> lista { get; set; }
        public AlimentosPage()
        {
            InitializeComponent();
            DBAlimento DB = new DBAlimento();

            var user = new Validacao().Listagem().SingleOrDefault();

            List<ListaAlimentosViewModel> dadosLista = new List<ListaAlimentosViewModel>();

            var cadastrados = DB.PesquisarAlimento().Where(x => x.UsuarioID == user.UsuarioID).ToList();
            foreach (var item in cadastrados)
            {
                var x = item.UsuarioID;
                ListaAlimentosViewModel dados = new ListaAlimentosViewModel
                {
                    AlimentoID = Convert.ToInt32(item.AlimentoID),
                    Alimento = "Alimento: " + Convert.ToString(item.NomeAlimento).ToUpper(),
                    Carboidratos = "Carboidratos: " + Convert.ToString(item.GramasCarbo) + " gramas",
                    Categoria = "Categoria: " + Categoria(item.Categoria),
                    UsuarioID = item.UsuarioID,
                    Medida = "Medida: " + Convert.ToString(Medidas(item.Medida))
                };
                dadosLista.Add(dados);
            }


            ListaAlimentos.ItemsSource = dadosLista.OrderBy(x => x.Alimento);
            lista = dadosLista;
        }

        public void ExcluirAction(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            ListaAlimentosViewModel lista = btn.CommandParameter as ListaAlimentosViewModel;
            var user = new Validacao().Listagem().SingleOrDefault();
            DBAlimento DB = new DBAlimento();
            var encontrar = DB.PesquisarAlimento().SingleOrDefault(c => c.UsuarioID == user.UsuarioID && c.AlimentoID == lista.AlimentoID);
            DB.DeleteAlimento(encontrar);
            App.Current.MainPage = new Master("AlimentosPage");
        }

        public void EditarAction(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            ListaAlimentosViewModel lista = btn.CommandParameter as ListaAlimentosViewModel;

            DBAlimento DB = new DBAlimento();
            var user = new Validacao().Listagem().SingleOrDefault();
            var dados = DB.PesquisarAlimento().SingleOrDefault(c => c.UsuarioID == user.UsuarioID && c.AlimentoID == lista.AlimentoID);

            App.Current.MainPage = new AlimentosEditPage(dados);
        }
        public void PesquisaAction(object sender, EventArgs args)
        {
            var user = new Validacao().Listagem().SingleOrDefault();
            var busca = lista.Where(c => c.UsuarioID == user.UsuarioID);

            if (!String.IsNullOrEmpty(Convert.ToString(SearchAlimento.Text)))
                busca = (List<ListaAlimentosViewModel>)busca.Where(x => x.Alimento.ToUpper().Contains(SearchAlimento.Text.ToUpper().ToUpper())).ToList();

            if (!String.IsNullOrEmpty(Convert.ToString(FiltroCategoria.SelectedItem)))
                busca = (List<ListaAlimentosViewModel>)busca.Where(x => x.Categoria.Contains(FiltroCategoria.SelectedItem.ToString())).ToList();

            if (!String.IsNullOrEmpty(Convert.ToString(Medida.SelectedItem)))
                busca = (List<ListaAlimentosViewModel>)busca.Where(x => x.Medida.Contains(Medida.SelectedItem.ToString())).ToList();

            ListaAlimentos.ItemsSource = busca.OrderBy(x => x.Alimento);
            SearchAlimento.Text = null;
            FiltroCategoria.SelectedIndex = -1;
            Medida.SelectedIndex = -1;
        }

        public static string Categoria(int categoria)
        {
            string nome = "";
            if (categoria == 0) nome = "Cereais, Pães e Tubérculos";
            else if (categoria == 1) nome = "Hortaliças";
            else if (categoria == 2) nome = "Frutas";
            else if (categoria == 3) nome = "Leguminosas";
            else if (categoria == 4) nome = "Carnes e Ovos";
            else if (categoria == 5) nome = "Leite e Derivados";
            else if (categoria == 6) nome = "Óleos e Gorduras";
            else nome = "Açúcares";

            return nome;
        }
        public static string Medidas(int medida)
        {
            string nome = "";
            if (medida == 0) nome = "Unidade";
            else if (medida == 1) nome = "Gramas";
            else nome = "ml";

            return nome;
        }
        public void CadastrarAlimentoAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new CadastrarAlimentoPage();
        }
    }
}