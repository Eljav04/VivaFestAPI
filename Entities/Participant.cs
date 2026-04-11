using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaFestAPI.Entities;

public class Participant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Plate { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsWinner { get; set; } = false;
}
