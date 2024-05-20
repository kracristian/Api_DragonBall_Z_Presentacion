using Api_DragonBall_Z.Models;
using Api_DragonBall_Z.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDragonBallService _api_DragonBall;

    public HomeController(ILogger<HomeController> logger, IDragonBallService api_DragonBall)
    {
        _logger = logger;
        _api_DragonBall = api_DragonBall;
    }

    public async Task<IActionResult> Index(int page = 1, int itemsPerPage = 12, string search = "")
    {
        if (search != "")
        {
            List<Character> characterResponse = await _api_DragonBall.SearchCharacters(search);
            return View(characterResponse);
        }
        else
        {
            DragonBallResponse characterResponse = await _api_DragonBall.GetCharacterPage(page, itemsPerPage);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(characterResponse.Meta.TotalItems / (double)itemsPerPage);
            return View(characterResponse.Items);
        }
    }

    public IActionResult PeronsajeId(int personaje = 1)
    {
        Character character = _api_DragonBall.ObtenerPersonaje(personaje);
        return View(character);
    }

}
