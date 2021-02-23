using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    public  class EverNoteUser:MyEntityBase

    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool IsActive { get; set; }
        [ScaffoldColumn(false)]
        public  string photo { get; set; }
        [ScaffoldColumn(false), Required]

        public Guid ActivateGuid { get; set; }
        public bool Admin { get; set; }
        public virtual List<Note> notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }



    }
}
