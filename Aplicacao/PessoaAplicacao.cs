using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoNess.DAO;
using ProjetoNess.Models;

namespace ProjetoNess.Aplicacao
{
    public class PessoaAplicacao
    {
        private readonly PersonContext contexto;

        public PessoaAplicacao()
        {
            contexto = new PersonContext();
        }

        public List<Pessoa> ListarTodos()
        {
            var pessoas = new List<Pessoa>();
            const string strQuery = "SELECT Id, nome, cpf, telefone FROM Pessoa";

            var rows = contexto.ExecutaComandoComRetorno(strQuery, null);
            foreach (var row in rows)
            {
                var tempPessoa = new Pessoa
                {
                    Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
                    nome = row["nome"],
                    cpf = row["cpf"],
                    telefone = row["telefone"]
                };
                pessoas.Add(tempPessoa);
            }

            return pessoas;
        }


        private int Inserir(Pessoa pessoa)
        {
            const string commandText = " INSERT INTO Pessoa (nome, cpf, telefone) VALUES (@nome, @cpf, @telefone) ";

            var parameters = new Dictionary<string, object>
            {
                {"nome", pessoa.nome },
                { "cpf", pessoa.cpf },
                { "telefone", pessoa.telefone}
            };

            return contexto.ExecutaComando(commandText, parameters);
        }

        private int Alterar(Pessoa pessoa)
        {
            var commandText = " UPDATE Pessoa SET ";
            commandText += " nome = @nome ";
            commandText += " cpf = @cpf ";
            commandText += " telefone = @telefone ";
            commandText += " WHERE Id = @Id ";

            var parameters = new Dictionary<string, object>
            {
                {"Id", pessoa.Id},
                {"nome", pessoa.nome},
                {"cpf", pessoa.cpf},
                {"telefone", pessoa.telefone}
            };

            return contexto.ExecutaComando(commandText, parameters);
        }

        public void Salvar(Pessoa pessoa)
        {
            if (pessoa.Id > 0)
                Alterar(pessoa);
            else
                Inserir(pessoa);
        }

        public int Excluir(int id)
        {
            const string strQuery = "DELETE FROM Pessoa WHERE Id = @Id";
            var parametros = new Dictionary<string, object>
            {
                {"Id", id}
            };

            return contexto.ExecutaComando(strQuery, parametros);
        }

        public Pessoa ListarPorId(int id)
        {
            var pessoas = new List<Pessoa>();
            const string strQuery = "SELECT Id, Nome FROM Pessoa WHERE Id = @Id";
            var parametros = new Dictionary<string, object>
            {
                {"Id", id}
            };
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            foreach (var row in rows)
            {
                var tempPessoa = new Pessoa
                {
                    Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
                    nome = row["nome"],
                    cpf = row["cpf"],
                    telefone = row["telefone"]
                };
                pessoas.Add(tempPessoa);
            }

            return pessoas.FirstOrDefault();
        }
    }
}
