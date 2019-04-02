using System.Collections.Generic;
using System.Linq;
using EF_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF_Demo.Controllers
{
   [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkshopController: ControllerBase 
    {
         private ApplicationDbContext _context;   

         public WorkshopController(ApplicationDbContext context)  
    {  
  
      _context = context;  
       if (!_context.Workshops.Any())   
       {  
         _context.Workshops.Add(new Workshop   
                  { Name = "Event Management", Speaker = "Shweta"});  
         _context.SaveChanges();   
       }
         
    }

    [HttpGet]  
 public IEnumerable<Workshop> GetWorkshops()  
  {  
          return _context.Workshops;  
   }
[HttpPost]
    public IEnumerable<Workshop> setWorkshops(Workshop c)  
  {  

        _context.Workshops.Add(c);  
         _context.SaveChanges();   
          return _context.Workshops;  
   }

   [HttpPost]
   public IActionResult AddWorkshop (Workshop workshop)
   {
       if (workshop == null)  
            return BadRequest();  
             _context.Workshops.Add(workshop);  
              _context.SaveChanges(); 
               return Accepted();
   }
    [HttpPut]
    public IActionResult EditWorkshop (int id, [FromBody] Workshop ws)
    {
        if(ws == null || ws.Id != id)
        {
            return BadRequest();
        }

         var workshop = _context.Workshops.FirstOrDefault(i => i.Id == id);
         if(workshop == null)
         {
             return NotFound();
            
         }

         workshop.Name = ws.Name;
         workshop.Speaker = ws.Speaker;

         _context.Workshops.Update(workshop);
         _context.SaveChanges();
         return Accepted();
        
    }

    [HttpDelete]  
public IActionResult DeleteWorkshopByID(int id)  
{  
    var workshop = _context.Workshops.FirstOrDefault(i => i.Id == id);  
    if (workshop == null)  
         return NotFound();  
  
    _context.Workshops.Remove(workshop);  
    _context.SaveChanges();  
  
    return new NoContentResult();  
}  

}

}