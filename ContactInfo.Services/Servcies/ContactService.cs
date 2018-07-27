using ContactInfo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactInfo.ViewModels;
using Elmah;

namespace ContactInfo.Services.Servcies
{
    public class ContactService : BaseService, IContactService
    {
        #region "Public member"
        readonly string uri = string.Format("{0}{1}", baseUrl, "ContactApi");
        GenericHttpClient<ContactViewModel> contactInfoApi = new GenericHttpClient<ContactViewModel>();
        #endregion

        #region Methods
        /// <summary>
        /// purspose- Get conatct details from API GetContactDetails method
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public async Task<List<ContactViewModel>> GetContactDetails()
        {
            string strUri = string.Format("{0}/ContactDetails", uri);
            List<ContactViewModel> result = null;
            try
            {
                result = await contactInfoApi.APIGetListAsync<ContactViewModel>(strUri);
            }
            catch (Exception e)
            {
                
                 ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the GetContactDetails method " +
                    " in ContactService layer." + e));
                 result = null;
            }
         
            return result;
          
        }


        /// <summary>
        /// purspose- Insert or update conatct details from API InsertUpdatecontact method
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public async Task<bool> InsertUpdatecontact(ContactViewModel model)
        {
            var result = false;
            string strUri = string.Format("{0}/InsertUpdatecontact", uri);
            try
            {
                result = await contactInfoApi.APIPostAsync<bool>(model, strUri);
            }
            catch(Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the InsertUpdatecontact method " +
                   " in ContactService layer." + e));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// purspose- delete conatct details from API DeleteContact method
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public async Task<bool> DeleteContact(int contactId)
        {
            var result = false;
            string strUri = string.Format("{0}/DeleteContact?contactId={1}", uri, contactId);
            try
            {
                result = await contactInfoApi.APIDeleteAsync<bool>(strUri);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the DeleteContact method " +
                   " in ContactService layer." + e));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// purspose- upadte status from API StatusUpdate method
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public async Task<bool> StatusUpdate(int contactId)
        {
            var result = false;
            string strUri = string.Format("{0}/StatusUpdate?contactId={1}", uri, contactId);
            try
            {
                
                result = await contactInfoApi.APIGetAsync<bool>(strUri);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the StatusUpdate method " +
                      " in ContactService layer." + e));
                result = false;
            }
            return result;
        }


        /// <summary>
        /// purspose- upadte status from API StatusUpdate method
        /// </summary>
        /// <returns></returns>
        /// Dev-Sushil Shipalkar
        /// Date-24/7/2018
        public async Task<ContactViewModel> GetContactById(int contactId)
        {
            var result = new ContactViewModel();
            string strUri = string.Format("{0}/GetContactById?contactId={1}", uri, contactId);
            try
            {
              
                result = await contactInfoApi.APIGetAsync<ContactViewModel>(strUri);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(new NotImplementedException("An error occurred in the GetContactById method " +
                    " in ContactService layer." + e));
                result = null;
            }
            return result;
        }
        #endregion
    }
}
