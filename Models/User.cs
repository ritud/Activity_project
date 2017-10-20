using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_project.Models
{
    public class User
    {
        public int userid {get; set;}
        
        public string first_name {get; set;}

        public string last_name {get; set;}

        public string email {get; set;}

        public string password {get; set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at { get; set; }


        public List<Participant> Attending {get; set;}

        public User()
        {
            Attending = new List<Participant>();
        }
    }
}