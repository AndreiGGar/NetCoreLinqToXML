using NetCoreLinqToXML.Helpers;
using NetCoreLinqToXML.Models;
using System.Xml.Linq;

namespace NetCoreLinqToXML.Repositories
{
    public class RepositoryXML
    {
        private HelperPathProvider helper;
        private XDocument documentClientes;
        private XDocument documentPeliculas;
        private XDocument documentEscenas;
        private string pathClientes;
        private string pathPeliculas;
        private string pathEscenas;

        public RepositoryXML(HelperPathProvider helper)
        {
            this.helper = helper;
            this.pathClientes = this.helper.MapPath("ClientesID.xml", Folders.Documents);
            documentClientes = XDocument.Load(this.pathClientes);
            this.pathPeliculas = this.helper.MapPath("peliculas.xml", Folders.Documents);
            documentPeliculas = XDocument.Load(this.pathPeliculas);
            this.pathEscenas = this.helper.MapPath("escenaspeliculas.xml", Folders.Documents);
            documentEscenas = XDocument.Load(this.pathEscenas);
        }

        public List<Joyeria> GetJoyerias()
        {
            string path = helper.MapPath("joyerias.xml", Folders.Documents);
            XDocument document = XDocument.Load(path);
            List<Joyeria> joyerias = new List<Joyeria>();
            var consulta = from datos in document.Descendants("joyeria")
                           select datos;
            foreach (XElement tag in consulta)
            {
                Joyeria joyeria = new Joyeria();
                joyeria.Nombre = tag.Element("nombrejoyeria").Value;
                joyeria.CIF = tag.Attribute("cif").Value;
                joyeria.Telefono = tag.Element("telf").Value;
                joyeria.Direccion = tag.Element("direccion").Value;
                joyerias.Add(joyeria);
            }
            return joyerias;
        }

        public List<Cliente> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            var consulta = from datos in this.documentClientes.Descendants("CLIENTE")
                           select datos;
            foreach (XElement tag in consulta)
            {
                Cliente cliente = new Cliente();
                cliente.IdCliente = int.Parse(tag.Element("IDCLIENTE").Value);
                cliente.Nombre = tag.Element("NOMBRE").Value;
                cliente.Direccion = tag.Element("DIRECCION").Value;
                cliente.Email = tag.Element("EMAIL").Value;
                cliente.Imagen = tag.Element("IMAGENCLIENTE").Value;
                clientes.Add(cliente);
            }
            return clientes;
        }

        public Cliente FindCliente(int idcliente)
        {
            var consulta = from datos in this.documentClientes.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == idcliente.ToString()
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            } else
            {
                XElement tag = consulta.FirstOrDefault();
                Cliente cliente = new Cliente();
                cliente.IdCliente = int.Parse(tag.Element("IDCLIENTE").Value);
                cliente.Nombre = tag.Element("NOMBRE").Value;
                cliente.Direccion = tag.Element("DIRECCION").Value;
                cliente.Email = tag.Element("EMAIL").Value;
                cliente.Imagen = tag.Element("IMAGENCLIENTE").Value;
                return cliente;
            }
        }

        private XElement FindXMLCliente (string id)
        {
            var consulta = from datos in this.documentClientes.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void DeleteCliente(int idcliente)
        {
            XElement clienteXML = this.FindXMLCliente(idcliente.ToString());
            clienteXML.Remove();
            this.documentClientes.Save(this.pathClientes);
        }

        public void UpdateCliente(int idcliente, string nombre, string email, string direccion, string imagen)
        {
            XElement clienteXML = this.FindXMLCliente(idcliente.ToString());
            clienteXML.Element("NOMBRE").Value = nombre;
            clienteXML.Element("EMAIL").Value = email;
            clienteXML.Element("DIRECCION").Value = direccion;
            clienteXML.Element("IMAGENCLIENTE").Value = imagen;
            this.documentClientes.Save(this.pathClientes);
        }

        public void CreateCliente(int idcliente, string nombre, string email, string direccion, string imagen)
        {
            XElement rootCliente = new XElement("CLIENTE");
            rootCliente.Add(new XElement("IDCLIENTE", idcliente));
            rootCliente.Add(new XElement("NOMBRE", nombre));
            rootCliente.Add(new XElement("EMAIL", email));
            rootCliente.Add(new XElement("DIRECCION", direccion));
            rootCliente.Add(new XElement("IMAGENCLIENTE", imagen));
            this.documentClientes.Element("CLIENTES").Add(rootCliente);
            this.documentClientes.Save(this.pathClientes);
        }

        public List<Pelicula> GetPeliculas()
        {
            List<Pelicula> peliculas = new List<Pelicula>();
            var consulta = from datos in this.documentPeliculas.Descendants("pelicula")
                           select datos;
            foreach (XElement tag in consulta)
            {
                Pelicula pelicula = new Pelicula();
                pelicula.IdPelicula = int.Parse(tag.Attribute("idpelicula").Value);
                pelicula.Titulo = tag.Element("titulo").Value;
                pelicula.TituloOriginal = tag.Element("titulooriginal").Value;
                pelicula.Descripcion = tag.Element("descripcion").Value;
                pelicula.Poster = tag.Element("poster").Value;
                peliculas.Add(pelicula);
            }
            return peliculas;
        }

        public List<EscenaPelicula> GetEscenas(int idpelicula)
        {
            List<EscenaPelicula> escenas = new List<EscenaPelicula>();
            var consulta = from datos in this.documentEscenas.Descendants("escena")
                           where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                           select datos;
            foreach (XElement tag in consulta)
            {
                EscenaPelicula escena = new EscenaPelicula();
                escena.IdPelicula = int.Parse(tag.Attribute("idpelicula").Value);
                escena.Titulo = tag.Element("tituloescena").Value;
                escena.Descripcion = tag.Element("descripcion").Value;
                escena.Imagen = tag.Element("imagen").Value;
                escenas.Add(escena);
            }
            return escenas;
        }
    }
}
