namespace AkkaSystem
{
    public class WriteNumber
    {
        public WriteNumber(int number)
        {
            Number = number;
        }

        public int Number { get; }
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}