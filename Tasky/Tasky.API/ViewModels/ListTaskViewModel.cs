namespace Tasky.API.ViewModels;

public class ListTaskViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
    public int Order {  get; set; }
}
