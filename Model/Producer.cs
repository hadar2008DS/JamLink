using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // 1:1 with Person
    public class Producer: Person
    {

        private bool isActive;  
        public bool IsActive { get => isActive; set => isActive = value; }

        public override string ToString()
        {
            return $"Producer: {base.ToString()}, IsActive: {IsActive}";
        }
    }
}
