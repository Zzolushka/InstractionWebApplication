
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstructionsWebApplication.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IQueryable<Instruction> Instructions { get; set; }

        public string Text { get; set; }
    }
}
