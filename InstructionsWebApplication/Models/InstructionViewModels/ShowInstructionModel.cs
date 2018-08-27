using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstructionsWebApplication.Models.InstructionViewModels
{
    public class ShowInstructionModel
    {
        public IEnumerable<Page> Pages { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
