using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AgendaOnline.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AgendaOnline
{
    /// <summary>
    /// Summary description for AgendaOnlineWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AgendaOnlineWS : System.Web.Services.WebService {

        //Modelo de seguridad
        ApplicationDbContext context = new ApplicationDbContext();
        //Modelo de datos
        AgendaModelContainer db = new AgendaModelContainer();

        [WebMethod]
        public bool CreateUser(string Nombre, string Apellido, string Email, string Telf, string Password) {

            var ManejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();
            user.UserName = Email;
            user.Email = Email;
            string pwd = Password;
            //procedemos a agregar el usuario
            var verificar = ManejadorUsuario.Create(user, pwd);
            if (verificar.Succeeded)
            {
                //Creamos un nuevo registro de persona
                Usuario U = new Usuario();
                U.Nombre = Nombre;
                U.Apellidos = Apellido;
                U.Telf = Telf;
                U.Email = Email;
                db.Usuarios.Add(U);
                db.SaveChanges();
                return true;
            }
            else
            { 
                return false;
            }
        }

        [WebMethod]
        public int Logueo(string Email, string password)
        {
            if(Validar(Email, password))
            {
                return db.Usuarios.Where(x => x.Email == Email).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }


        [WebMethod]
        public List<GrupoWS> ListarGrupos(string Email, string Password)
        {
           if  (Validar(Email, Password))
            {
                return db.Grupos.Where(x => x.Usuario.Email == Email).Select(x => new GrupoWS()
                {
                    Id=x.Id
                    ,Nombre=x.Nombre
                    ,Color=x.Color
                }).ToList();
            }else
            {
                return null;
            }

        }

        [WebMethod]
        public List<ContactoWS> ListarContactos(string Email, string Password,int IdGroup)
        {
            if (Validar(Email, Password))
            {
                return db.Contactos.Where(x => x.GrupoId == IdGroup).Select(x => new ContactoWS()
                {
                    Id = x.Id
                    ,Nombre = x.Nombre
                    ,Apellidos = x.Apellido
                    ,Email=Email
                    ,Telf=x.Telf
                }).ToList();
            }
            else
            {
                return null;
            }

        }

        [WebMethod]
        public bool CrearGrupo(string Email, string Password, string NombreGrupo, string Color)
        {
            if (Validar(Email, Password))
            {
                Grupo G = new Grupo();
                G.UsuarioId = db.Usuarios.Where(x => x.Email == Email).FirstOrDefault().Id;
                G.Nombre = NombreGrupo;
                G.Color = Color;

                db.Grupos.Add(G);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        [WebMethod]
        public bool CrearContacto(string Email, string Password, int IdGrupo, ContactoWS C)
        {
            if (Validar(Email, Password))
            {
                Contacto Cont = new Contacto();
                Cont.GrupoId = IdGrupo;
                Cont.Nombre = C.Nombre;
                Cont.Apellido = C.Apellidos;
                Cont.Email = C.Email;
                Cont.Telf = C.Telf;

                db.Contactos.Add(Cont);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        bool  Validar(string UserName, string password)
        {
            var result = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>().PasswordSignIn(UserName, password, false, false);

            //Verificamos si fue exitoso
            if (result == SignInStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public class GrupoWS
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Color { get; set; }
        }

        public class ContactoWS
        {
            //Propiedades
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Telf { get; set; }
        }

    }
}
