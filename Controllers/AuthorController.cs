using day2.DTO;
using day2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        Librarybook context;
        public AuthorController(Librarybook c)
        {
            context = c;
        }
        #region getall Author
        [HttpGet]
        public IActionResult getAll()
        {
            List<Author> Au = context.Authors.Include(a => a.Books).ToList();

            List<AuthorWithBook> ABS = new List<AuthorWithBook>();
            foreach (var item in Au)
            {
                AuthorWithBook AB = new AuthorWithBook();
                AB.Name = item.Name;
                AB.id = item.Id;
                AB.bookTitle = new List<string>();
                if (item.Books != null && item.Books.Count > 0)
                {

                    foreach (var item1 in item.Books)
                    {
                        AB.bookTitle.Add(item1.Title);
                    }
                }


                ABS.Add(AB);

            }


            return Ok(ABS);

        }

        #endregion


        #region get Author by id
        [HttpGet("{id}")]
        public IActionResult getbyid(int id)
        {
            var aut = context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            if (aut == null)
            {
                return NotFound();
            }
            AuthorWithBook AB = new AuthorWithBook();
            AB.Name = aut.Name;
            AB.id = aut.Id;
            if (aut.Books != null && aut.Books.Count > 0)
            {
                AB.bookTitle = new List<string>();
                foreach (var item in aut.Books)
                { AB.bookTitle.Add($"{item.Title}"); }

            }
            return Ok(AB);

        }

        #endregion


        #region edit author 
        [HttpPut("{id}")]
        public IActionResult update(int id, AuthorWithBook a)
        {
            var au = context.Authors.FirstOrDefault(a => a.Id == id);
            if (au == null)
            {
                return NotFound();
            }
            au.Name = a.Name;

            context.Entry(au).State = EntityState.Modified;
            context.SaveChanges();
            //return Ok(au);
            return NoContent();


        }

        #endregion


        #region delete 
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var au = context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            if (au == null)
            {
                return NotFound();
            }
            if (au.Books != null)
            {
                foreach (var item in au.Books)
                {
                    item.AuthorId = null;
                    item.Authors.Name = au.Name;
                    context.Entry(item).State = EntityState.Modified;

                }


            }
            List<labAuthor>lA=context.labAuthors.ToList();
            foreach (var  item in lA)
            {
                if (item.Authorid == id)
                {
                    item.Authorid = null;
                    context.Entry(item).State = EntityState.Modified;
                }
            }


            context.Authors.Remove(au);
            context.SaveChanges();
            return NoContent();

        }

        #endregion

        #region add author
        [HttpPost]
        public IActionResult post(AuthorWithBook a)
        {
            Author au = new Author();
            au.Name = a.Name;

            if (ModelState.IsValid)
            {
                context.Authors.Add(au);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }

        #endregion



    }
}
