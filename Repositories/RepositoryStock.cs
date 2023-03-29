using ApiStockV2.Data;
using ApiStockV2.Models;

namespace ApiStockV2.Repositories
{
    public class RepositoryStock
    {
        private StockContext context;
        public RepositoryStock(StockContext context)
        {
            this.context = context;
        }
        public LoginModel ExisteAdmin(string userName, string password)
        {
            if (userName == "Admin1" && password == "Admin1")
            {
                LoginModel admin = new LoginModel();
                admin.UserName = userName;
                admin.Password = password;
                return admin;
            }
            else
            {
                return null;
            }
        }
        public List<Stock> GetStock()
        {
            var consulta = from datos in this.context.Stock
                           select new Stock
                           {
                               Id = datos.Id,
                               Producto = datos.Producto,
                               Marca = datos.Marca,
                               Unidades = datos.Unidades,
                               PrecioVenta = datos.PrecioVenta,
                           };
            return consulta.ToList();
        }
        public Stock GetStockId(int id)
        {
            var consulta = from datos in this.context.Stock
                           where datos.Id == id
                           select datos;
            return consulta.FirstOrDefault();
        }
        public async Task UpdateStockId(int id)
        {
            Stock s = GetStockId(id);
            s.Id = id;
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateStockProducto(int id, string producto)
        {
            Stock s = GetStockId(id);
            s.Producto = producto;
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateStockTipo(int id, string tipo)
        {
            Stock s = GetStockId(id);
            s.Tipo = tipo;
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateStockMarca(int id, string marca)
        {
            Stock s = GetStockId(id);
            s.Marca = marca;
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateStockUnidades(int id, int unidades)
        {
            Stock s = GetStockId(id);
            s.Unidades = unidades;
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateStockPrecioVenta(int id, decimal precioVenta)
        {
            Stock s = GetStockId(id);
            s.PrecioVenta = precioVenta;
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateStockPrecioCompra(int id, decimal precioCompra)
        {
            Stock s = GetStockId(id);
            s.PrecioCompra = precioCompra;
            await this.context.SaveChangesAsync();
        }
        public async Task DeleteStock(int id)
        {
            Stock s = GetStockId(id);
            this.context.Stock.Remove(s);
            await this.context.SaveChangesAsync();
        }
        public async Task InsertStock(int id, string producto, string tipo, string marca, int unidades, decimal precioCompra, decimal precioVenta)
        {
            Stock s = new Stock();
            s.Id = id;
            s.Producto = producto;
            s.Tipo = tipo;
            s.Marca = marca;
            s.Unidades = unidades;
            s.PrecioCompra = precioCompra;
            s.PrecioVenta = precioVenta;
            this.context.Stock.Add(s);
            await this.context.SaveChangesAsync();
        }
    }
}
