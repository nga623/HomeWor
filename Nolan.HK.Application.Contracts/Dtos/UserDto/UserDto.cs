using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application.Contracts.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage  = "Name length can't be more than 50.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[._~!@#$^&*])[A-Za-z0-9._~!@#$^&*]{8,20}$", ErrorMessage = "The password is case sensitive and consists of letters, digits, and special characters")]
        public string Password { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$",ErrorMessage ="is not email")]
        public string Email { get; set; }
        public string Address { get; set; }
        public int UserTypeEnum { get; set; }
        public string UserType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
    }
}
