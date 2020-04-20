using System;

namespace MC.ApplicationServices.DTOs
{
    public class RatingDto : BaseDto, IValidate
    {
        public string Name { get; set; }

        public bool Validate()
        {
            return !String.IsNullOrEmpty(Name);
        }
    }
}
