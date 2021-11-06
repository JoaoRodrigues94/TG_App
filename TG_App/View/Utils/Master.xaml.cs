using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View.Utils
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Master : MasterDetailPage
  {
    public Master()
    {
      InitializeComponent();

      DataBase DB = new DataBase();

      Notificacao.TaskScheduler.Instance.ScheduleTask(22, 35, 24,
      () =>
      {
        Debug.WriteLine("task1: " + DateTime.Now);
        CrossLocalNotifications.Current.Show("Atenção", "Hora de Registrar Seu Exame de Glicemia!");
      });
    }

    private void GoHome(object sender, EventArgs args)
    {
      Detail = new NavigationPage(new HomePage());
    }

    private void GoRelatorio(object sender, EventArgs args)
    {
      Detail = new NavigationPage(new RelatorioPage());
    }

    private void Configuracoes(object sender, EventArgs args)
    {
      Detail = new NavigationPage(new ConfiguracoesPage());
    }
  }
}