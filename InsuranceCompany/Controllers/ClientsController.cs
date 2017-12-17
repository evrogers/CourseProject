using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsuranceCompany.Data;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceCompany.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationContext _context;

        public ClientsController(ApplicationContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        // GET: Clients
        public IActionResult Index(string ClientName,  int page = 1, SortState sortOrder = SortState.ClientNameDesc)
        {
            int pageSize = 10;

            IQueryable<Client> source = _context.Clients.Include(c => c.Group);

            if (ClientName != null)
            {
                source = source.Where(x => x.ClientName.Contains(ClientName));
            }


            switch (sortOrder)
            {
                case SortState.ClientNameAsc:
                    source = source.OrderBy(x => x.ClientName);
                    break;
                case SortState.ClientNameDesc:
                    source = source.OrderByDescending(x => x.ClientName);
                    break;
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel( ClientName),
                Client = items
            };
            return View(ivm);
        }
        [Authorize(Roles = "admin,user")]
        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
        [Authorize(Roles = "admin")]
        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "GroupName");
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientName,ClientDateBirth,ClientSex,ClientAddress,ClientPhone,ClientPassport,GroupId")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "GroupName");
            return View(client);
        }
        [Authorize(Roles = "admin")]
        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "GroupName");
            return View(client);
        }
        [Authorize(Roles = "admin")]
        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientName,ClientDateBirth,ClientSex,ClientAddress,ClientPhone,ClientPassport,GroupId")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            ViewData["GroupId"] = new SelectList(_context.ClientGroups, "Id", "GroupName");
            return View(client);
        }
        [Authorize(Roles = "admin")]
        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
        [Authorize(Roles = "admin")]
        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
