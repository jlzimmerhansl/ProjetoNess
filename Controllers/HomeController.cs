using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoNess.Aplicacao;
using ProjetoNess.DAO;
using ProjetoNess.Models;

namespace ProjetoNess.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PersonContext _db;
        private PessoaAplicacao pessoaAplicacao;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public HomeController()
        {
            pessoaAplicacao = new PessoaAplicacao();
        }

        public HomeController(PersonContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var lista = pessoaAplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoaAplicacao.Salvar(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        public ActionResult Editar(int id)
        {
            var pessoa = pessoaAplicacao.ListarPorId(id);

            if (pessoa == null)
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            return View(pessoa);
        }

        [HttpPost]
        public ActionResult Editar(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                pessoaAplicacao.Salvar(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        public ActionResult Detalhe(int id)
        {
            var pessoa = pessoaAplicacao.ListarPorId(id);

            if (pessoa == null)
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            return View(pessoa);
        }

        public ActionResult Excluir(int id)
        {
            var pessoa = pessoaAplicacao.ListarPorId(id);

            if (pessoa == null)
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            return View(pessoa);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExcluir(int id)
        {
            pessoaAplicacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
