using System.ComponentModel.DataAnnotations;

namespace CSharpExam.Data
{
    public class Contact
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
