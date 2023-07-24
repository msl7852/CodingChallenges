namespace iA_CodingChallenge
{
    public class Medication
    {
        public readonly double Cost;
        public readonly string Name;

        public Medication(string name, int minValue, int maxValue)
        {
            Name = name;
            Random rnd = new Random();
            Cost = rnd.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
