using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Plugin.Calendar.Models;
using TG_App.Model;
using System.Globalization;

namespace TG_App.ViewModel
{
  public class AtividadeFisicaViewModel : INotifyPropertyChanged
  {
    public int AtividadeFisicaID { get; set; }
    public int UsuarioID { get; set; }
    public string NomeAtividade { get; set; }
    public string Data { get; set; }
    public string Inicio { get; set; }
    public string Fim { get; set; }
    public string Observacao { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
