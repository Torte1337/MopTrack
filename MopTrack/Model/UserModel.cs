using System;
using System.ComponentModel.DataAnnotations;

namespace MopTrack.Model;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string SignPath { get; set; }
    
}
