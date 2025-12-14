using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class Apps : BaseEntity
    {
        private string? appName;
        public string? AppName { get => appName; set => appName = value; }

        public override string ToString()
        {
            return $"Id: {Id}, AppName: {AppName}";

        }
    }

}
