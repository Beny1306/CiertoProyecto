using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinariosModel;
using Repositorios;

namespace GranjaV4.Controllers
{
    public class VeterinariosController : Controller
    {
        private IVeterinariosRepository veterinarios;
        public VeterinariosController(IVeterinariosRepository veterinariosRepo)
        {
            veterinarios = veterinariosRepo;
        }
        // GET: Veterinarios
         public async Task<ActionResult> Index()
        {
            var model = (await veterinarios.LeerTodos())
                        .Select(c =>
                        new Veterinario(){
                            RFC = c.RFC,
                            nombre = c.nombre,
                            apellidoP = c.apellidoP,
                            apellidoM = c.apellidoM,
                            especialidad = c.especialidad,
                            telefono = c.telefono
                        });
            return View(model);
        }

        // GET: Veterinarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model = await veterinarios.LeerPorId(id);
            return View(new Veterinario(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM,
                especialidad = model.especialidad,
                telefono = model.telefono
            });
        }

        // GET: Veterinarios/Create
        public ActionResult Create()
        {
            var model = new crearVeterinario();
            return View(model);
        }

        // POST: Veterinarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(crearVeterinario model)
        {
            if(ModelState.IsValid){
                try
                {
                    // TODO: genera el cattle real
                    await veterinarios.Guardar(new VeterinariosEntity(){
                    RFC = model.RFC,
                    nombre = model.nombre,
                    apellidoP = model.apellidoP,
                    apellidoM = model.apellidoM,
                    especialidad = model.especialidad,
                    telefono = model.telefono
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
            var model = await veterinarios.LeerPorId(id);
            return View(new editarVeterinario(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM,
                especialidad = model.especialidad,
                telefono = model.telefono
            });
        }

        // POST: Veterinarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(editarVeterinario model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // TODO: llenar datos
                    await veterinarios.Actualizar(new VeterinariosEntity(){
                    RFC = model.RFC,
                    nombre = model.nombre,
                    apellidoP = model.apellidoP,
                    apellidoM = model.apellidoM,
                    especialidad = model.especialidad,
                    telefono = model.telefono
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Veterinarios/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await veterinarios.LeerPorId(id);
            return View(new eliminarVeterinario(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM
            });
        }

        // POST: Veterinarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                await veterinarios.Borrar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}