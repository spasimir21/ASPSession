using BorrowLend.Data;
using BorrowLend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BorrowLend.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Item> obj = _db.items;
            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(Item item)
		{
            _db.items.Add(item);
            _db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Update(int? id)
		{
			var obj = _db.items.Find(id);

			if (obj == null) return NotFound();

			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(Item obj)
		{
			if (obj == null) return NotFound();

			_db.items.Update(obj);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}

        public IActionResult Delete(int? id)
        {
            var obj = _db.items.Find(id);

            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Item obj)
        {
            if (obj == null) return NotFound();

            _db.items.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
