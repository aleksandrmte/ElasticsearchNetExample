using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticDomain;

namespace DomainModel
{
    public class Document: BaseDocument<Guid>
    {
        public Document()
        {
            Pages = new List<Page>();
        }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public int CountPages { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<Page> Pages { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
