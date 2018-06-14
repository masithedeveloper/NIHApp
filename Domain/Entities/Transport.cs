using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHApp.Domain.Entities
{
    public class Transport : Entity
    {
        public virtual long TraId { get; set; }
        public virtual string TraRegistration { get; set; }
        public virtual string TraMake { get; set; }
        public virtual string TraModel { get; set; }

        public Transport() {
        }
    }
}
