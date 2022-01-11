namespace CSharpExam.Models
{
    public class ContactModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }

        public ContactModel(int id, string name, string address, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.Email = email;
        }

        public ContactModel() { }
    }
}
