using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpleadosModels;
using Repositorios;

namespace GranjaV4.Controllers
{
    public class EmpleadosController : Controller
    {
        private IEmpleadosRepository empleados;
        public EmpleadosController(IEmpleadosRepository empleadosRepo)
        {
            empleados = empleadosRepo;
        }
        // GET: Empleados
        public async Task<ActionResult> Index()
        {
            var model = (await empleados.LeerTodos())
                        .Select(c =>
                        new Empleado(){
                            RFC = c.RFC,
                            nombre = c.nombre,
                            apellidoP = c.apellidoP,
                            apellidoM = c.apellidoM,
                            nacimiento = c.nacimiento,
                            tipo = c.tipo,
                            telefono = c.telefono,
                            sueldo = c.sueldo,
                            horario = c.horario
                        });
            return View(model);
        }

        // GET: Empleados/Details/5
         public async Task<ActionResult> Details(string id)
        {
            var model = await empleados.LeerPorId(id);
            return View(new Empleado(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM,
                nacimiento = model.nacimiento,
                tipo = model.tipo,
                telefono = model.telefono,
                sueldo = model.sueldo,
                horario = model.horario
            });
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            var model = new crearEmpleado();
            return View(model);
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(crearEmpleado model)
        {
            if(ModelState.IsValid){
                try
                {
                    // TODO: genera el cattle real
                    await empleados.Guardar(new EmpleadosEntity(){
                    RFC = model.RFC,
                    nombre = model.nombre,
                    apellidoP = model.apellidoP,
                    apellidoM = model.apellidoM,
                    nacimiento = model.nacimiento,
                    tipo = model.tipo,
                    telefono = model.telefono,
                    sueldo = model.sueldo,
                    horario = model.horario
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            }
            return View();
        }

        // GET: Veterinarios/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await empleados.LeerPorId(id);
            return View(new editarEmpleado(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM,
                nacimiento = model.nacimiento,
                tipo = model.tipo,
                telefono = model.telefono,
                sueldo = model.sueldo,
                horario = model.horario
            });
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(editarEmpleado model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // TODO: llenar datos
                    await empleados.Actualizar(new EmpleadosEntity(){
                    RFC = model.RFC,
                    nombre = model.nombre,
                    apellidoP = model.apellidoP,
                    apellidoM = model.apellidoM,
                    nacimiento = model.nacimiento,
                    tipo = model.tipo,
                    telefono = model.telefono,
                    sueldo = model.sueldo,
                    horario = model.horario
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Empleados/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await empleados.LeerPorId(id);
            return View(new eliminarEmpleado(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM
            });
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                await empleados.Borrar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}