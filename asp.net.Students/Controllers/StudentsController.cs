using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace asp.net.Students.Controllers
{
    public class StudentsController : Controller
    {
        public SiteContext _context { get; set; }
        public ImageService _imageService { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentsController(SiteContext context, ImageService imageService, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
            _imageService = imageService;
        }

        [Authorize(Roles = "Student,Admin")]
        public IActionResult StudentsList()
        {
            var studentsWithPhotos = _context.Infos.Include(info => info.Photos).ToList();

            return View(studentsWithPhotos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddStudent()
        {
            ViewData["Groups"] = _context.Groups.ToList();
            ViewData["Subjects"] = _context.Subjects.ToList();
            return View(new Info());
        }
        
        [Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> AddStudent([FromForm] Info form, IFormCollection formCollection, List<int> SelectedSubjectIds)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			_context.Infos.Add(form);
			await _context.SaveChangesAsync();

			foreach (var subjectId in SelectedSubjectIds)
			{
				var subject = await _context.Subjects.FindAsync(subjectId);
				if (subject != null)
				{
					if (subject.StudentsList == null)
					{
						subject.StudentsList = new List<Info>();
					}
					subject.StudentsList.Add(form);
				}
			}

			var photos = formCollection.Files.GetFiles("Photos");
			if (photos.Any())
			{
				foreach (var photo in photos)
				{
					string imageUrl = await _imageService.SaveImageAsync(photo, form.Id);
					_context.Photos.Add(new Photo() { Url = imageUrl, InfoId = form.Id });
				}
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("StudentsList");
		}

		[Authorize(Roles = "Student,Admin,Teacher")]
        public IActionResult Show(int id)
        {
            var info = _context.Infos
                                .Include(i => i.Photos)
                                .Include(i => i.Group)
                                .Include(i => i.Subjects)
                                .FirstOrDefault(x => x.Id == id);

            List<Grade> grades = _context.Grades.Where(g => g.InfoId == id).ToList();

            double averageGrade = 0;
            int numOfGrades = 0;

            if (grades.Any())
            {
                averageGrade = Math.Round(grades.Average(g => g.Value), 2);
                numOfGrades = grades.Count();
            }

            ViewData["AvgeByAllSub"] = averageGrade;
            ViewData["NumOfGradeByAllSub"] = numOfGrades;
            ViewData["Grades"] = grades;

            return View(info);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult Edit(int id)
		{
			var info = _context.Infos.Include(i => i.Photos).Include(i => i.Group).Include(i => i.Subjects).FirstOrDefault(x => x.Id == id);

			ViewData["Groups"] = _context.Groups.ToList();
			ViewData["Subjects"] = _context.Subjects.ToList();

			return View(info);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Edit([FromForm] Info model, int id, IFormCollection formCollection)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var infoItem = _context.Infos.Include(i => i.Subjects).FirstOrDefault(x => x.Id == id);

			infoItem.Name = model.Name;
			infoItem.Surname = model.Surname;
			infoItem.BirthDay = model.BirthDay;
			infoItem.Description = model.Description;
			infoItem.IsFree = model.IsFree;
			infoItem.GroupId = model.GroupId;

			infoItem.Subjects.Clear();
			var selectedSubjectIds = formCollection["SelectedSubjectIds"];
			foreach (var subjectId in selectedSubjectIds)
			{
				int parsedId;
				if (int.TryParse(subjectId, out parsedId))
				{
					var subject = await _context.Subjects.FindAsync(parsedId);
					if (subject != null)
					{
						infoItem.Subjects.Add(subject);
					}
				}
			}

			var photos = formCollection.Files.GetFiles("Photos");
			if (photos.Any())
			{
				foreach (var photo in photos)
				{
					string imageUrl = await _imageService.SaveImageAsync(photo, id);
					_context.Photos.Add(new Photo() { Url = imageUrl, InfoId = id });
				}
			}

			await _context.SaveChangesAsync();
			return RedirectToAction("Show", new { id = id });
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
        {
            var info = _context.Infos.Include(i => i.Photos).FirstOrDefault(x => x.Id == id);

            if (info == null)
            {
                return NotFound();
            }

            string folderPath = Path.Combine("wwwroot", "images", id.ToString());
            info.Photos.Clear();

            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }

            _context.Infos.Remove(info);

            await _context.SaveChangesAsync();

            return RedirectToAction("StudentsList");
        }
		
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemovePhoto(int id, int photo)
        {
            var infoItem = _context.Infos.Include(info => info.Photos).FirstOrDefault(x => x.Id == id);

            if (infoItem == null)
            {
                return NotFound();
            }

            var photoToRemove = infoItem.Photos.FirstOrDefault(p => p.Id == photo);

            if (photoToRemove != null)
            {
                string filePath = photoToRemove.Url;
                string absoluteFilePath =
                    Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/')));

                if (System.IO.File.Exists(absoluteFilePath))
                {
                    System.IO.File.Delete(absoluteFilePath);
                }

                infoItem.Photos.Remove(photoToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = id });
        }
    }
}
