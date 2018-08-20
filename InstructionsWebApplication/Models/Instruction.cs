using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstructionsWebApplication.Models
{
    public class Instruction
    {
        [Key]
        public string InstructionId { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public string Tags { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

        
    }
}
