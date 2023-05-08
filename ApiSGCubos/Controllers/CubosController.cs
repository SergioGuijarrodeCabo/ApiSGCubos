using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Security.Claims;
using ApiSGCubos.Repositories;
using ApiSGCubos.Models;

namespace ApiSGCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("CUBOS")]
    public class CubosController : ControllerBase
    {

        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> GetCubos()
        {
            //USERDATA
            //HttpContext.User.FindFirst("USERDATA")
            return await this.repo.GetCubosAsync();
        }


        [HttpGet("{marca}")]
        public async Task<ActionResult<List<Cubo>>> FindCubosMarca(string marca)
        {
            return await this.repo.GetCubosMarcaAsync(marca);
        }

        //[HttpPost]
        //public async Task<ActionResult>
        //InsertUsuario(Usuario usuario)
        //{
        //    await this.repo.InsertarUsuarioAsync
        //        (usuario.Id_Usuario, usuario.Nombre, usuario.Imagen, usuario.Email, usuario.Pass);
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task<ActionResult>
        //   InsertCubo(Cubo cubo)
        //    {
        //        await this.repo.InsertarCuboAsync
        //            (cubo.Id_Cubo, cubo.Nombre, cubo.Imagen, cubo.Marca, cubo.Precio);
        //        return Ok();
        // }

        [HttpPost("usuario")]
        public async Task<ActionResult> InsertUsuario(Usuario usuario)
        {
            await this.repo.InsertarUsuarioAsync
                (usuario.Id_Usuario, usuario.Nombre, usuario.Imagen, usuario.Email, usuario.Pass);
            return Ok();
        }

        [HttpPost("cubo")]
        public async Task<ActionResult> InsertCubo(Cubo cubo)
        {
            await this.repo.InsertarCuboAsync
                (cubo.Id_Cubo, cubo.Nombre, cubo.Imagen, cubo.Marca, cubo.Precio);
            return Ok();
        }


        //[HttpGet("{id}")]
        ////[ProducesResponseType(StatusCodes.Status200OK)]
        ////[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Usuario>> DetailsUsuario(int id_usuario)
        //{
        //    var usuario = await this.repo.PerfilUsuarioAsync(id_usuario);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }
        //    return usuario;
        //}

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> PerfilUsuario()
        {
            //DEBEMOS BUSCAR EL CLAIM DEL EMPLEADO
            Claim claim = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "UserData");
            string jsonEmpleado =
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonEmpleado);
            return usuario;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<CompraCubos>>> PedidosUsuario()
        {
            //DEBEMOS BUSCAR EL CLAIM DEL EMPLEADO
            Claim claim = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "UserData");
            string jsonEmpleado =
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonEmpleado);

         
           return await this.repo.GetPedidosUsuarioAsync(usuario.Id_Usuario);
     
        }

        [Authorize]
        [HttpPost]
        [Route("[action]/{id_pedido}/{id_cubo}/{fechapedido}/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePedido(int id_pedido, int id_cubo, DateTime fechapedido)
        {
            Claim claim = HttpContext.User.Claims
          .SingleOrDefault(x => x.Type == "UserData");
            string jsonEmpleado =
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonEmpleado);

            await this.repo.InsertarPedidoAsync(id_pedido, id_cubo, usuario.Id_Usuario, fechapedido);

            return Ok();
        }
    }
}
