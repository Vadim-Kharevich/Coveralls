using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoverallsWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoreLinq;

namespace CoverallsWeb.Controllers
{
    public class HomeController : Controller
    {
        private CoverallsContext db;
        public HomeController(CoverallsContext context)
        {
            db = context;
        }

        public IActionResult Index(bool sort=false, string column=null, bool high=false, bool search=false,string searchString=null,string searchFor=null)
        {
            var storage = db.Storage.ToList();
            foreach (var st in storage)
            {
                st.Coveralls = db.Coveralls.Where(x => x.Id == st.CoverallsId).First();
                st.Coveralls.Type = db.Types.Where(x => x.Id == st.Coveralls.TypeId).First();
            }
            if (sort)
            {
                switch (column)
                {
                    case "Name":
                        if (high) storage = storage.OrderBy(x => x.Coveralls.Name).ToList();
                        else storage = storage.OrderByDescending(x => x.Coveralls.Name).ToList();
                        break;
                    case "Size":
                        if (high) storage = storage.OrderBy(x => x.Coveralls.Size).ToList();
                        else storage = storage.OrderByDescending(x => x.Coveralls.Size).ToList();
                        break;
                    case "Height":
                        if (high) storage = storage.OrderBy(x => x.Coveralls.Height).ToList();
                        else storage = storage.OrderByDescending(x => x.Coveralls.Height).ToList();
                        break;
                    case "Count":
                        if (high) storage = storage.OrderBy(x => x.Count).ToList();
                        else storage=storage.OrderByDescending(x=>x.Count).ToList();
                        break;
                }
            }
            if (search && !string.IsNullOrEmpty(searchString))
            {
                switch (searchFor)
                {
                    case "All":
                        var name = storage.FindAll(x => x.Coveralls.Name.ToLower().Contains(searchString.ToLower()));
                        var size = storage.FindAll(x => x.Coveralls.Size.ToString().Contains(searchString.ToLower()));
                        var height = storage.FindAll(x => x.Coveralls.Height.ToString().ToLower().Contains(searchString.ToLower()));
                        var count = storage.FindAll(x => x.Count.ToString().ToLower().Contains(searchString.ToLower()));
                        storage.Clear();
                        storage.AddRange(name);
                        storage.AddRange(size);
                        storage.AddRange(height);
                        storage.AddRange(count);
                        storage = storage.Distinct().ToList();
                        break;
                    case "Name":
                        storage = storage.FindAll(x => x.Coveralls.Name.ToLower().Contains(searchString.ToLower()));
                        break;
                    case "Size":
                        storage = storage.FindAll(x => x.Coveralls.Size.ToString().Contains(searchString.ToLower()));
                        break;
                    case "Height":
                        storage = storage.FindAll(x => x.Coveralls.Height.ToString().ToLower().Contains(searchString.ToLower()));
                        break;
                    case "Count":
                        storage = storage.FindAll(x => x.Count.ToString().ToLower().Contains(searchString.ToLower()));
                        break;
                }
            }
            ViewBag.High = !high;
            ViewBag.Column = column;
            ViewBag.Search = searchFor;
            if (storage.Count()==0) ViewBag.Empty = true;
            return View(storage);
        }
        public IActionResult Sort(bool sort,string column,bool high)
        {
            var storage = db.Storage.ToList();
            foreach (var st in storage)
            {
                st.Coveralls = db.Coveralls.Where(x => x.Id == st.CoverallsId).First();
                st.Coveralls.Type = db.Types.Where(x => x.Id == st.Coveralls.TypeId).First();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var storage = db.Storage.Where(x => x.Id == Id).FirstOrDefault();
            storage.Coveralls = db.Coveralls.Where(x => x.Id == storage.CoverallsId).FirstOrDefault();
            storage.Coveralls.Type = db.Types.Where(x => x.Id == storage.Coveralls.TypeId).FirstOrDefault();
            IEnumerable<CoverallsType> types = db.Types.Where(x=>x!= storage.Coveralls.Type).ToList();
            ViewBag.Type = new SelectList(types, "Id", "Name");
            return View(storage);
        }

        [HttpPost]
        public IActionResult Edit(Storage storage)
        {
            var coveralls = new Coveralls();
            if (storage.Coveralls.Type.Name == "Обувь")
            {
                storage.Coveralls.TypeId = 2;
                storage.Coveralls.Type.Id = 2;
                storage.Coveralls.Height = null;
                coveralls = storage.Coveralls;
            }
            else 
            {
                storage.Coveralls.TypeId = 1;
                storage.Coveralls.Type.Id = 1;
                coveralls = storage.Coveralls;
            }
            db.Coveralls.Update(coveralls);
            db.Storage.Update(storage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            var storage = db.Storage.Where(x => x.Id == Id).FirstOrDefault();
            var coveralls = db.Coveralls.Where(x => x.Id == storage.CoverallsId).FirstOrDefault();
            db.Storage.Remove(storage);
            db.Coveralls.Remove(coveralls);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Storage storage)
        {
            if (storage.Coveralls.Type.Name == "Обувь")
            {
                storage.Coveralls.TypeId = 2;
                storage.Coveralls.Height = null;
            }
            else
            {
                storage.Coveralls.TypeId = 1;
            }
            storage.Coveralls.Type = null;
            db.Coveralls.Add(storage.Coveralls);
            db.SaveChanges();
            var name = storage.Coveralls.Name;
            storage.Coveralls = null;
            storage.CoverallsId = db.Coveralls.OrderByDescending(x=>x.Id).Select(x => x.Id).FirstOrDefault();
            db.Storage.Add(storage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
