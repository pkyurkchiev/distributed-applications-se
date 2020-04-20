using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MC.Website.Utils
{
    public static class LoadDataUtil
    {
        public static SelectList LoadGenreData()
        {
            using (GenresReference.GenresClient service = new GenresReference.GenresClient())
            {
                return new SelectList(service.GetGenres(), "Id", "Name");
            }
        }
    }
}