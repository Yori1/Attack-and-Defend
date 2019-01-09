using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attack_And_Defend.Models;
using Attack_And_Defend.Logic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Attack_And_Defend.Controllers
{
    public class StatisticsController : Controller
    {
        StatisticsHandler statisticsHandler;

        public StatisticsController(StatisticsHandler statisticsHandler)
        {
            this.statisticsHandler = statisticsHandler;
        }

        // GET: /<controller>/
        public IActionResult Overview()
        {
            Dictionary<JobNumber, int> dictionary = statisticsHandler.GetAmountForEveryJob();
            StatisticsViewModel vm = new StatisticsViewModel(dictionary);
            return View(vm);
        }


    }
}
