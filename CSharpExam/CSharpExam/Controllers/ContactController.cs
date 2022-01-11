using CSharpExam.Data;
using CSharpExam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CSharpExam.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactRepository contactRepository;

        public ContactController(DataContext dataContext)
        {
            this.contactRepository = new ContactRepository(dataContext);
        }

        public IActionResult Index()
        {
            return View("Index", contactRepository.GetAll());
        }

        #region Create

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactModel contactModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contactRepository.AddAsync(contactModel);
                    return RedirectToAction("Index");
                }
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View("Create");
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return View("Error");
            }

            return View("Edit", await contactRepository.GetByIdAsync(id.Value));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(ContactModel contactModel)
        {
            await contactRepository.UpdateAsync(contactModel.Id, contactModel);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {



            return View("Delete", await contactRepository.GetByIdAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContact(ContactModel contactModel)
        {
            await contactRepository.DeleteAsync(contactModel.Id);
            return RedirectToAction("Index");
        }



        #endregion

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            return View("Details", await contactRepository.GetByIdAsync(id));
        }

        #endregion
    }
}
