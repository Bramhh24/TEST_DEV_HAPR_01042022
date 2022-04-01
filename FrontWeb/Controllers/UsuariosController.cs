using FrontWeb.Models;
using FrontWeb.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontWeb.Controllers
{
    [Authorize] // Authorize de nivel controlador
    public class UsuariosController: Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioUsuarios repositorioUsuarios;

        public UsuariosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
                                    IServicioUsuarios servicioUsuarios, IRepositorioUsuarios repositorioUsuarios)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous] // Se utiliza para poder permitir ingresar a registro, etc.
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new Usuario() 
            { 
                Nombre = modelo.Nombre,
                ApellidoPaterno = modelo.ApellidoPaterno,
                ApellidoMaterno = modelo.ApellidoMaterno,
                Email = modelo.Email 
            };

            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);

            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.Email, modelo.Password,
                                modelo.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrecto.");
                return View(modelo);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Editar()
        {
            var id = servicioUsuarios.ObtenerId();
            var datos = await repositorioUsuarios.BuscarUsuarioPorId(id);

            var modelo = new ActualizacionViewModel()
            {
                Id = datos.Id,
                Nombre = datos.Nombre,
                ApellidoPaterno = datos.ApellidoPaterno,
                ApellidoMaterno = datos.ApellidoMaterno
            };

            if(modelo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ActualizacionViewModel modelo)
        {

            var usuario = new Usuario()
            {
                Id = modelo.Id,
                Nombre = modelo.Nombre,
                ApellidoPaterno = modelo.ApellidoPaterno,
                ApellidoMaterno = modelo.ApellidoMaterno
            };

            var resultado = await userManager.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Borrar()
        {
            var id = servicioUsuarios.ObtenerId();
            var modelo = await repositorioUsuarios.BuscarUsuarioPorId(id);

            if (modelo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarUsuario(int id)
        {

            var modelo = await repositorioUsuarios.BuscarUsuarioPorId(id);

            if (modelo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var resultado = await userManager.DeleteAsync(modelo);

            if (resultado.Succeeded)
            {
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }
        }

    }
}
