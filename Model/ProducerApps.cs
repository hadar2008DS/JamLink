using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // table for Producer and Apps (many to many)
    public class ProducerApps: BaseEntity
    {
        private Producer producer;
        private Apps apps;

        public Producer Producer { get => producer; set => producer = value; }
        public Apps Apps { get => apps; set => apps = value; }

        public override string ToString()
        {
            return $"Id: {Id}, Producer: {Producer}, Apps: {Apps}";
        }
    }
}
