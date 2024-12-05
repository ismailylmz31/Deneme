using Core.Persistence.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechCareer.Models.Entities;

public class Category : Entity<int>
{
    public object Event;

    public string Name { get; set; }

    // Navigation Property
    public ICollection<Event> Events { get; set; } = new List<Event>();

}