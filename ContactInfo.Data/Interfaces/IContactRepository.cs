using ContactInfo.Data.Models;
using ContactInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Data.Interfaces
{
   public interface IContactRepository
    {
       List<ContactViewModel> GetContactDetails();
       bool InsertUpdatecontact(ContactViewModel model);
       bool DeleteContact(int contactId);
       bool StatusUpdate(int contactId);
       ContactViewModel GetContactById(int contactId);
    }
}
