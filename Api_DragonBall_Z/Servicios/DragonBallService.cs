using System.Collections.Generic;
using System.Threading.Tasks;
using Api_DragonBall_Z.Models;

namespace Api_DragonBall_Z.Servicios
{
    public interface IDragonBallService
    {
        Task<DragonBallResponse> GetCharacterPage(int page = 1, int limit = 12);
        Character ObtenerPersonaje(int id);
        Task<List<Character>> SearchCharacters(string search);
    }
}
