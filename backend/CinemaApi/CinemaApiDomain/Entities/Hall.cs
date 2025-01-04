namespace CinemaApiDomain.Entities
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        private int capacity;
        public int Capacity 
        {
            get => capacity;
            set
            {
                if (Math.Sqrt(value) % 1 == 0)
                {
                    capacity = value;
                }
                else
                {
                    throw new Exception($"Value: {value} is not a perfect square.");
                }
            }
        }

        public List<Seance> Seances { get; set; } = new();
    }
}
