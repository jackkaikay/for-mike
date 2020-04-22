using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationMovies.Models
{
    [Table("REVIEW")]
    public class Review
    {


        [Column("review_id")]
        [Display(Name = "ID")]
        public int ReviewID { get; set; }

        [Required]
        [Column("film_id")]
        [Display(Name = "Film")]
        public int FilmID { get; set; }

        [Required]
        [Column("review_name")]
        [Display(Name = "Username")]
        public string ReviewUname { get; set; }

        [Required]
        [Column("review_content")]
        [Display(Name = "Review Content")]
        [DataType(DataType.MultilineText)]
        public string ReviewContent { get; set; }

        [Required]
        [Column("review_rating")]
        [Display(Name = "Rating")]
        public int ReviewRating { get; set; }

        public string ReviewContentTrimmed
        {
            get
            {
                if ((ReviewContent.Length) > 100)
                    return ReviewContent.Substring(0, 100) + "...";
                else
                    return ReviewContent;
            }
        }

    }
}