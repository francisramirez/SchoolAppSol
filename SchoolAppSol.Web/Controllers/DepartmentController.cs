using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.ApiClient.Interfaces;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Application.Dtos.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAppSol.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentApiClient _departmentService;

        public DepartmentController(IDepartmentApiClient departmentService)
        {
            _departmentService = departmentService;
        }
        public async Task<ActionResult> Index()
        {
            ServiceResult<List<DepartmentModel>> result = new ServiceResult<List<DepartmentModel>>();

            result = await _departmentService.GetAllDepartmentsAsync();

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }


        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ServiceResult<DepartmentModel> result = new ServiceResult<DepartmentModel>();

            result = await _departmentService.GetDepartmentByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentAddDto collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }

                var result = await _departmentService.CreateDepartmentAsync(collection);

                if (!result.Success)
                {
                    ViewBag.Message = result.Message;
                    return View(collection);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ServiceResult<DepartmentModel> result = new ServiceResult<DepartmentModel>();

            result = await _departmentService.GetDepartmentByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateDepartmentDto collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }

                var result = await _departmentService.UpdateDepartmentAsync(id, collection);

                if (!result.Success)
                {
                    ViewBag.Message = result.Message;
                    return View(collection);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
