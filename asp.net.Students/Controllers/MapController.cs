using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Controllers
{
    public class MapController : Controller
    {
        public SiteContext _context { get; set; }

        public MapController(SiteContext context)
        {
            _context = context;
        }

        public IActionResult Map()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddPoint([FromBody] MapInfo marker)
        {
            _context.MapInfos.Add(marker);
            await _context.SaveChangesAsync();
            return Json(new { ok = "true", title = marker.Title });
        }

        public async Task<IActionResult> MapData()
        {
            return Json(_context.MapInfos);
        }

        [Authorize]
        public async Task<IActionResult> DeleteMarker(int id)
        {
            var markerToDelete = _context.MapInfos.FirstOrDefault(x => x.Id == id);
            if (markerToDelete != null)
            {
                _context.MapInfos.Remove(markerToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
