using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TG.Model;
using Xamarin.Forms;

namespace TG_App.DB
{
  class DataBase
  {
    // String de Conexão
    private SQLiteConnection _conexao;
    public DataBase()
    {
      var dep = DependencyService.Get<ICaminho>();
      string caminho = dep.ObterCaminho("database.sqlite");

      _conexao = new SQLiteConnection(caminho);
      // Criar tabela
      _conexao.CreateTable<Usuario>();
      _conexao.CreateTable<Horarios>();
    }

    // Métodos  Cadastro
    public void CadastrarUsuario(Usuario user)
    {
      _conexao.Insert(user);
    }
    public void CadastrarHorario(Horarios horario)
    {
      _conexao.Insert(horario);
    }
    // Métodos Update
    public void UpdateUsuario(Usuario user)
    {
      _conexao.Update(user);
    }
    public void UpdateHorario(Horarios horarios)
    {
      _conexao.Update(horarios);
    }
    // Métodos Exclusão
    public void DeleteUsuario(Usuario user)
    {
      _conexao.Delete(user);
    }
    public void DeleteUsuario(Horarios horario)
    {
      _conexao.Delete(horario);
    }
  }
}
