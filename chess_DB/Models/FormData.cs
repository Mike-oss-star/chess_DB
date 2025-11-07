using System;

namespace chess_DB.Models;

public class FormData
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthdate { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    
    public override string ToString() =>
        $"{Name} {Surname} ({Gender}) - {Country}";
    
}