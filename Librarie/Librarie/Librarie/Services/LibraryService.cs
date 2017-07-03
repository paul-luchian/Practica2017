using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Librarie.Data.Entities;
//using System.Data.SqlClient;
//using System.Configuration;
using Librarie.Data;
using Microsoft.EntityFrameworkCore;

namespace Librarie.Services
{
    public class LibraryService : ILibraryService
    {
        private ApplicationDbContext _context;
        public LibraryService(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<Book> getBooks()
        {

            return _context.books.ToList();

        }
        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.Include(item => item.User).ToList();
        }

        public bool Borrow(string userId, int id)
        {
            _context.Transactions.Add(new Transaction
            {
                UserId = userId,
                BookId=id
            });
            return _context.SaveChanges()==1;//ne ajuta sa aflam daca operatiunea s-a efectuat cu succes
            //aflam cate inregistrari au fost adaugate in DB
            //return new EmptyResult
           
        }

        public bool Return(int id, string userId)//metoda de return
        {
            //context entity frameork
            var transaction = _context.Transactions.Single(t => t.Book.id == id && t.UserId == userId);
            _context.Transactions.Remove(transaction);
            return _context.SaveChanges() == 1;
             
        }
        /*
public List<Book> getBooks()
{
List<Book> books = new List<Book>();

SqlConnection dee = new SqlConnection();

// dee.ConnectionString = ConfigurationManager.ConnectionStrings["Data Source =DESKTOP-TUEJL5H/SQLEXPRESS; Initial Catalog = Librarie; Integrated Security = True; MultipleActiveResultSets = True"].ToString();
dee.ConnectionString = "Data Source = DESKTOP-TUEJL5H\\SQLEXPRESS; Initial Catalog = Librarie; Integrated Security = True; MultipleActiveResultSets = True";

dee.Open();
SqlCommand cm = new SqlCommand();
cm.CommandText = "SELECT * FROM [BOOKS]";

cm.Connection = dee;
SqlDataReader rd = cm.ExecuteReader();
if(rd.HasRows)
{
while(rd.Read())
{
  books.Add(new Book(Convert.ToInt32(rd[0]),rd[1].ToString(),rd[2].ToString()));
}
}
dee.Close();

return books;

//rd.Close();

return new List<Book>()
{
new Book() { id = 1, title = "The Autumn Republic", author = "Brian McClellan"},
new Book() { id = 2, title = "Anna Karenina", author = "Lev Tolstoi"},
new Book() { id = 3, title = "Hamlet", author = "William Shakespeare"},
new Book() { id = 4, title = "Norse Mythology", author = "Neil Gaiman"},
new Book() { id = 5, title = "Caraval", author = "Stephanie Garber"}
};

}
*/
    }
}
