using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("Customer Form", viewModel);
        }

        [HttpPost]//Set this as a POST request only
        public ActionResult Save(Customer customer) //<-- Model Binding
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer); // Adds to memory only. DB context has a change tracking mechanism
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);              
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //Can also use something like AutoMapper to update the properties. Look this up later. 

                //    //The properties of this customer object will be updated based on the key/value pairs in the request data
                //    TryUpdateModel(customerInDb, new string[] { "Name", "Email" }); //Opens up security holes in the app
                //                     //Microsoft workaround ^^ Whitelist properties BUT now you have magic strings
            }

            _context.SaveChanges(); // Persist the changes to DB

            return RedirectToAction("Index", "Customers"); // Redirect User back to the list of customers
                                                
        }
        
        public ActionResult Index()
        {
            //Executed immediately, otherwise, executed when iterated over in the view
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
                                               //^^Eager Loading - Loads MembershipType related object when loading the Customer object                                           

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new NewCustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}