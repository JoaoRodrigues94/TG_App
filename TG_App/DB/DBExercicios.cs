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
  class DBExercicios
  {
    // String de Conexão
    private SQLiteConnection _conexao;
    public DBExercicios()
    {
      var dep = DependencyService.Get<ICaminho>();
      string caminho = dep.ObterCaminho("diabetes.sqlite");

      _conexao = new SQLiteConnection(caminho);
      // Criar tabela
      _conexao.CreateTable<AtividadesFisicas>();
    }

    // Métodos  Cadastro
    public void CadastrarAtividade(AtividadesFisicas dados)
    {
      _conexao.Insert(dados);
    }
    // Métodos Pesquisa
    public List<AtividadesFisicas> PesquisarAtividade()
    {
      return _conexao.Table<AtividadesFisicas>().ToList(); 
    }
    // Métodos Update
    public void UpdateAtividade(AtividadesFisicas dados)
    {
      _conexao.Update(dados);
    }
    // Métodos Exclusão
    public void DeleteAlimento(AtividadesFisicas dados)
    {
      _conexao.Delete(dados);
    }
  }
}
