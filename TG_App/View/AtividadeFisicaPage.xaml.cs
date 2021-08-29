using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AtividadeFisicaPage : ContentPage
  {
    public AtividadeFisicaPage()
    {
      InitializeComponent();

      Data.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    private bool VerificarData()
    {
      int dia = Convert.ToInt32(Data.Text.Substring(0, 2));
      int mes = Convert.ToInt32(Data.Text.Substring(3, 2));
      int ano = Convert.ToInt32(Data.Text.Substring(6, 4));
      bool next;

      next = dia > 31 || dia < 1 ? false : true;
      next = mes > 12 || mes < 1 ? false : true;
      next = ano < 2021 ? false : true;

      return next;
    }
  }
  public class MaskedData : Behavior<Entry>
  {
    private string _mask = DateTime.Today.ToString("dd/MM/yyyy");
    public string Mask
    {
      get => _mask;
      set
      {
        _mask = value;
        SetPositions();
      }
    }

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      base.OnDetachingFrom(entry);
    }

    IDictionary<int, char> _positions;

    void SetPositions()
    {
      if (string.IsNullOrEmpty(Mask))
      {
        _positions = null;
        return;
      }

      var list = new Dictionary<int, char>();
      for (var i = 0; i < Mask.Length; i++)
        if (Mask[i] != 'X')
          list.Add(i, Mask[i]);

      _positions = list;
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      var entry = sender as Entry;

      var text = entry.Text;

      if (string.IsNullOrWhiteSpace(text) || _positions == null)
        return;

      if (text.Length > _mask.Length)
      {
        entry.Text = text.Remove(text.Length - 1);
        return;
      }

      foreach (var position in _positions)
        if (text.Length >= position.Key + 1)
        {
          var value = position.Value.ToString();
          if (text.Substring(position.Key, 1) != value)
            text = text.Insert(position.Key, value);
        }

      if (entry.Text != text)
      {
        entry.Text = text;
      }
    }
  }
}