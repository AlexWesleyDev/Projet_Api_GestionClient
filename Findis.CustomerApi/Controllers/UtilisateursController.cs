using Microsoft.AspNetCore.Mvc;
using Findis.CustomerApi.Models;

namespace Findis.CustomerApi.Controllers
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

        [HttpGet]
        public ActionResult<IEnumerable<Utilisateur>> Get()
        {
            return utilisateurs;
        }

        [HttpGet("{id}")]
        public ActionResult<Utilisateur> Get(int id)
        {
            var utilisateur = utilisateurs.FirstOrDefault(u => u.Id == id);
            if (utilisateur == null) return NotFound();
            return utilisateur;
        }

        [HttpPost]
        public ActionResult<Utilisateur> Post(Utilisateur utilisateur)
        {
            utilisateur.Id = utilisateurs.Max(u => u.Id) + 1;
            utilisateurs.Add(utilisateur);
            return CreatedAtAction(nameof(Get), new { id = utilisateur.Id }, utilisateur);
        }
    }
}