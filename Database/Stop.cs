using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.Database

{
    public class Stop
    {
        public int Id { get; set; }

        public int SeatsAtStop { get; set; }

        public int LocationId { get; set; }

        public int StopIndex { get; set; }

    }
}
