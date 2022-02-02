using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseP.models
{
    public class MoviesActors
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        [StringLength(75)]
        public string? Character { get; set; }
        public int Order { get; set; }
        public Actor? Actor { get; set; }
        public Movie? Movie { get; set; }
    }
}
