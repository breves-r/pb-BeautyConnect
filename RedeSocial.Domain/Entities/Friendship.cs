using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Entities
{
    public class Friendship
    {
        public Guid IdProfileA { get; set; }
        public Profile ProfileA { get; set; }

        public Guid IdProfileB { get; set; }
        public Profile ProfileB { get; set; }

        public Friendship() { }

        public Friendship(Guid idProfileA, Profile profileA, Guid idProfileB, Profile profileB)
        {
            IdProfileA = idProfileA;
            ProfileA = profileA;
            IdProfileB = idProfileB;
            ProfileB = profileB;
        }

        public override string ToString()
        {
            return ProfileA.Nome + " - " + ProfileB.Nome;
        }
    }
}
