using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TG.Model
{
  public class Horarios : INotifyPropertyChanged
  {
    public int HorarioID { get; set; }
    public int Pickers { get; set; }
    public int Unidades { get; set; }
    public DateTime Horario { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChange(string param)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(param));
      }
    }
  }
}
