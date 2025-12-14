using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // Base class for Person entities
    public class Person: BaseEntity
    {
       
        private string username;
        private string passw;
        private bool isActive;
        public string Username { get => username; set => username = value; }
        public string PassW { get => passw; set => passw = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public override string ToString()
        {
            return $"Id: {Id}, Username: {Username},PassW: {PassW} IsActive: {IsActive}";
        }
    }
}
