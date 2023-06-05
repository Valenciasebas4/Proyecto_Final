﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Final.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public class AddTrainingUserViewModel
    {
        public User User { get; set; }
        public Training Training { get; set; }

        [Display(Name = "Entrenamiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid TrainingId { get; set; }

        public IEnumerable<SelectListItem> Trainings { get; set; }

        public DateTime DateClass { get; set; }
    }
}
