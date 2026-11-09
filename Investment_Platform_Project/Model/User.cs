using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment_Platform_Project.Model
{
    class User:BaseEntity
    {

            public int UserId { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public string RegistrationDate { get; set; }

        public static User CreateFromDatabase(int id, string fName, string lName, string email, string role, string regDate)
        {
            return new User
            {
                UserId = id,
                FName = fName,
                LName = lName,
                Email = email,
                Role = role,
                RegistrationDate = regDate
            };
        }

    }
}

