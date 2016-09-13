using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BaseM
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void okayButton_Clicked(object sender, EventArgs e)
        {
            resultLabel.Text = Translator.Translate(numberEntry.Text, NumericSystem.Hexadecimal, NumericSystem.Octal);
        }
    }
}
