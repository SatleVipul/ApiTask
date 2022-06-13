using System.ComponentModel.DataAnnotations;

namespace ResturantProject.Models
{
    public class dbPlayer
    {
       [Key]
        public int PlayerId { get; set; }
     
        public string Name{ get; set; }

        public string dob { get; set; }

        public string primaryaddress { get; set; }
        public string alternateaddress { get; set; }

        public string officeaddress{ get; set; }

        public string mobilenumber { get; set; }

        public string email { get; set; }

        public string driverslicense { get; set; }

        public string passport { get; set; }

        public string pstreetnumber { get; set; }

        public string paddressline1 { get; set; }

        public string paddressline2 { get; set; }

        public string pCity { get; set; }

        public string pState { get; set; }

        public string pPostal { get; set; }

        public string pcountry { get; set; }


































    }
}
