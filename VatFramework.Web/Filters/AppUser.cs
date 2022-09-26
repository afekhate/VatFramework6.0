using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VatFramework.Web.Filters
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string Name
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.Name).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }



        public string Gender
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.Gender).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public string UserId
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public string Email
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.Email).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public string FullName
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.GivenName).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public string RoleId
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.PostalCode).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public string PlantId
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.UserData).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }


        public string MiniPlantId
        {
            get
            {
                try
                {
                    return this.FindFirst(ClaimTypes.Webpage).Value;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
