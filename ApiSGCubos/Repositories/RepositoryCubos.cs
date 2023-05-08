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


    }
}
