using ApiSGCubos.Data;
using ApiSGCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSGCubos.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await
                this.context.Cubos.ToListAsync();
        }



        public async Task<List<Cubo>> GetCubosMarcaAsync(string marca)
        {
            return await
                this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }

        public async Task InsertarUsuarioAsync(int Id_Usuario, string Nombre, string Email, string Pass, string Imagen)
        {
            Usuario usuario = new Usuario();
            usuario.Id_Usuario = Id_Usuario;
            usuario.Nombre = Nombre;
            usuario.Pass = Pass;
            usuario.Email = Email;
            usuario.Imagen = Imagen;
            this.context.Usuario.Add(usuario);
            await this.context.SaveChangesAsync();
        }

        public async Task InsertarCuboAsync(int Id_Cubo, string Nombre, string Marca, string Imagen, int Precio)
        {
            Cubo cubo = new Cubo();
            cubo.Id_Cubo = Id_Cubo;
            cubo.Nombre = Nombre;
            cubo.Marca = Marca;      
            cubo.Imagen = Imagen;
            cubo.Precio = Precio;
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> PerfilUsuarioAsync(int Id_Usuario)
        {
            return await
              this.context.Usuario.FirstOrDefaultAsync
              (x => x.Id_Usuario == Id_Usuario);
        }

        public async Task<List<CompraCubos>> GetPedidosUsuarioAsync(int Id_Usuario)
        {
            return await
              this.context.Pedidos.Where(x => x.Id_Usuario == Id_Usuario).ToListAsync();
        }
        
        public async Task InsertarPedidoAsync(int Id_Pedido, int Id_Cubo, int Id_Usuario, DateTime FechaPedido)
        {
            CompraCubos pedido = new CompraCubos();
            pedido.Id_Pedido = Id_Pedido;
            pedido.Id_Cubo = Id_Cubo;
            pedido.Id_Usuario = Id_Usuario;
            pedido.FechaPedido = FechaPedido;
            this.context.Pedidos.Add(pedido);
            await this.context.SaveChangesAsync();

        }

        public async Task<Usuario> ExisteUsuarioAsync
          (string nombre, string pass)
        {
            return await
                this.context.Usuario
                .FirstOrDefaultAsync(x => x.Nombre == nombre
                && x.Pass == pass);
        }

        public async Task<int> LastPedido()
        {
            int lastPedidoId = await this.context.Pedidos
                .OrderByDescending(p => p.Id_Pedido)
                .Select(p => p.Id_Pedido)
                .FirstOrDefaultAsync();

            return lastPedidoId;
        }

    }
}
