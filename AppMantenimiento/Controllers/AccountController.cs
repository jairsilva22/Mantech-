using AppMantenimiento.Models;
using AppMantenimiento.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ClientModel.Primitives;
using System.ComponentModel.Design;

namespace AppMantenimiento.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAccountRepositorio accountRepositorio;
       

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAccountRepositorio accountRepositorio)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountRepositorio = accountRepositorio;
           
        }

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
            var user = await userManager.FindByEmailAsync(modelo.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "El correo no está registrado.");
                return View(modelo);
            }

            var esPasswordValida = await userManager.CheckPasswordAsync(user, modelo.Password);
            if (!esPasswordValida)
            {
                ModelState.AddModelError(string.Empty, "La contraseña es incorrecta.");
                return View(modelo);
            }
            var resultado = await signInManager.PasswordSignInAsync(user.UserName, modelo.Password, modelo.RememberMe, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                return View(modelo);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }



      
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Registro()
        {
            var modelo = new RegistroUsuarioViewModel();

            // Cargar los roles disponibles
            modelo.Roles = await accountRepositorio.ObtenerRoles();

            return View(modelo);
        }


        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Registro(RegistroUsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                // IMPORTANTE: Recargar los roles si hay errores
                modelo.Roles = await accountRepositorio.ObtenerRoles();
                return View(modelo);
            }

            if (modelo.UserName != modelo.Email)
            {
                ModelState.AddModelError("UserName", "Los correos deben coincidir.");
                // Recargar roles antes de devolver la vista
                modelo.Roles = await accountRepositorio.ObtenerRoles();
                return View(modelo);
            }
          
            var usuario = new IdentityUser
            {
                UserName = modelo.Email,
                Email = modelo.Email,
                PhoneNumber = modelo.PhoneNumber
            };

            var resultado = await userManager.CreateAsync(usuario, modelo.Password);

            if (resultado.Succeeded)
            {
                // Asignar el rol seleccionado al usuario
                if (!string.IsNullOrEmpty(modelo.RolSeleccionado))
                {
                    var rolResult = await userManager.AddToRoleAsync(usuario, modelo.RolSeleccionado);
                    if (!rolResult.Succeeded)
                    {
                        // Manejar error de asignación de rol
                        foreach (var error in rolResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, $"Error asignando rol: {error.Description}");
                        }
                        modelo.Roles = await accountRepositorio.ObtenerRoles();
                        return View(modelo);
                    }
                }

                return RedirectToAction("Index", routeValues : new {mensaje = "Usuario creado correctamente"});
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // Recargar roles antes de devolver la vista
                modelo.Roles = await accountRepositorio.ObtenerRoles();
                return View(modelo);
            }
        }



        [HttpGet]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Index(string mensaje = null)
        {

            var resultado = await accountRepositorio.ObtenerUsuariosAsync();

            return View(resultado);
        }


        [HttpGet]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Eliminar(UsuarioViewModel model)
        {
            model = await accountRepositorio.ObtenerUsuarioPorIdAsync(model.Id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            var usuario = await userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var resultado = await userManager.DeleteAsync(usuario);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", new { mensaje = "Usuario eliminado correctamente" });
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Error");
        }




        [HttpGet]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Editar(string id)
        {
            var usuario = await userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var roles = await accountRepositorio.ObtenerRoles();
            var rolActual = (await userManager.GetRolesAsync(usuario)).FirstOrDefault();

            var modelo = new EditarUsuarioViewModel
            {
                Id = usuario.Id,
                Email = usuario.Email,
                PhoneNumber = usuario.PhoneNumber,
                RolSeleccionado = rolActual,
                Roles = roles
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Editar(EditarUsuarioViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                modelo.Roles = await accountRepositorio.ObtenerRoles();
                return View(modelo);
            }

            var usuario = await userManager.FindByIdAsync(modelo.Id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Actualizar email y teléfono
            usuario.Email = modelo.Email;
            usuario.UserName = modelo.Email;
            usuario.PhoneNumber = modelo.PhoneNumber;

            var resultado = await userManager.UpdateAsync(usuario);
            if (!resultado.Succeeded)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                modelo.Roles = await accountRepositorio.ObtenerRoles();
                return View(modelo);
            }

            // Actualizar rol (sólo si cambió)
            var rolesActuales = await userManager.GetRolesAsync(usuario);
            var rolActual = rolesActuales.FirstOrDefault();

            if (rolActual != modelo.RolSeleccionado)
            {
                // Quitar el rol anterior (si existe) y asignar el nuevo
                if (!string.IsNullOrEmpty(rolActual))
                {
                    await userManager.RemoveFromRoleAsync(usuario, rolActual);
                }

                await userManager.AddToRoleAsync(usuario, modelo.RolSeleccionado);
            }

            TempData["Mensaje"] = "Usuario actualizado correctamente.";
            return RedirectToAction("Index", "Account");
        }


    }
}
