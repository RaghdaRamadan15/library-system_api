using day2.Models;
using day2.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BOOKController : ControllerBase
    {
        Librarybook context;
        public BOOKController(Librarybook c)
        {
            context = c;
        }

        #region getAall books
        [HttpGet]
        public IActionResult Getall()
        {
            List<book> b = context.books.Include(bo => bo.Authors).ToList();
            //create obj combin book,author

            List<bookwithAuthor> BAS = new List<bookwithAuthor>();
            foreach (var item in b)
            {
                bookwithAuthor ba = new bookwithAuthor();
                if (item.Authors != null)
                {
                    ba.AuthersName = item.Authors.Name;
                }
                else
                {
                    ba.AuthersName = "Unknown";
                }
                //ba.AuthersName = item.Authors.Name;
                ba.Publisher = item.Publisher;
                ba.Price = item.Price;
                ba.Isbn = item.Isbn;
                ba.PageCount = item.PageCount;
                ba.id = item.Id;
                ba.AuthorId = item.AuthorId;
                ba.Title = item.Title;
                BAS.Add(ba);
            }
            return Ok(BAS);
        }

        #endregion


        #region get book by id
        [HttpGet("{id}")]
        public IActionResult Getbyid(int id)
        {
            var item = context.books.Include(b => b.Authors).FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                return NotFound();

            }

            bookwithAuthor ba = new bookwithAuthor();

            ba.AuthersName = item.Authors.Name;
            ba.Publisher = item.Publisher;
            ba.Price = item.Price;
            ba.Isbn = item.Isbn;
            ba.PageCount = item.PageCount;

            ba.Title = item.Title;
            ba.id = item.Id;
            return Ok(ba);
        }

        #endregion

        #region edit book
        [HttpPut("{id}")]

        public IActionResult update(int id, bookwithAuthor b)
        {
            var boo = context.books.FirstOrDefault(b => b.Id == id);
            if (boo == null)
            {
                return NotFound();

            }
            boo.Publisher = b.Publisher;
            boo.Price = b.Price;
            boo.Isbn = b.Isbn;
            boo.Title = b.Title;
            boo.PageCount = b.PageCount;

            context.Entry(boo).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        #endregion


        #region add book
        [HttpPost]
        public IActionResult post(bookwithAuthor b)
        {
            book boo = new book();
            boo.Publisher = b.Publisher;
            boo.Price = b.Price;
            boo.Isbn = b.Isbn;
            boo.Title = b.Title;
            boo.PageCount = b.PageCount;
            boo.AuthorId = b.AuthorId;
            if (ModelState.IsValid)
            {

                context.books.Add(boo);
                context.SaveChanges();
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        #endregion


        #region delete book
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            book b = context.books.FirstOrDefault(b => b.Id == id);
            if (b == null)
            {
                return NotFound();
            }
            context.books.Remove(b);
            context.SaveChanges();
            return NoContent();

        }
        #endregion


    }
}
