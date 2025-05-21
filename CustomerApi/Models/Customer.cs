namespace CustomerApi.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // On crée un UUID en automatique

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Adresse { get; set; }
        
        public string Ville { get; set; }

        public string Codepostal { get; set; }

        public DateTime Datecreation { get; set; } = DateTime.Now;
    }
}