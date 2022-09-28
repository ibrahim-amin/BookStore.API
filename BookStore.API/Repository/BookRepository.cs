using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository:IBookRepository
    {
        private readonly BookStoreContext dc;
        private readonly IMapper mapper;

        public BookRepository(BookStoreContext dc,IMapper mapper)
        {
            this.dc = dc;
            this.mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllBookAsync()
        {
            //var records = await dc.Books.Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,

            //}).ToListAsync();
            //return records;


            var records = await dc.Books.ToListAsync();
            return mapper.Map<List<BookModel>>(records);


        }

        public async Task<BookModel> GetBookById(int BookId)
        {
            //var records = await dc.Books.Where(X => X.Id==BookId).Select(X => new BookModel() 
            //{
            //    Id = X.Id,
            //    Title=X.Title,
            //    Description=X.Description,
            //}).FirstOrDefaultAsync();

            //return records;
            var book = await dc.Books.FindAsync(BookId);
            return mapper.Map<BookModel>(book);


        }

        public async Task<int> AddBook(BookModel bookModel)
        {
            var book = new Books()
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Description = bookModel.Description,
            };
            await dc.Books.AddAsync(book);
           await dc.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBook(int Id, BookModel bookModel)
        {
            //var book = await dc.Books.FindAsync(Id);
            //if(book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;
            //      await dc.SaveChangesAsync();
            //}
            //alter native way
            var book = new Books()
            {
                Id = Id,
                Title = bookModel.Title,
                Description = bookModel.Description,
            };
             dc.Books.Update(book);
            await dc.SaveChangesAsync();

        }  
        
        
        
        public async Task UpdatePatchBookAsync(int Id, JsonPatchDocument bookModel)
        {
            var book = await dc.Books.FindAsync(Id);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await dc.SaveChangesAsync();
            }


        }

       
        //public async Task DeleteBookAsync(int BookId)
        //{
        //    var book = new Books
        //    {
        //        Id = BookId
        //    };
        //    dc.Remove(book);
        //    await dc.SaveChangesAsync();
        //  }
    // Alter native Way

    public async Task DeleteBookAsync(int BookId)
    {
            var book = dc.Books.Find(BookId);
        if(book != null)
            {
        dc.Remove(book);
        await dc.SaveChangesAsync();

            }

          }





    }
}
