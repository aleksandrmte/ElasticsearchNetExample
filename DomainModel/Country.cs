using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Country
    {
        public Country()
        {
            Authors = new List<Author>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
    }
}
