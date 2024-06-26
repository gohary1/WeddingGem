using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingGem.Dashboard.Models;

namespace WeddingGem.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles=await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name.Trim());
                if (roleExist)
                {
                    ModelState.AddModelError("Name", "this role already exist");
                    return View(model);
                }
                await _roleManager.CreateAsync(new IdentityRole() { Name = model.Name.Trim() });
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
            
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new RoleFormViewDeleteModel
            {
                id = role.Id,
                Name = role.Name
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleFormViewDeleteModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.id);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new RoleFormViewDeleteModel
            {
                id = role.Id,
                Name = role.Name
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleFormViewDeleteModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.id);
                if (role != null)
                {
                    role.Name = model.Name.Trim();
                    await _roleManager.UpdateAsync(role);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
