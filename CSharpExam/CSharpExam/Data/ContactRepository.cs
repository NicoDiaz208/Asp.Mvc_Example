using CSharpExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpExam.Data
{
    public class ContactRepository
    {
        //EntityFramework
        private readonly DataContext _context;

        public ContactRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<ContactModel> AddAsync(ContactModel contactModel)
        {
            var contact = new Contact();
            contact.Name = contactModel.Name;
            contact.Address = contactModel.Address;
            contact.Email = contactModel.Email;

            _context.Add(contact);
            await _context.SaveChangesAsync();

            return contactModel;
        }

        public IEnumerable<ContactModel> GetAll()
        {
            
            return _context.Contacts.Select(e => 
            new ContactModel
            {
                Name = e.Name,
                Address = e.Address,
                Email = e.Email,
                Id = e.Id,
            }).ToList();
        }

        public async Task<ContactModel> GetByIdAsync(int id) => 
            await _context.Contacts
            .Where(e => e.Id == id)
            .Select(o=> new ContactModel
            {
                Name = o.Name,
                Address = o.Address,
                Email = o.Email,
                Id = o.Id
            }).FirstOrDefaultAsync();

        public async Task<bool> UpdateAsync(int id, ContactModel contactModel)
        {
            var entity = _context.Contacts.Where(e => e.Id == id).FirstOrDefault();
            
            if(entity == null)
                return false;

            entity.Name = contactModel.Name;
            entity.Address = contactModel.Address;
            entity.Email = contactModel.Email;

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Contacts.FirstOrDefaultAsync(e => e.Id == id);

            if(entity == null) return false;

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
