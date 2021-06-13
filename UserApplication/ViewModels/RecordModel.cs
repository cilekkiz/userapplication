using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.ViewModels
{
    public class RecordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name!")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name has must max character 50!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter SurName!")]
        [Display(Name = "SurName")]
        [StringLength(50, ErrorMessage = "SurName has must max character 50!")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Please enter Email!")]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Email has must max character 150!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter BirthDate dd.MM.YYYY!")]
        [Display(Name = "BirthDate")]
        public Nullable<System.DateTime> BirthDate { get; set; }
        
        
        [Required(ErrorMessage = "Please enter Phone +90 XXX XXX XX XX!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter Location!")]
        [Display(Name = "Location")]
        public string Location { get; set; }
        
        public string ImgUrl { get; set; }
        public int UserId { get; set; }
    }
}