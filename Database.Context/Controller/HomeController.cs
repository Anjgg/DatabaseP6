using Database.Context.Model;
using Database.Context.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Database.Context.Controller
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        private readonly IApplicationService _service;

        public HomeController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var listTickets = await _service.GetProduitsAsync();

            if (listTickets == null || listTickets.Count == 0)
            {
                return NotFound("No data found");
            }

            var result = listTickets.Select(item => new { id = item.Id, 
                                                          Product = item.ProduitSystemeExploitationVersion?.Produit?.Name, 
                                                          Version = item.ProduitSystemeExploitationVersion?.Version?.NumVersion, 
                                                          OS = item.ProduitSystemeExploitationVersion?.SystemeExploitation?.Name,
                                                          Statut = item.Statut,
                                                          Description = item.Description,
                                                          Resolution = item.Resolution,
                                                          CreationDate = item.CreationDate,
                                                          ClosingDate = item.ClosingDate }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] List<ApiModel> requete)
        {
            if (requete == null)
            {
                return BadRequest("Request cannot be null");
            }
            
            var nbOps = await _service.AddAsync(requete);
            return Ok($"{nbOps} tickets was added successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllAsync()
        {
            var nbOps = await _service.DeleteAllAsync();
            return Ok($"-{nbOps} tickets was deleted successfully");
        }
    }
}
