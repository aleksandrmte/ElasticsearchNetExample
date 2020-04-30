using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Page
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Number { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
