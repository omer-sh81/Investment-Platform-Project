using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment_Platform_Project.Model
{
    public class BaseEntity
    {
        private int id;

        protected int Id { get => id; set => id = value; }
    }
}
