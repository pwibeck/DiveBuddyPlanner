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
using System.Collections.Generic;
using DiveLibrary;
using System.Collections.ObjectModel;

namespace DiveBuddy_Planner
{
    public class DivePlanData
    {
        public Collection<WayPoint> WayPoints { get; set; }
        public Collection<Gas> DecoGas { get; set; }

        public DivePlanData()
        {
            WayPoints = new Collection<WayPoint>();
            DecoGas = new Collection<Gas>();
        }
    }
}
