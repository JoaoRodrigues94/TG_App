using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TG_App.Model;
using TG_App.Notificacao;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NotificacaoPage : ContentPage
  {
    public NotificacaoPage()
    {
      InitializeComponent();

      Notificacao.TaskScheduler.Instance.ScheduleTask(20, 46, 24,
      () =>
      {
        Debug.WriteLine("task1: " + DateTime.Now);
        CrossLocalNotifications.Current.Show("Atenção", "Hora de Registrar Seu Exame de Glicemia!");
      });
    }
  }
}