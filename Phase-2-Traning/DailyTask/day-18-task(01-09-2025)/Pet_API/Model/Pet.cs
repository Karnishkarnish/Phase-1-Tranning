namespace PetApi.Models;

public class Pet
{
    public int Id { get; set; }
    public string Breed { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
}
