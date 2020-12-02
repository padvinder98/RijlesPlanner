using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RijlesPlanner.ApplicationCore.Models;

namespace RijlesPlanner.UI.MVC.ViewModels.LessonViewModels
{
    public class AddLessonViewModel
    {
        [Required]
        public string StudentId { get; set; }

        public List<SelectListItem> Students { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Start datum en tijd")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Eind datum en tijd")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
