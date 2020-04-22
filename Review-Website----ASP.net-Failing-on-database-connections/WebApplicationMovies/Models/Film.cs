using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationMovies.Models
{   [Table("FILM")]
    public class Film
    {
        [Column("film_id")]
        [Display(Name = "ID")]
        public int FilmID { get; set; }      

        [Required]
        [Column("film_title")]
        [Display(Name = "Title")]
        public string FilmTitle { get; set; }

        [Required]
        [Column("film_genre")]
        [Display(Name = "Genre")]
        public string FilmGenre { get; set; }

        [Required]
        [Column("film_desc")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string FilmDesc { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
            ApplyFormatInEditMode = true)]
        [Column("film_r_date")]
        [Display(Name = "Release Date")]
        public DateTime? FilmReleaseDate{ get; set; }

        [Column("film_img")]
        [Display(Name = "Image")]
        public string FilmImage { get; set; }


        public string FilmSecTrimmed;


        public string FilmDescTrimmed
        {

            get
            {

                if ((FilmDesc.Length) > 100)

                    return FilmDesc.Substring(0, 100) + "...";
                else
                    return FilmDesc;
            }
        }
    }
}