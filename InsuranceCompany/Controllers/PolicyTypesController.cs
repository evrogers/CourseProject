using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceCompany.Data;
using InsuranceCompany.Models;
using InsuranceCompany.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceCompany.Controllers
{
    public class PolicyTypesController : Controller
    {
        private readonly ApplicationContext _context;

        public PolicyTypesController(ApplicationContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        // GET: PolicyTypes
        public IActionResult Index(string PolicyName, string PolicyDescription, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;

            IQueryable<PolicyTypes> source = _context.PolicyTypes;

            if (PolicyName != null)
            {
                source = source.Where(x => x.PolicyName.Contains(PolicyName));
            }
            if (PolicyDescription != null)
            {
                source = source.Where(x => x.PolicyDescription.Contains(PolicyDescription));
            }

            switch (sortOrder)
            {
                case SortState.NameAsc:
                    source = source.OrderBy(x => x.PolicyName);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.PolicyName);
                    break;
                case SortState.PolicyConditionAsc:
                    source = source.OrderBy(x => x.PolicyCondition);
                    break;
                case SortState.PolicyConditionDesc:
                    source = source.OrderByDescending(x => x.PolicyCondition);
                    break;
                case SortState.DescriptionAsc:
                    source = source.OrderBy(x => x.PolicyDescription);
                    break;
                case SortState.DescriptionDesc:
                    source = source.OrderByDescending(x => x.PolicyDescription);
                    break;
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(PolicyName, PolicyDescription),
                PolicyTypes = items
            };
            return View(ivm);
        }
        [Authorize(Roles = "admin,user")]
        // GET: PolicyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PolicyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyName,PolicyDescription,PolicyCondition")] PolicyTypes policyTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policyTypes);
        }
        [Authorize(Roles = "admin")]
        // GET: PolicyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyTypes = await _context.PolicyTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (policyTypes == null)
            {
                return NotFound();
            }
            return View(policyTypes);
        }
        [Authorize(Roles = "admin")]
        // POST: PolicyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PolicyName,PolicyDescription,PolicyCondition")] PolicyTypes policyTypes)
        {
            if (id != policyTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyTypesExists(policyTypes.Id))
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
            return View(policyTypes);
        }
        [Authorize(Roles = "admin")]
        // GET: PolicyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyTypes = await _context.PolicyTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (policyTypes == null)
            {
                return NotFound();
            }

            return View(policyTypes);
        }
        [Authorize(Roles = "admin")]
        // POST: PolicyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyTypes = await _context.PolicyTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.PolicyTypes.Remove(policyTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyTypesExists(int id)
        {
            return _context.PolicyTypes.Any(e => e.Id == id);
        }
    }
}
