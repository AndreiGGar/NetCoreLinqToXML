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

        public IActionResult DetallePeliculaEscena(int idpelicula, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 0;
            }
            int numeroEscenas = 0;
            EscenaPelicula escena = this.repo.GetEscenaPelicula(idpelicula, posicion.Value, ref numeroEscenas);
            ViewData["DATOS"] = "Escena: " + (posicion + 1) + " de " + numeroEscenas;
            int siguiente = posicion.Value + 1;
            if (siguiente >= numeroEscenas)
            {
                siguiente = 0;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 0)
            {
                anterior = numeroEscenas - 1;
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            Pelicula peli = this.repo.FindPelicula(idpelicula);
            ViewData["PELICULA"] = peli;
            return View(escena);
        }
    }
}
