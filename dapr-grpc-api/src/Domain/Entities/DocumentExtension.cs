using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class DocumentExtension
    {
        public DocumentExtension()
        {
            DocumentTemplates = new HashSet<DocumentTemplate>();
            Documents = new HashSet<Document>();
        }

        public string Extension { get; set; }
        public string CommonName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
