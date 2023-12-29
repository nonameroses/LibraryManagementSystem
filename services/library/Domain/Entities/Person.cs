using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Abstractions;

namespace Domain.Entities;

[BsonCollection("people")]
public class Person : Document
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}