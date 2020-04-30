using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Author
    {
        public Author()
        {
            Documents = new List<Document>();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public List<Document> Documents { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
