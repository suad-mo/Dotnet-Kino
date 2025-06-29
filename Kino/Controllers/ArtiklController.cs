using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kino.Services.Artikl;
using Kino.Core.ViewModels;

namespace Kino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtiklController : ControllerBase
    {
        private readonly IArtiklService _artiklService;

        public ArtiklController(IArtiklService artiklService)
        {
            _artiklService = artiklService;
        }

        [Authorize(Roles = "Admin, Radnik")]
        [HttpGet("all")]
        public ActionResult<List<ArtiklViewModel>> GetAllArtikl()
        {
            try
            {
                var artikli = _artiklService.GetArtikle();

                if (artikli == null || artikli.ToArray().Length == 0)
                    return NotFound("Nema artikala za prikaz.");

                return Ok(artikli);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom dohvaćanja artikala.");
            }
        }

        [Authorize(Roles = "Admin, Radnik")]
        [HttpGet]
        public ActionResult<List<ArtiklViewModel>> GetArtikle(int pageNumber, int pageSize, string searchType, string searchQuery)
        {
            try
            {
                var artiklList = _artiklService.GetAllArtikle(pageNumber, pageSize, searchType, searchQuery);

                if (artiklList == null || artiklList.ToArray().Length == 0)
                    return NotFound("Nije pronađen niti jedan artikl.");

                var paginationMD = _artiklService.GetPaginationMetadata(pageSize);

                // Fix: Use IHeaderDictionary.Append instead of Add to avoid duplicate key issues
                Response.Headers.Append("X-Total-Count", paginationMD.ItemsCount.ToString());
                Response.Headers.Append("X-Page-Count", paginationMD.PageCount.ToString());

                return Ok(artiklList);
            }
            catch
            {
                return BadRequest("Došlo je do greške.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ArtiklViewModel> GetArtikl(int id)
        {
            try
            {
                // Fix: Correct the variable name to use the existing _artiklService field
                var result = _artiklService.GetOneArtikl(id);

                if (result == null)
                    return NotFound($"Artikl nije pronađen. Id: {id}");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške.");
            }
        }

        //[Authorize(Roles = "Admin, Radnik")]
        [HttpPost]
        public ActionResult<ArtiklViewModel> PostArtikl(ArtiklViewModel artiklViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Došlo je do greške.");

            try
            {
                var result = _artiklService.Save(artiklViewModel);

                if (result == null)
                    return BadRequest("Dodavanje artikla neuspješno.");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške.");
            }
        }

        [Authorize(Roles = "Admin, Radnik")]
        [HttpPut("{id}")]
        public ActionResult<ArtiklViewModel> PutArtikl(int id, ArtiklViewModel artiklViewModel)
        {
            if (id != artiklViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest("Došlo je do greške.");


            try
            {
                var result = _artiklService.Save(artiklViewModel);

                if (result == null)
                    return BadRequest($"Ažuriranje artikla neuspješno. Id: {id}");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške.");
            }
        }

        [Authorize(Roles = "Admin, Radnik")]
        [HttpDelete]
        public ActionResult<ArtiklViewModel> DeleteArtikl(int id)
        {
            try
            {
                var result = _artiklService.Delete(id);

                if (result == null)
                    return BadRequest($"Brisanje artikla neuspješno. Id: {id}");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške.");
            }
        }
    }
}
