using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToXML.Models;
using NetCoreLinqToXML.Repositories;

namespace NetCoreLinqToXML.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryXML repo;

        public PeliculasController(RepositoryXML repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Pelicula> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }

        public IActionResult EscenasPelicula(int idpelicula)
        {
            List<EscenaPelicula> escenas = this.repo.GetEscenas(idpelicula);
            return View(escenas);
        }
    }
}
