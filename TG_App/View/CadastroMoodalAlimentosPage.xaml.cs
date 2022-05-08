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
    public partial class CadastroMoodalAlimentosPage : ContentPage
    {
        public int UsuarioID { get; set; }

        public CadastroMoodalAlimentosPage()
        {
            InitializeComponent();
            BindingContext = new AlimentoViewModel();
        }
        public void VoltarAction(object sender, EventArgs args)
        {
            Navigation.PopModalAsync();
        }
        public void SalvarAlimentoAction(object sender, EventArgs args)
        {
            string nome = Alimento.Text;
            int medida = Medida.SelectedIndex;
            decimal porcao = Convert.ToDecimal(PorcaoAlimento.Text.Replace(",", "."));
            decimal carbo = Convert.ToDecimal(GramasCarbo.Text.Replace(",", "."));
            int categoria = Categoria.SelectedIndex;
            bool next = true;
            string message = "";
            var user = new Validacao().Listagem().SingleOrDefault();

            DBAlimento DB = new DBAlimento();

            var encontrar = DB.PesquisarAlimento().Where(x => x.NomeAlimento.ToUpper() == nome.ToUpper() && x.UsuarioID == user.UsuarioID).ToList();

            if (encontrar.Count != 0)
            {
                next = false;
                message += "Já existe um alimento cadastrado com esse nome!\n";
            }

            if (nome == null)
            {
                next = false;
                message += "Nome do alimento é um campo obrigatório!\n";
            }
            if (porcao == 0)
            {
                next = false;
                message += "Informe um valor válido para a porção!\n";
            }
            if (carbo == 0)
            {
                next = false;
                message += "informe um valor válido para a quantidade de carboidratos!";
            }
            if (next)
            {
                int id = DB.PesquisarAlimento().Count + 1;
                Food dados = new Food
                {
                    Categoria = categoria,
                    GramasCarbo = Convert.ToDecimal(carbo.ToString().Replace(",", ".")),
                    NomeAlimento = nome,
                    Medida = medida,
                    PorcaoAlimento = Convert.ToDecimal(porcao.ToString().Replace(",", ".")),
                    UsuarioID = user.UsuarioID
                };
                DB.CadastrarAlimento(dados);
                Navigation.PopModalAsync();
            }
            else
            {
                DisplayAlert("Erro", message, "OK");
            }
        }

        public void Usuario(int id)
        {
            UsuarioID = id;
        }
    }
}