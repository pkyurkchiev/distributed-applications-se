using System;

namespace MC.ApplicationServices.DTOs
{
    public class DirectorDto : BaseDto, IValidate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public bool Validate()
        {
            return !String.IsNullOrEmpty(FirstName) && !String.IsNullOrEmpty(UserName);
        }
    }
}
