using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstructionsWebApplication.Models
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; }

        public string UserURL { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        [ForeignKey("InstructionId")]
        public virtual Instruction Instruction { get; set; }
    }
}
