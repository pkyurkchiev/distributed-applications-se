using System;

namespace MC.ApplicationServices.DTOs
{
    public class GenreDto : BaseDto, IValidate
    {
        public string Name { get; set; }

        public bool Validate()
        {
            return !String.IsNullOrEmpty(Name);
        }
    }
}
