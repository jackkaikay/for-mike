using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplicationMovies.Models
{

    [Table("ENTRY")]
    public class Entry
    {

        [Column("entry_id")]
        [Display(Name = "ID")]
        public int EntryID { get; set; }

        [Required]
        [Column("entry_fname")]
        [Display(Name = "Title")]
        public string EntryFname { get; set; }

        [Required]
        [Column("entry_sname")]
        [Display(Name = "Genre")]
        public string EntrySname { get; set; }

        [Required]
        [Column("entry_desc")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string EntryDesc { get; set; }

        [Column("entry_img")]
        [Display(Name = "Image")]
        public string EntryImage { get; set; }



        public string EntryDescTrimmed

        {
            get
            {
                if ((EntryDesc.Length > 100))

                    return EntryDesc.Substring(0, 100) + "...";
                else
                    return EntryDesc;


                    
            }
        }
    }
}