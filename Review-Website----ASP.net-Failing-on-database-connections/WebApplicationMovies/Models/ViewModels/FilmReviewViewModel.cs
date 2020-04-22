using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMovies.Models.ViewModels
{
    public class FilmReviewViewModel
    {
        public Review Review { get; set; }

        public Film Film { get; set; }
    }
}