using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_project.Models
{
    public class Participant
    {
        [Key]
        public int participantsid {get; set;}

        public int userid {get; set;}
        public User User {get; set;}

        public int activityid {get; set;}
        public Activity Activity {get; set;}

    }
}

        