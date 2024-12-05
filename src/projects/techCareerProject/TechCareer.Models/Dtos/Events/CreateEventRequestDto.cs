using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCareer.Models.Dtos.Events;

public sealed record CreateEventRequestDto(


    string Title,
    string Description,
    string ImageUrl,
    string ParticipationText,
    int CategoryId
    );