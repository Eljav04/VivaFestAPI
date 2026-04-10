namespace VivaFestAPI.Entities;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
