namespace DiveLibrary
{
    public class Preference
    {
        public Preference()
        {
            DepthBetweenDecoStops = 3.0;
            LowGradient = 0.30;
            HighGradient = 0.80;
            DescRate = 20;
            AscRate = -10;
            MiniDepthForDeco = 3.0;
            TimeToSwitchGas = 3.0;
        }

        public double DepthBetweenDecoStops { get; set; }

        public double LowGradient { get; set; }

        public double HighGradient { get; set; }

        public double DescRate { get; set; }

        public double AscRate { get; set; }

        public double MiniDepthForDeco { get; set; }

        public double TimeToSwitchGas { get; set; }
    }
}