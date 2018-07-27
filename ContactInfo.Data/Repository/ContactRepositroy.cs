using ContactInfo.Data.Interfaces;
using ContactInfo.Data.Models;
using ContactInfo.Data.EntityToviewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactInfo.ViewModels;
using Elmah;
namespace ContactInfo.Data.Repository
{
    public class ContactRepositroy : IContactRepository
    {
        private IGenericRepository<Contact> _Contactrepository;

        public ContactRepositroy(IGenericRepository<Contact> Contactrepository)
        {
            _Contactrepository = Contactrepository;
        }

        public string GetDetails()
        {
            return "Sushil";
        }

        public List<ContactViewModel> GetContactDetails()
        {
            // Mapper.CreateMap<Contact, ContactViewModel>();
            List<Contact> result = new List<Contact>();
            try
            {
              
                result = _Contactrepository.Get().ToList();
            }
            catch (Exception e)
            {

                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the GetContactDetails method " +
                    " in ContactRepository layer." + e));
                result = null;
            }

            return result.ToViewModel().ToList();

        }
        /// <summary>
        /// Pupose- Insert or update contact to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Dev-Sushil S
        /// Date 24/7/2018
        public bool InsertUpdatecontact(ContactViewModel model)
        {
            var result = false;
            var ContactData = _Contactrepository.Get(x => x.ContactId == model.ContactId).FirstOrDefault();
            try
            {
                if (ContactData == null)
                {
                    ContactData = new Contact
                    {
                        Status = model.Status,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    };
                    _Contactrepository.Insert(ContactData);
                }
                else
                {
                    ContactData.FirstName = model.FirstName;
                    ContactData.LastName = model.LastName;
                    ContactData.Status = model.Status;
                    ContactData.Email = model.Email;
                    ContactData.ModifiedDate = DateTime.Now;

                    _Contactrepository.Update(ContactData);
                }
                result = true;
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the InsertUpdatecontact method " +
                   " in ContactRepository layer." + e));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Pupose- Delete contact from database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Dev-Sushil S
        /// Date 24/7/2018
        public bool DeleteContact(int contactId)
        {
            var result = false;
            try
            {

                var ContactData = _Contactrepository.Get(x => x.ContactId == contactId).FirstOrDefault();
                if (ContactData != null)
                {
                    _Contactrepository.Delete(ContactData);
                }
                result = true;
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the DeleteContact method " +
                   " in ContactRepository layer." + e));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Pupose- update status for contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Dev-Sushil S
        /// Date 24/7/2018
        public bool StatusUpdate(int contactId)
        {
            var result = false;
            try
            {

                var ContactData = _Contactrepository.Get(x => x.ContactId == contactId).FirstOrDefault();
                if (ContactData != null && ContactData.Status == true)
                {
                    ContactData = new Contact
                    {
                        Status = false,
                        //CreatedDate = DateTime.Now,
                        //ModifiedDate = DateTime.Now,
                    };
                    _Contactrepository.Update(ContactData);
                }
                result = true;
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the StatusUpdate method " +
                   " in ContactRepository layer." + e));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Purpose- Get Contact detail by contact id 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public ContactViewModel GetContactById(int contactId)
        {
            var ContactViewModel = new ContactViewModel();
            try
            {
                ContactViewModel = _Contactrepository.Get(x => x.ContactId == contactId).FirstOrDefault().ToViewModel();
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the GetContactById method " +
                   " in ContactRepository layer." + e));

                ContactViewModel = null;
            }

            return ContactViewModel;
        }
    }



}
