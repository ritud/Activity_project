using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_project.Models
{
    public class Activity
    {
        public int activityid {get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name="Title")]
        public string title {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Date")]
        public DateTime date {get; set;}
        
        [Required]
        [DataType(DataType.Time)]
        [Display(Name="Time")]
        public DateTime time {get; set;}

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name="Duration")]
        public int duration {get; set;}

         public string durationtype {get; set;}

        [Required]
        [MinLength(10,ErrorMessage="Minimun 10 characters required")]
        [Display(Name="Description")]
        public string description {get; set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; set; }

        public int userid {get; set;}
        public User User {get; set;}

        public List<Participant> participants {get; set;}

        public Activity()
        {
            participants = new List<Participant>();
        }
    }
}