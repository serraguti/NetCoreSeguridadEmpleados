﻿using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadEmpleados.Data;
using NetCoreSeguridadEmpleados.Models;
using System.Xml;

namespace NetCoreSeguridadEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public async Task<Empleado> LogInEmpleadoAsync
            (string apellido, int idEmpleado)
        {
            Empleado empleado =
                await this.context.Empleados
                .Where(z => z.Apellido == apellido
                && z.IdEmpleado == idEmpleado).FirstOrDefaultAsync();
            return empleado;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await this.context.Empleados.ToListAsync();
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados
                .FirstOrDefaultAsync(x => x.IdEmpleado == idEmpleado);
        }

        public async Task<List<Empleado>> 
            GetEmpleadosDepartamentoAsync(int idDepartamento)
        {
            return await this.context.Empleados
                .Where(x => x.Departamento == idDepartamento).ToListAsync();
        }

        public async Task 
            UpdateSalarioEmpleadosDepartamentoAsync
            (int idDepartamento, int incremento)
        {
            List<Empleado> empleados = await
                this.GetEmpleadosDepartamentoAsync(idDepartamento);
            foreach (Empleado empleado in empleados)
            {
                empleado.Salario += incremento;
            }
            await this.context.SaveChangesAsync();
        }
    }
}
