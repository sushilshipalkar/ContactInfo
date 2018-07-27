using ContactInfo.Data.Interfaces;
using ContactInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Elmah;

namespace ContactInfoAPI.Controllers
{
    public class ContactApiController : ApiController
    {
        // GET: Contact
        public readonly IContactRepository _IContactRepository;

        public ContactApiController(IContactRepository IContactRepository)
        {
            _IContactRepository = IContactRepository;
        }

        /// <summary>
        /// purpose- Get contact details
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        [System.Web.Http.AcceptVerbs("GET")]
        [ResponseType(typeof(List<ContactViewModel>))]
        public IHttpActionResult ContactDetails()
        {
            List<ContactViewModel> result = new List<ContactViewModel>();
            try
            {
                result = _IContactRepository.GetContactDetails();
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the ContactDetails method " +
                    " in ContactApiController layer." + e));
                result = null;
            }

            return Ok(result);
        }

        /// <summary>
        /// Purpose- Insert or update contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        [System.Web.Http.AcceptVerbs("POST")]
        public IHttpActionResult InsertUpdatecontact(ContactViewModel model)
        {
            var result = false;
            try
            {
                result = _IContactRepository.InsertUpdatecontact(model);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the InsertUpdatecontact method " +
                    " in ContactApiController layer." + e));
                result = false;
            }
            return Ok(result);
        }

        /// <summary>
        /// Purpose- Delete contact details
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        [System.Web.Http.AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteContact(int contactId)
        {
            var result = false;
            try
            {
                result = _IContactRepository.DeleteContact(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the DeleteContact method " +
                    " in ContactApiController layer." + e));
                result = false;
            }
            return Ok(result);
        }
        /// <summary>
        /// Purpose- update contact status
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        [System.Web.Http.AcceptVerbs("POST")]
        public IHttpActionResult StatusUpdate(int contactId)
        {
            var result = false;
            try
            {
                result = _IContactRepository.StatusUpdate(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the StatusUpdate method " +
                    " in ContactApiController layer." + e));
                result = false;
            }
            return Ok(result);
        }

        /// <summary>
        /// Purpose- Get Contact detail by contact id 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        [System.Web.Http.AcceptVerbs("GET")]
        public IHttpActionResult GetContactById(int contactId)
        {
            var result = new ContactViewModel();
            try
            {
                result = _IContactRepository.GetContactById(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the GetContactById method " +
                    " in ContactApiController layer." + e));
                result = null;
            }
            return Ok(result);
        }



    }
}
