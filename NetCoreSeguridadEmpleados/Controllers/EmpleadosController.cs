using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadEmpleados.Filters;
using NetCoreSeguridadEmpleados.Models;
using NetCoreSeguridadEmpleados.Repositories;
using System.Security.Claims;

namespace NetCoreSeguridadEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        [AuthorizeEmpleados]
        public IActionResult PerfilEmpleado()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        //[AuthorizeEmpleados]
        //public async Task<IActionResult> Details(int idempleado)
        //{
        //    Empleado empleado = await this.repo.FindEmpleadoAsync(idempleado);
        //    return View(empleado);
        //}

        [AuthorizeEmpleados]
        public async Task<IActionResult> Details(int id)
        {
            Empleado empleado = await this.repo.FindEmpleadoAsync(id);
            return View(empleado);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Compis()
        {
            //RECUPERAMOS EL DATO DEL DEPARTAMENTO DEL CLAIM
            string dato =
                HttpContext.User.FindFirst("Departamento").Value;
            int idDepartamento = int.Parse(dato);
            List<Empleado> empleados = await
                this.repo.GetEmpleadosDepartamentoAsync(idDepartamento);
            return View(empleados);
        }

        [AuthorizeEmpleados]
        [HttpPost]
        public async Task<IActionResult> Compis(int incremento)
        {
            string dato =
                HttpContext.User.FindFirst("Departamento").Value;
            int idDepartamento = int.Parse(dato);
            await this.repo.UpdateSalarioEmpleadosDepartamentoAsync
                (idDepartamento, incremento);
            List<Empleado> empleados = await
                this.repo.GetEmpleadosDepartamentoAsync(idDepartamento);
            return View(empleados);
        }
    }
}
