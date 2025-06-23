namespace CustomerApi.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Adresscode { get; set; }
        public DateTime Datecreation { get; set; } = DateTime.Now;
    } }