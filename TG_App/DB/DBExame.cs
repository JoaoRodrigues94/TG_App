using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using Xamarin.Forms;

namespace TG_App.Banco
{
  class DBExame
  {
    // String de Conexão
    private SQLiteConnection _conexao;
    public DBExame()
    {
      var dep = DependencyService.Get<ICaminho>();
      string caminho = dep.ObterCaminho("diabetes.sqlite");

      _conexao = new SQLiteConnection(caminho);
      // Criar tabela
      _conexao.CreateTable<Exame>();
    }

    // Métodos  Cadastro
    public void Cadastrar(Exame exame)
    {
      _conexao.Insert(exame);
    }
    // Métodos Pesquisa
    public List<Exame> Pesquisar()
    {
      //.Where(x => x.UsuarioID == id).ToList();
      return _conexao.Table<Exame>().ToList(); 
    }
    // Métodos Update
    public void Update(Exame exame)
    {
      _conexao.Update(exame);
    }
    // Métodos Exclusão
    public void Delete(Exame exame)
    {
      _conexao.Delete(exame);
    }
  }
}
