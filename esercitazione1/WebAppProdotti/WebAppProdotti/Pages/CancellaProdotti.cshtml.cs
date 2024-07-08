using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAppProdotti.Pages;

namespace WebAppProdotti.Pages;

public class CancellaProdottiModel : PageModel
{
    public Prodotto? Prodotto{get; set;}

    
    
    public void OnGet(string nome)
    {
        var json = System.IO.File.ReadAllText("wwwroot/listaJson.json");
        var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json);
        Prodotto = prodotti.FirstOrDefault(p => p.Nome == nome);
        //se vogliamo trasmettere alla post il prezzo e il dettaglio per poi essere mandati alla view
        ViewData["Prezzo"] = Prodotto.Prezzo;
        ViewData["Dettaglio"] = Prodotto.Dettaglio;
        ViewData["Nome"] = Prodotto.Nome;
        //nella post possiamo usare ViewData["Prezzo"] e ViewData["Dettaglio"]
    }
        
        public IActionResult OnPost(string nome)
        {
            var json = System.IO.File.ReadAllText("wwwroot/listaJson.json");
            var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json);
            var prodotto = prodotti.FirstOrDefault(p => p.Nome == nome) ;
            prodotti.Remove(prodotto);
            //salva il file json formattato
            System.IO.File.WriteAllText("wwwroot/listaJson.json", JsonConvert.SerializeObject(prodotti, Formatting.Indented));
            return RedirectToPage("Prodotti");
        }
    }
