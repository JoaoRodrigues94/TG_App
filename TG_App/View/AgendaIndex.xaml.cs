using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.DB;
using TG_App.Model;
using TG_App.View.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaIndex : ContentPage
    {
        public AgendaIndex()
        {
            InitializeComponent();

            DBAgenda DB = new DBAgenda();
            var user = new Validacao().Listagem().SingleOrDefault();
            var dados = DB.PesquisarAgenda().Where(c => c.UsuarioID == user.UsuarioID).ToList();

            List<Agenda> lstAgenda = new List<Agenda>();
            foreach (var item in dados)
            {
                Agenda dadosAgenda = new Agenda
                {
                    AgendaID = item.AgendaID,
                    Data = item.Data + " " + item.Horario,
                    Descrição = item.Descrição,
                    Local = item.Local,
                    Observacao = item.Observacao,
                    Horario = item.Horario,
                    Status = item.Status
                };
                lstAgenda.Add(dadosAgenda);
            }

            ListaAgenda.ItemsSource = lstAgenda;
        }

        public void Agendamento(object sender, EventArgs args)
        {
            App.Current.MainPage = new Master("AgendaPage");
        }

        public void Atualizar(object sender, EventArgs args)
        {

        }

        public void Excluir(object sender, EventArgs args)
        {
            DBAgenda DB = new DBAgenda();

            Button btn = (Button)sender;
            var user = new Validacao().Listagem().SingleOrDefault();
        }
    }
}