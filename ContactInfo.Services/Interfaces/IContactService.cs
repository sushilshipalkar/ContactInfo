using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactInfo.ViewModels;

namespace ContactInfo.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactViewModel>> GetContactDetails();
        Task<bool> InsertUpdatecontact(ContactViewModel model);
        Task<bool> DeleteContact(int contactId);
        Task<bool> StatusUpdate(int contactId);
         Task<ContactViewModel> GetContactById(int contactId);
    }
}
