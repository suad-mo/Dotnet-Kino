using Kino.Core.ViewModels;
using Kino.Services.KategorijaArtikl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorijaArtiklController : ControllerBase
    {
        private readonly IKategorijaArtiklService _iKategorijaArtiklService;

        public KategorijaArtiklController(IKategorijaArtiklService iKategorijaArtiklService)
        {
            _iKategorijaArtiklService = iKategorijaArtiklService;
        }

        [HttpGet]
        public ActionResult<List<KategorijaArtiklViewModel>> GetKategorije()
        {
            try
            {
                var kategorije = _iKategorijaArtiklService.GetKategorije();
                
                if (kategorije == null || kategorije.ToArray().Length == 0)
                    return NotFound("Nema kategorija za prikaz.");
                
                return Ok(kategorije);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom dohvaćanja kategorija.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<KategorijaArtiklViewModel> GetKategorijaArtikl(int id)
        {
            try
            {
                var kategorija = _iKategorijaArtiklService.GetOneKategorija(id);
                
                if (kategorija == null)
                    return NotFound("Nije pronađena kategorija sa tim ID-jem.");
                
                return Ok(kategorija);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom dohvaćanja kategorije.");
            }
        }

        //[Authorize(Roles = "Admin, Radnik")]
        [HttpPost]
        public ActionResult<KategorijaArtiklViewModel> PostKategorijaArtikl(KategorijaArtiklViewModel kategorijaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Došlo je do greške. Molimo provjerite unesene podatke.");
                
                var result = _iKategorijaArtiklService.Save(kategorijaViewModel);
                
                if (result == null)
                    return BadRequest("Došlo je do greške prilikom spašavanja kategorije.");
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Došlo je do greške prilikom spašavanja kategorije: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin, Radnik")]
        [HttpPut("{id}")]
        public ActionResult<KategorijaArtiklViewModel> PutKategorijaArtikl(int id, KategorijaArtiklViewModel kategorijaViewModel)
        {
            try
            {
                if (id != kategorijaViewModel.Id)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest("Došlo je do greške. Molimo provjerite unesene podatke.");
                
                var result = _iKategorijaArtiklService.Save(kategorijaViewModel);
                
                if (result == null)
                    return BadRequest("Došlo je do greške prilikom ažuriranja kategorije.");
                
                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom ažuriranja kategorije.");
            }
        }

        //[Authorize(Roles = "Admin, Radnik")]
        [HttpDelete]
        public ActionResult<KategorijaArtiklViewModel> DeleteKategorijaArtikl(int id)
        {
            try
            {
                var result = _iKategorijaArtiklService.Delete(id);

                if (result == null)
                    return BadRequest($"Brisanje kategorije artikla neuspješno. Id: {id}");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške.");
            }
        }
    }
}
