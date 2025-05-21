/*using Microsoft.AspNetCore.Mvc;
using CustomerApi.Models;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;

namespace CustomerApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateursController : ControllerBase
    {
        private static List<Utilisateur> utilisateurs = new List<Utilisateur>
        {
            new Utilisateur { Id = 1, Nom = "Wesley" },
            new Utilisateur { Id = 2, Nom = "Alexandre" }
        };
        
        //Liste des utilisateurs
        
        [HttpGet]
        public ActionResult<IEnumerable<Utilisateur>> GetAll()
        {
            return utilisateurs;
        }
        
        // Détail d’un utilisateur selon l'ID saisi
        
        [HttpGet("{id}")]
        public ActionResult<Utilisateur> GetById(int id)
        {
            var utilisateur = utilisateurs.FirstOrDefault(u => u.Id == id);
            if (utilisateur == null) return NotFound();
            return utilisateur;
        }
        
        // Ajouter un utilisateur

        [HttpPost]
        public ActionResult<Utilisateur> Post(Utilisateur utilisateur)
        {
            utilisateur.Id = utilisateurs.Max(u => u.Id) + 1;
            utilisateurs.Add(utilisateur);
            return CreatedAtAction(nameof(Post), new { id = utilisateur.Id }, utilisateur);
        }
        
        // Supprimer un utilisateur
        
        [HttpDelete]
        public ActionResult<Utilisateur> Delete(int id)
        {
            var utilisateur = utilisateurs.FirstOrDefault(u => u.Id == id);
            if (utilisateur == null) return NotFound();
            string chaineInformations = "\n ID : " + utilisateur.Id + "\n Nom : " + utilisateur.Nom + "\n Prénom : " + utilisateur.Prenom;
            utilisateurs.Remove(utilisateur);
            
            return Ok("L'utilisateur sélectionné dont les informations sont : " + chaineInformations + "\n Suppression réussie !");
        }
        
    }
}
*/