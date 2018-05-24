using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnimalesModels;
using Repositorios;

namespace GranjaV4.Controllers
{
    public class AnimalesController : Controller
    {
        private IAnimalesRepository animales;
        public AnimalesController(IAnimalesRepository animalesRepo)
        {
            animales = animalesRepo;
        }
        // GET: Animales
        public async Task<ActionResult> Index()
        {
            var model = (await animales.LeerTodos())
                        .Select(c =>
                        new Animal(){
                            Id = c.Id,
                            tipo = c.tipo,
                            nacimiento = c.nacimiento,
                            estatus = c.estatus,
                            corral = c.corral,
                            envioMatadero = c.envioMatadero
                        });
            return View(model);
        }

        // GET: Animales/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model = await animales.LeerPorId(id);
            return View(new Animal(){
                Id = model.Id,
                tipo = model.tipo,
                nacimiento = model.nacimiento,
                estatus = model.estatus,
                corral = model.corral,
                envioMatadero = model.envioMatadero
            });
        }

        // GET: Animales/Create
        public ActionResult Create()
        {
            var model = new crearAnimal();
            return View(model);
        }


        // POST: Animales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(crearAnimal model)
        {
            if(ModelState.IsValid){
                try
                {
                    // TODO: genera el cattle real
                    await animales.Guardar(new AnimalesEntity(){
                    Id = model.Id,
                    tipo = model.tipo,
                    nacimiento = model.nacimiento,
                    estatus = model.estatus,
                    corral = model.corral,
                    envioMatadero = model.envioMatadero
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

        // GET: Animales/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await animales.LeerPorId(id);
            return View(new editarAnimal(){
                Id = model.Id,
                tipo = model.tipo,
                nacimiento = model.nacimiento,
                estatus = model.estatus,
                corral = model.corral,
                envioMatadero = model.envioMatadero
            });
        }

        // POST: Animales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(editarAnimal model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // TODO: llenar datos
                    await animales.Actualizar(new AnimalesEntity(){
                    Id = model.Id,
                    tipo = model.tipo,
                    nacimiento = model.nacimiento,
                    estatus = model.estatus,
                    corral = model.corral,
                    envioMatadero = model.envioMatadero
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Animales/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await animales.LeerPorId(id);
            return View(new eliminarAnimal(){
                Id = model.Id
            });
        }

        // POST: Animales/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                await animales.Borrar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}