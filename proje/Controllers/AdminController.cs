using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using proje.Entities;
using proje.Models;
using System.Linq;

namespace proje.Controllers
{
    public class AdminController : Controller
    {
        public string connectionString = "SERVER=DESKTOP-VI5LI79;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
        private readonly IMapper _mapper;
        private readonly DataBaseContext _dataBaseContext;
        public AdminController(IMapper mapper, DataBaseContext dataBaseContext)
        {
            _mapper = mapper;
            _dataBaseContext = dataBaseContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SellerListPartial()
        {

            string sqlquery = "SELECT * FROM Users INNER JOIN Sellers ON Users.Id=Sellers.UserId; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();
            SqlDataReader a = command.ExecuteReader();
            List<RegisterViewModel> model1 = new List<RegisterViewModel>();

            //List<User> users = _dataBaseContext.Users.FromSqlRaw("SELECT * FROM Users INNER JOIN Sellers ON Users.Id=Sellers.UserId; " ).ToList();

            //List<RegisterViewModel> model2 = users.Select(x => _mapper.Map<RegisterViewModel>(x)).ToList();
            while (a.Read())
            {
               var details = new RegisterViewModel();
                details.Name = a["Name"].ToString();
                details.Email = a["Email"].ToString();
                details.Telephone = a["Telephone"].ToString();
                details.lastName = a["LastName"].ToString();
                details.Id = a["Id"].ToString();
                details.Role = "Seller";

                model1.Add(details);
                
            }


            return PartialView("_UserListPartial", model1);
        }
        public IActionResult CustomerListPartial()
        {

            string sqlquery = "SELECT * FROM Users INNER JOIN Customers ON Users.Id=Customers.UserId; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();
            SqlDataReader a = command.ExecuteReader();
            List<RegisterViewModel> model2 = new List<RegisterViewModel>();
            while (a.Read())
            {
                var details = new RegisterViewModel();
                details.Name = a["Name"].ToString();
                details.Email = a["Email"].ToString();
                details.Telephone = a["Telephone"].ToString();
                details.lastName = a["LastName"].ToString();
                details.Id = a["Id"].ToString();
                details.Role = "Customer";
                model2.Add(details);

            }
            return PartialView("_UserListPartial", model2);
        }
        public IActionResult DeleteUser(string id,string role)
        {
           
            SqlConnection connection1 = new SqlConnection(connectionString);
            
            connection1.Open();
            
            string sqlquery = "Delete  From Users where Id='" + id + "' ; ";
            SqlCommand command = new SqlCommand(sqlquery, connection1);
            command.ExecuteNonQuery();
            if (role == "Customer")
            {
                string sqlquery1 = "Delete  From Customers where UserId='"+id+"' ; ";
                SqlCommand command1 = new SqlCommand(sqlquery1, connection1);
                command1.ExecuteNonQuery();
                
                command.Dispose();
                command1.Dispose();
                connection1.Close();
                return CustomerListPartial();
            }
            else if (role == "Seller")
            {
                string sqlquery2 = "Delete  From Seller where UserId='" + id + "' ; ";
                SqlCommand command2 = new SqlCommand(sqlquery2, connection1);
                command2.ExecuteNonQuery();

                command.Dispose();
                command2.Dispose();
                connection1.Close();
            }
            
            return SellerListPartial();

        }

    }
}
