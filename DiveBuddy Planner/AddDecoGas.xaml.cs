using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using DiveLibrary;
using System.Collections.ObjectModel;

namespace DiveBuddy_Planner
{
    public partial class AddDecoGas : PhoneApplicationPage
    {
        bool enabled = false;
        private int helium, oxygen;

        public AddDecoGas()
        {
            InitializeComponent();

            enabled = true;
            this.helium = (int)SelectedHelium.Value;
            this.oxygen = (int)SelectedOxygen.Value;
            this.UpdateMod();
        }

        private bool ValidateOxygenHeliumRate(int o2, int h)
        {
            if (o2 + h > 100)
            {
                MessageBox.Show("Oxygen plus helium can not be bigger than 100%");
                return false;
            }

            return true;
        }
        
        private void oxygen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   
            if (enabled)
            {            
                int tmp = (int)Math.Round(e.NewValue);
                if(ValidateOxygenHeliumRate(tmp, this.helium))
                {
                    this.oxygen = tmp;
                    this.oxygenView.Text = this.oxygen+ " %";                
                    this.UpdateMod();
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {           
            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            if (data == null)
            {
                data = new DivePlanData();
            }

            data.DecoGas.Add(new Gas((double)this.oxygen / 100.0, (double)this.helium / 100.0));

            SerializerHelper.Serialize<DivePlanData>("DivePlanData.xml", data);
            this.NavigationService.GoBack();
        }

        private void helium_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (enabled)
            {
                int tmp = (int)Math.Round(e.NewValue);
                if (ValidateOxygenHeliumRate(this.oxygen, tmp))
                {
                    this.helium = tmp;
                    this.heliumView.Text = this.helium + " %";
                    this.UpdateMod();
                }                
            }
        }

        private void UpdateMod()
        {
            var decoDepth = DiveLibrary.Calculations.MaximumOperatingDepth(this.oxygen, 1.6);
            this.mod.Text = "MOD " + decoDepth + "m";
        }
    }
}