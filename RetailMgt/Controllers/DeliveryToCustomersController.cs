using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailMgt.Data;
using RetailMgt.Models;

namespace RetailMgt.Controllers
{
    public class DeliveryToCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryToCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryToCustomers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryToCustomer.Include(d => d.Customer).Include(d => d.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryToCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToCustomer = await _context.DeliveryToCustomer
                .Include(d => d.Customer)
                .Include(d => d.Supplier)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (deliveryToCustomer == null)
            {
                return NotFound();
            }

            return View(deliveryToCustomer);
        }

        // GET: DeliveryToCustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "ID", "ID");
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "ID", "ID");
            return View();
        }

        // POST: DeliveryToCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierID,CustomerID,DeliveryDate,Price")] DeliveryToCustomer deliveryToCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryToCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "ID", "ID", deliveryToCustomer.CustomerID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "ID", "ID", deliveryToCustomer.SupplierID);
            return View(deliveryToCustomer);
        }

        // GET: DeliveryToCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToCustomer = await _context.DeliveryToCustomer.SingleOrDefaultAsync(m => m.ID == id);
            if (deliveryToCustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "ID", "ID", deliveryToCustomer.CustomerID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "ID", "ID", deliveryToCustomer.SupplierID);
            return View(deliveryToCustomer);
        }

        // POST: DeliveryToCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SupplierID,CustomerID,DeliveryDate,Price")] DeliveryToCustomer deliveryToCustomer)
        {
            if (id != deliveryToCustomer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryToCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryToCustomerExists(deliveryToCustomer.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "ID", "ID", deliveryToCustomer.CustomerID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "ID", "ID", deliveryToCustomer.SupplierID);
            return View(deliveryToCustomer);
        }

        // GET: DeliveryToCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToCustomer = await _context.DeliveryToCustomer
                .Include(d => d.Customer)
                .Include(d => d.Supplier)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (deliveryToCustomer == null)
            {
                return NotFound();
            }

            return View(deliveryToCustomer);
        }

        // POST: DeliveryToCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryToCustomer = await _context.DeliveryToCustomer.SingleOrDefaultAsync(m => m.ID == id);
            _context.DeliveryToCustomer.Remove(deliveryToCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryToCustomerExists(int id)
        {
            return _context.DeliveryToCustomer.Any(e => e.ID == id);
        }
    }
}
