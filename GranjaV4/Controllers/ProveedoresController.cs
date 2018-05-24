using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProveedoresModel;
using Repositorios;

namespace GranjaV4.Controllers
{
    public class ProveedoresController : Controller
    {
        private IProveedoresRepository proveedores;
        public ProveedoresController(IProveedoresRepository proveedoresRepo)
        {
            proveedores = proveedoresRepo;
        }
        // GET: Proveedores
        public async Task<ActionResult> Index()
        {
            var model = (await proveedores.LeerTodos())
                        .Select(c =>
                        new Proveedor(){
                            RFC = c.RFC,
                            nombre = c.nombre,
                            apellidoP = c.apellidoP,
                            apellidoM = c.apellidoM,
                            telefono = c.telefono
                        });
            return View(model);
        }

        // GET: Proveedores/Details/5
         public async Task<ActionResult> Details(string id)
        {
            var model = await proveedores.LeerPorId(id);
            return View(new Proveedor(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM,
                telefono = model.telefono
            });
        }

        // GET: Proveedores/Create
         public ActionResult Create()
        {
            var model = new crearProveedor();
            return View(model);
        }

        // POST: Proveedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(crearProveedor model)
        {
            if(ModelState.IsValid){
                try
                {
                    // TODO: genera el cattle real
                    await proveedores.Guardar(new ProveedoresEntity(){
                    RFC = model.RFC,
                    nombre = model.nombre,
                    apellidoP = model.apellidoP,
                    apellidoM = model.apellidoM,
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

        // GET: Proveedores/Edit/5
         public async Task<ActionResult> Edit(string id)
        {
            var model = await proveedores.LeerPorId(id);
            return View(new editarProveedor(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM,
                telefono = model.telefono
            });
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(editarProveedor model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // TODO: llenar datos
                    await proveedores.Actualizar(new ProveedoresEntity(){
                    RFC = model.RFC,
                    nombre = model.nombre,
                    apellidoP = model.apellidoP,
                    apellidoM = model.apellidoM,
                    telefono = model.telefono
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Proveedores/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await proveedores.LeerPorId(id);
            return View(new eliminarProveedor(){
                RFC = model.RFC,
                nombre = model.nombre,
                apellidoP = model.apellidoP,
                apellidoM = model.apellidoM
            });
        }

        // POST: Proveedores/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                await proveedores.Borrar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}