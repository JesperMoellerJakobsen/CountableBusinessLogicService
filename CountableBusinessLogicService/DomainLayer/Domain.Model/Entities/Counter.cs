namespace Domain.Model.Entities
{
    public class Counter : ICounter
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public byte[] Version { get; set; }
    }
}
