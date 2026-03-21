using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Application.Services.Course;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: CourseController
        public async Task<ActionResult> Index()
        {
            ServiceResult<List<CourseModel>> result = new ServiceResult<List<CourseModel>>();

            result = await _courseService.GetAllCoursesAsync();

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ServiceResult<CourseModel> result = new ServiceResult<CourseModel>();

            result = await _courseService.GetCourseByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CourseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ServiceResult<CourseModel> result = new ServiceResult<CourseModel>();

            result = await _courseService.GetCourseByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
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
