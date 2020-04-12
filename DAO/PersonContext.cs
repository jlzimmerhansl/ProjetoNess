using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjetoNess.Models;

namespace ProjetoNess.DAO
{
    public class PersonContext : DbContext
    {
        private SqlCommand conexao;

        DbSet<Pessoa> Pessoa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1401; Database=NessDatabase;User=SA; Password=1StrongPassword!");
        }


        public int ExecutaComando(string comandoSQL, Dictionary<string, object> paramentros) {
            var resultado = 0;
            if (String.IsNullOrEmpty(comandoSQL))
            {
                throw new Exception("O comandoSQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, paramentros);
            }
            finally
            {
                FecharConexao();
            }

            return resultado;
        }

        public object ExecutaComandoComSimplesRetorno(string comandoSQL, Dictionary<string, object> parametros)
        {
            object resultado = null;
            if (String.IsNullOrEmpty(comandoSQL))
            {
                throw new Exception("O comandoSQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);

                resultado = cmdComando.GetType();
            }
            finally {

                FecharConexao();
            }

            return resultado;

        }

        public string ExecutaComandoComSimplesRetornoString(string comandoSQL, Dictionary<string, object> parametros)
        {
            var value = ExecutaComandoComSimplesRetorno(comandoSQL, parametros) as string;
            return value;
        }

        public List<Dictionary<string, string>> ExecutaComandoComRetorno(string comandoSQL, Dictionary<string, object> parametros)
        {
            List<Dictionary<string, string>> linhas = null;

            if (String.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comandoSQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                
            }
            finally
            {
                FecharConexao();
            }

            return linhas;
        }

        private object CriarComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            throw new NotImplementedException();
        }

        private static void AdicionarParamatros(SqlCommand cmdComando, Dictionary<string, object> parametros)
        {
            if (parametros == null)
                return;

            foreach (var item in parametros)
            {
                var parametro = cmdComando.CreateParameter();
                parametro.ParameterName = item.Key;
                parametro.Value = item.Value ?? DBNull.Value;
                cmdComando.Parameters.Add(parametro);
            }
        }

        /// <summary>
        /// Tenta abrir uma conexao com o servidor, por 3 vezes
        /// </summary>
        private void AbrirConexao()
        {
            _ = conexao.Connection;
            
        }

        private void FecharConexao()
        {

            conexao.Dispose();
            
        }

        

    }
}
