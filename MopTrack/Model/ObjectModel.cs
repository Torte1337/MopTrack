using System;
using System.ComponentModel.DataAnnotations;

namespace MopTrack.Model;

public class ObjectModel
{
    [Key]
    public Guid Id { get; set; }
    public required string ObjectName { get; set; }
    public required string ObjectLocation { get; set; }
    public required float ObjectTime { get; set; }
    
}
