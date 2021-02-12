using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KonacniProjekat
{
    public class OdjavaModel : PageModel
    {
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }          
            else
            {
                SessionClass.SessionId=null;
                SessionClass.TipKorisnika=null;
               
                return RedirectToPage("./Index");
            }
        }
    }
}
