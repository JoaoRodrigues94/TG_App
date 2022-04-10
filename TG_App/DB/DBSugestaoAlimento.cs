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
    class DBSugestaoAlimento
    {
        // String de Conexão
        private SQLiteConnection _conexao;
        public DBSugestaoAlimento()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.ObterCaminho("diabetes.sqlite");

            _conexao = new SQLiteConnection(caminho);
            // Criar tabela
            _conexao.CreateTable<SugestaoAlimento>();
        }

        // Métodos  Cadastro
        public void Cadastrar(SugestaoAlimento sugestao)
        {
            _conexao.Insert(sugestao);
        }
        // Métodos Pesquisa
        public List<SugestaoAlimento> Pesquisar()
        {
            //.Where(x => x.UsuarioID == id).ToList();
            return _conexao.Table<SugestaoAlimento>().ToList();
        }
        // Métodos Update
        public void Update(SugestaoAlimento sugestao)
        {
            _conexao.Update(sugestao);
        }
        // Métodos Exclusão
        public void Delete(SugestaoAlimento sugestao)
        {
            _conexao.Delete(sugestao);
        }
    }
}
