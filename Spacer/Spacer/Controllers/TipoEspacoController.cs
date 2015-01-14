using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spacer.Models;

namespace Spacer.Controllers
{
    public class TipoEspacoController : Controller
    {
        private ContextoDB db = new ContextoDB();

        [Authorize(Roles = "Admin, VisualizacaoTipoEspaco, CadastroTipoEspaco")]
        public ActionResult Index()
        {
            var model = db.TipoEspaco.ToList();

            return View(model);
        }
    }
}