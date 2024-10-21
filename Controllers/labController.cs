using day2.DTO;
using day2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class labController : ControllerBase
    {
        Librarybook context;
        public labController(Librarybook c)
        {
            context = c;
        }

        #region create
        [HttpPost]
        public IActionResult Post(labwithauthor a)
        {

            lab l = new lab
            {
                address = a.Adress,
                Name = a.Name
            };


            context.lab.Add(l);

            
                context.SaveChanges();
            


            int labId = l.Id;


            if (a.Author_id != null && a.Author_id.Count > 0)
            {
                foreach (var authorId in a.Author_id)
                {
                    labAuthor lA = new labAuthor
                    {
                        Authorid = authorId,
                        labid = labId
                    };

                    context.labAuthors.Add(lA);
                }

                  
                    context.SaveChanges();
                
            }

            return Ok();
        }

        #endregion



        #region getall
        [HttpGet]
        public async Task<IActionResult> get()
        {
            List<lab>la=context.lab.Include(l=>l.labAuthors).ToList();
            List<labwithauthor> lab_author = new List<labwithauthor>();
            foreach (var item in la)
            {
                labwithauthor las = new labwithauthor();
                las.Name = item.Name;
                las.Adress = item.address;
                las.id = item.Id;
                if(item.labAuthors!=null&& item.labAuthors.Count > 0)
                {
                    las.Auhtors = new List< string > ();
                    foreach (var item1 in item.labAuthors)
                    {
                        var value = context.Authors.FirstOrDefault(a => a.Id == item1.Authorid);
                        if(value != null) {
                            las.Auhtors.Add(value.Name);
                        }
                        
                    }
                }
                lab_author.Add(las);

            }
            return Ok(lab_author);
        }
        #endregion


        #region delete 
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var lab = context.lab.Include(l=>l.labAuthors).FirstOrDefault(a => a.Id == id);
            if (lab == null)
            {
                return NotFound();
            }
            if (lab.labAuthors != null)
            {
                foreach (var item in lab.labAuthors)
                {
                    item.Authorid = null;
                   
                    context.Entry(item).State = EntityState.Modified;

                }


            }
            //List<labAuthor> lA = context.labAuthors.ToList();
            //foreach (var item in lA)
            //{
            //    if (item.Authorid == id)
            //    {
            //        item.Authorid = null;
            //        context.Entry(item).State = EntityState.Modified;
            //    }
            //}


            context.lab.Remove(lab);
            context.SaveChanges();
            return NoContent();

        }

        #endregion


        #region get Author by id
        [HttpGet("{id}")]
        public IActionResult getbyid(int id)
        {
            var lab = context.lab.Include(a => a.labAuthors).FirstOrDefault(a => a.Id == id);
            if (lab == null)
            {
                return NotFound();
            }
            labwithauthor labs = new labwithauthor();
            labs.Name = lab.Name;
            labs.Adress=lab.address;
            labs.id = lab.Id;
            if (lab.labAuthors != null && lab.labAuthors.Count > 0)
            {
                labs.Auhtors = new List<string>();
                foreach (var item in lab.labAuthors) { 
                    if (item.Author != null)
                { labs.Auhtors.Add($"{item.Author.Name}"); }
                }

            }
            return Ok(labs);

        }

        #endregion


        #region edit author 
        [HttpPut("{id}")]
        public IActionResult update(int id, labwithauthor a)
        {
            var lab = context.lab.FirstOrDefault(a => a.Id == id);
            if (lab == null)
            {
                return NotFound();
            }
            lab.Name = a.Name;
            lab.address = a.Adress;

            context.Entry(lab).State = EntityState.Modified;
            context.SaveChanges();
          
            return NoContent();


        }

        #endregion


    }
}
