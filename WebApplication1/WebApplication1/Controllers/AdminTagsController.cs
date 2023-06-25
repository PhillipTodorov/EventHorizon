using EventHorizonBackend.Data;
using EventHorizonBackend.Models.Domain;
using EventHorizonBackend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventHorizonBackend.Controllers
{
    public class AdminTagsController : Controller
    {

        private EventHorizonDbContext eventHorizonDbContext;

        public AdminTagsController(EventHorizonDbContext eventHorizonDbContext) 
        { 
            this.eventHorizonDbContext = eventHorizonDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            
            eventHorizonDbContext.Tags.Add(tag);
            eventHorizonDbContext.SaveChanges();

            return View("Add");
        }
    }
}