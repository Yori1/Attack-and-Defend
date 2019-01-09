using System;
using System.Collections.Generic;
using System.Text;

namespace Attack_And_Defend.Models
{
    public class StatisticsViewModel
    {
        public StatisticsViewModel(Dictionary<JobNumber, int> classToNumberOfCharacters)
        {
            ClassToNumberOfCharacters = classToNumberOfCharacters;
        }

        public Dictionary<JobNumber, int> ClassToNumberOfCharacters { get; private set; }
    }
}
