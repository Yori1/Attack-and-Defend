using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attack_And_Defend.Models
{
    public class TestableUser
    {
        public string UserName { get; private set; }
        public List<Party> Parties { get; private set; } = new List<Party>();
        public int Id { get; private set; }

        public TestableUser(string username, int Id)
        {
            UserName = username;
            this.Id = Id;
        }

    }
}
