using AppMantenimiento.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AppMantenimiento.Servicios
{
    public interface IAccountRepositorio
    {
        
        Task<List<SelectListItem>> ObtenerRoles();
        Task<UsuarioViewModel> ObtenerUsuarioPorIdAsync(string id);
        Task<UsuarioListadoViewModel> ObtenerUsuariosAsync();
    }
    public class AccountRepositorio : IAccountRepositorio
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AccountRepositorio(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        //  traar los roles de los usuarios y sus datos básicos

        public async Task<List<SelectListItem>> ObtenerRoles()
        {
            var resultado = await applicationDbContext.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .ToListAsync();

            return resultado; 
        }


        public async Task<UsuarioListadoViewModel> ObtenerUsuariosAsync()
        {
            var usuarios = await applicationDbContext.Users
                .Select(u => new UsuarioViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Rol = applicationDbContext.UserRoles
                        .Where(ur => ur.UserId == u.Id)
                        .Select(ur => applicationDbContext.Roles.FirstOrDefault(r => r.Id == ur.RoleId).Name)
                        .FirstOrDefault()
                })

                .ToListAsync();
            return new UsuarioListadoViewModel
            {
                Usuarios = usuarios,
                Mensaje = ""
            };
        }

        public async Task<UsuarioViewModel> ObtenerUsuarioPorIdAsync(string id)
        {
            var usuario = await applicationDbContext.Users
                .Where(u => u.Id == id)
                .Select(u => new UsuarioViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Rol = applicationDbContext.UserRoles
                        .Where(ur => ur.UserId == u.Id)
                        .Select(ur => applicationDbContext.Roles.FirstOrDefault(r => r.Id == ur.RoleId).Name)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();
            return usuario;
        }




    }

}
