using ApiStockV2.Models;
using ApiStockV2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiStockV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        RepositoryStock repo;
        public StockController(RepositoryStock repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<List<Stock>> GetStock()
        {
            return repo.GetStock();
        }
        [HttpGet("{id}")]
        public ActionResult<Stock> GetStock(int id)
        {
            return repo.GetStockId(id);
        }
        [HttpPut]
        [Route("[action]/{id}/{unidades}")]
        public async Task<ActionResult> UpdateStockUnidades(int id, int unidades)
        {
            await repo.UpdateStockUnidades(id, unidades);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task DeleteStock(int id)
        {
            await repo.DeleteStock(id);
        }
        [HttpPost]
        public async Task InsertStock(Stock s)
        {
            await this.repo.InsertStock(s.Id, s.Producto, s.Tipo, s.Marca, s.Unidades, s.PrecioCompra, s.PrecioVenta);
        }
        
    }
}
