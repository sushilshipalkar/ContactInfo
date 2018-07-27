using ContactInfo.Data.Models;
using ContactInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Data.EntityToviewModel
{
    public static partial class EntityToViewModelExtensions
    {
        public static ContactViewModel ToViewModel(this Contact ContactEntity)
        {
            return new ContactViewModel()
            {
                ContactId = ContactEntity.ContactId,
                Email = ContactEntity.Email,
                FirstName = ContactEntity.FirstName,
                LastName = ContactEntity.LastName,
                Status = ContactEntity.Status
            };
        }

        public static IEnumerable<ContactViewModel> ToViewModel(this IEnumerable<Contact> ContactEntity)
        {
            List<ContactViewModel> ContactViewModels = new List<ContactViewModel>();

            if (ContactEntity != null)
            {
                foreach (Contact u in ContactEntity)
                {
                    ContactViewModels.Add(u.ToViewModel());
                }
            }

            return ContactViewModels;
        }
    }
}
