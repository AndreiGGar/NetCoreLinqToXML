using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToXML.Models;
using NetCoreLinqToXML.Repositories;

namespace NetCoreLinqToXML.Controllers
{
    public class JoyeriasController : Controller
    {
        private RepositoryXML repo;

        public JoyeriasController(RepositoryXML repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Joyeria> joyerias = this.repo.GetJoyerias();
            return View(joyerias);
        }
    }
}
