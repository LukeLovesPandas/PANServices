using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PANServices.Models;
using System.Linq;


namespace PANServices.Controllers
{
    [Route("api/[controller]")]
    public class PanimalController : Controller
    {
        private readonly PanimalContext _context;

        public PanimalController(PanimalContext context)
        {
            _context = context;

            if (_context.PanimalItems.Count() == 0)
            {
                _context.PanimalItems.Add(new Panimal { Name = "Panther", URL = "https://i.ytimg.com/vi/x8V-i1YoFbY/maxresdefault.jpg" });
                _context.PanimalItems.Add(new Panimal {  Name = "Panda", URL = "https://www.thelocal.de/userdata/images/article/fa6fd5014ccbd8f4392f716473ab6ff354f871505d9128820bbb0461cce1d645.jpg"});
                _context.PanimalItems.Add(new Panimal { Name = "Pangolin", URL = "https://vignette4.wikia.nocookie.net/uncyclopedia/images/a/a3/Pangolin.jpg/revision/latest?cb=20150829033316" });
                _context.SaveChanges();
            }
        }  

        [HttpGet]
        public IEnumerable<Panimal> Get()
        {
            return _context.PanimalItems.ToList();
        }     

        [HttpGet("{id}", Name = "GetPanimal")]
        public IActionResult Get(int id)
        {
            var item = _context.PanimalItems.FirstOrDefault(t => t.Id == id);
            if (item == null) 
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Panimal item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.PanimalItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPanimal", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Panimal item)
        {
            if (item == null || item.Id != id || item.Name == null || item.URL == null)
            {
                return BadRequest();
            }

            var Panimal = _context.PanimalItems.FirstOrDefault(t => t.Id == id);
            if (Panimal == null)
            {
                return NotFound();
            }

            Panimal.URL = item.URL;
            Panimal.Name = item.Name;

            _context.PanimalItems.Update(Panimal);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}