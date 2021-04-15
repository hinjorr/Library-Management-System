using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace masood_lab.Models
{
    public class BooksModel
    {

        public int BookID { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookAuthor { get; set; }
        [Required]
        public string BookPublisher { get; set; }
        [Required]
        public string BookPrice { get; set; }
        public string BooksCategory { get; set; }

        [Required]  
        public int BooksCategoryID { get; set; }

        public string ImagePath { get; set; }


        public string AbstractBookname { get; set; }

        public string Abstractbooktype { get; set; }
        public string Abstractdepartment { get; set; }

        public string Abstractpublisher { get; set; }











    }
}