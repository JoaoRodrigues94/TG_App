using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using TG_App.View.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfiguracoesPage : ContentPage
    {
        public ConfiguracoesPage()
        {
            InitializeComponent();

            DataBase DB = new DataBase();
            var user = new Validacao().Listagem().SingleOrDefault();
            var lista = DB.GetUsuarios().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            List<Usuario> lstUser = new List<Usuario>();

            foreach(var item in lista)
            {

                Nome.Text = item.Nome;
                Email.Text = item.Email;
                Celular.Text = item.Celular;
                TipoDiabete.SelectedIndex = item.TipoDiabete;
                NomeInsulinaL.Text = item.InsulinaLenta.ToString();
                UnidadesL.Text = item.UnidadesLenta.ToString();
                NomeInsulinaR.Text = item.InsulinaRapida.ToString();
                UniAlimento.Text = item.AlimentoUni.ToString();
                Carboidratos.Text = item.GramasCarbo.ToString();
                Correcao.Text = item.UnidadeCorrecao.ToString();
                GlicemiaUnd.Text = item.UnidadeGlicemia.ToString();
            }
        }

        public void Salvar(object sender, EventArgs args)
        {
            DataBase DB = new DataBase();
            var user = new Validacao().Listagem().SingleOrDefault();

            var obj = DB.GetUsuarios().Where(c => c.UsuarioID == user.UsuarioID).SingleOrDefault();
            bool salvar = true;

            obj.UsuarioID = user.UsuarioID;
            obj.Nome = Nome.Text;
            obj.Email = Email.Text;
            obj.Celular = Celular.Text;
            obj.TipoDiabete = TipoDiabete.SelectedIndex;
            obj.InsulinaLenta = NomeInsulinaL.Text;
            obj.UnidadesLenta = Convert.ToInt32(UnidadesL.Text);
            obj.InsulinaRapida = NomeInsulinaR.Text;
            obj.AlimentoUni = Convert.ToInt32(UniAlimento.Text);
            obj.GramasCarbo = Convert.ToDecimal(Carboidratos.Text);
            obj.UnidadeCorrecao = Convert.ToInt32(Correcao.Text);
            obj.UnidadeGlicemia = Convert.ToInt32(GlicemiaUnd.Text);

            if (!String.IsNullOrEmpty(Senha.Text) || !String.IsNullOrEmpty(ConfirmarSenha.Text))
            {
                if (Senha.Text == ConfirmarSenha.Text)
                    obj.Senha = Senha.Text;
                else
                {
                    DisplayAlert("Erro", "As Senhas Não Conferem!", "Ok");
                    salvar = false;
                }
            }

            if (salvar)
            {
                DB.UpdateUsuario(obj);
                DisplayAlert("Sucesso", "Dados atualizados com sucesso!", "Ok");

            }
            Master master = new Master();
            master.Configuracoes();
        }

        public void Voltar(object sender, EventArgs args)
        {
            App.Current.MainPage = new NavigationPage(new Master());
        }
    }
}