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

      Notificacao.TaskScheduler.Instance.ScheduleTask(22, 35, 24,
      () =>
      {
        Debug.WriteLine("task1: " + DateTime.Now);
        CrossLocalNotifications.Current.Show("Atenção", "Hora de Registrar Seu Exame de Glicemia!");
      });

      var user = new Validacao().Listagem().SingleOrDefault();
      DataBase DB = new DataBase();

      var lista = DB.GetHorarios().Where(x => x.UsuarioID == user.UsuarioID).ToList();

      foreach(var item in lista)
      {
        Horarios h = new Horarios
        {
          Horario = item.Horario
        };

        var hora = TimeSpan.Parse(item.Horario);

        TimePicker et = new TimePicker { Format = "HH:mm", Time = hora};

        //AddHorarios.Children.Add(et);
      }
    }

    public void AddHorario(object sender, EventArgs args)
    {
      TimePicker et = new TimePicker { Format = "HH:mm"};

      //AddHorarios.Children.Add(et);
    }

    public void Salvar(object sender, EventArgs args)
    {
      DataBase DB = new DataBase();
      var user = new Validacao().Listagem().SingleOrDefault();

      var x = NovoHorario.Time.Hours;
      var y = NovoHorario.Time.Minutes;

      Notificacao.TaskScheduler.Instance.ScheduleTask(x, y, 24,
      () =>
      {
        Debug.WriteLine("task1: " + DateTime.Now);
        CrossLocalNotifications.Current.Show("Atenção", "Hora de Registrar Seu Exame de Glicemia!");
      });
    }
  }
}