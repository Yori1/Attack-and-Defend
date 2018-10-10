using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;
using Attack_And_Defend.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Attack_And_Defend.Controllers
{
    public class StatisticsController : Controller
    {
        PartyRepository repository;

        public StatisticsController(ApplicationDbContext context)
        {
            repository = new PartyRepository(context);
        }

        // GET: /<controller>/
        public IActionResult Overview()
        {
            Dictionary<JobNumber, int> dictionary = repository.GetAmountForEveryJob();
            StatisticsViewModel vm = new StatisticsViewModel() { ClassToNumberOfCharacters = dictionary };
            return View(vm);
        }


    }
}
