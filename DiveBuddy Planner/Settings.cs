using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DiveBuddy_Planner
{
    public class Settings
    {
        public enum AlgoritmType
        {
            ZH16B,
            ZH17B
        }

        public AlgoritmType Algoritm { get; set; }

        public double HighGraident { get; set; }

        public double LowGraident { get; set; }

        public double TimeToSwitchGas { get; set; }
        
        public double MiniDepthForDeco { get; set; }

        public Settings()
        {
            this.Algoritm = AlgoritmType.ZH16B;
            this.HighGraident = 0.8;
            this.LowGraident = 0.3;
            this.TimeToSwitchGas = 3.0;
            this.MiniDepthForDeco = 3.0;
        }
    }
}
