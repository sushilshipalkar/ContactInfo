using ContactInfo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ContactInfo.ViewModels;
using System.Web.Routing;
using Elmah;

namespace ContactInfo.Controllers
{
    public class ContactController : Controller
    {
        #region Public Members
        public readonly IContactService _ContactService;
        #endregion

        #region Constructor
        public ContactController(IContactService ContactService)
        {
            _ContactService = ContactService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// purpose- Get conatct details from service GetContactDetails method
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public async Task<ActionResult> Index()
        {
            List<ContactViewModel> contactDetails = new List<ContactViewModel>();
            try
            {
                contactDetails = await _ContactService.GetContactDetails();
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the Index method " +
                                 " in ContactController layer." + e));
            }
           
            return View(contactDetails);

        }

        /// <summary>
        /// purpose-Delete Contact details from service DeleteContact method
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// Dev-Sushil S
        /// Date 24/7/2018
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteContact(int contactId)
        {
            var result = false;

            try
            {
                result = await _ContactService.DeleteContact(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the DeleteContact post method " +
                                  " in ContactController layer." + e));
                result = false;
            }

            if (result)
            {
                return RedirectToAction("Index");
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// purpose-status update from service StatusUpdate method
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// Dev-Sushil S
        /// Date 24/7/2018
        public async Task<JsonResult> StatusUpdate(int contactId)
        {
            var result = false;

            try
            {
                result = await _ContactService.StatusUpdate(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the StatusUpdate method " +
                                  " in ContactController layer." + e));
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

         /// <summary>
         /// update contact details from service InsertUpdatecontact method
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         ///  /// Dev-Sushil S
         /// Date 24/7/2018
         [HttpPost]        
         public async Task<ActionResult> Edit(ContactViewModel model)
         {
             var result = false;

             try
             {
                 result = await _ContactService.InsertUpdatecontact(model);
             }
             catch (Exception e)
             {
                 ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the Edit post method " +
                                  " in ContactController layer." + e));
                 result = false;
             }

             if(result)
             {
                 return RedirectToAction("Index");
             }

             return Json(result, JsonRequestBehavior.AllowGet);

         }

         /// <summary>
         /// Create contact details from service InsertUpdatecontact method
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         ///  /// Dev-Sushil S
         /// Date 24/7/2018
         [HttpPost]
         public async Task<ActionResult> Create(ContactViewModel model)
         {
             var result = false;

             try
             {
                 result = await _ContactService.InsertUpdatecontact(model);
             }
             catch (Exception e)
             {
                 ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the Create method " +
                                  " in ContactController layer." + e));
                 result = false;
             }

             if (result)
             {
                 return RedirectToAction("Index");
             }

             return Json(result, JsonRequestBehavior.AllowGet);

         }

        public ActionResult Create()
        {

            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> DeleteView(int contactId)
        {
            var ContactViewModel = new ContactViewModel();
            try
            {
                ContactViewModel = await _ContactService.GetContactById(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the DeleteView method " +
                                  " in ContactController layer." + e));
                ContactViewModel = null;
            }
            return View(ContactViewModel);
        }
        public async Task<ActionResult> Details(int contactId)
        {
            var ContactViewModel = new ContactViewModel();
            try
            {
                ContactViewModel = await _ContactService.GetContactById(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the Details method " +
                                  " in ContactController layer." + e));
                ContactViewModel = null;
            }
            return View(ContactViewModel);
        }

        public async Task<ActionResult> Edit(int contactId)
        {
            var ContactViewModel = new ContactViewModel();
            try
            {
                ContactViewModel = await _ContactService.GetContactById(contactId);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the Edit method " +
                                  " in ContactController layer." + e));
                ContactViewModel = null;
            }
            return View(ContactViewModel);
        }

        #endregion

    }
}