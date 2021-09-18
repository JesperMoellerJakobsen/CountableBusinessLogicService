namespace Domain.Model.Entities
{
    public interface ICounter
    {
        int Id { get; set; }
        int Value { get; set; }
        byte[] Version { get; set; }
    }
}
