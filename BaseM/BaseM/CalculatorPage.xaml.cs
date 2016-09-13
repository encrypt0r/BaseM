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
        public string Number { get; set; }
        public NumericSystem System { get; set; }
        List<Label> labels = new List<Label>();

        public CalculatorPage()
        {
            InitializeComponent();

            System = NumericSystem.Decimal;
            Number = "0";
            SystemPicker.SelectedIndex = 2;
            ResetLabels(System);
        }

        private void SystemPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            switch (picker.Items[picker.SelectedIndex].ToLowerInvariant())
            {
                case "binary":
                    System = NumericSystem.Binary;
                    break;
                case "octal":
                    System = NumericSystem.Octal;
                    break;
                case "decimal":
                    System = NumericSystem.Decimal;
                    break;
                case "hexadecimal":
                    System = NumericSystem.Hexadecimal;
                    break;
            }

            if (System == NumericSystem.Hexadecimal)
                numberEntry.Keyboard = Keyboard.Chat;
            else
                numberEntry.Keyboard = Keyboard.Numeric;
            ResetLabels(System);
        }

        private void ResetLabels(NumericSystem system)
        {
            string[] names = { "BIN", "OCT", "DEC", "HEX" };
            string sysName = system.ToString().Substring(0, 3).ToUpper();

            resultStack.Children.Clear();
            labels.Clear();

            for (int i = 0; i < names.Length; i++)
            {
                var name = names[i];
                if (name == sysName) continue;

                var nameLabel = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    Text = name + ": "
                };

                var valueLabel = new Label();
                valueLabel.Text = "0";

                labels.Add(valueLabel);

                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = 50 },
                        new ColumnDefinition()
                    },
                    Children = { nameLabel, valueLabel }
                };

                Grid.SetColumn(valueLabel, 1);
                resultStack.Children.Add(grid);
            }

            CalculateResults(Number, System);
        }

        private void CalculateResults(string number, NumericSystem from)
        {
            List<NumericSystem> systems = new List<NumericSystem> { NumericSystem.Binary, NumericSystem.Octal, NumericSystem.Decimal, NumericSystem.Hexadecimal };

            for (int i = 0; i < 3; i++)
            {
                if (systems[i].ToString() == from.ToString())
                    continue;
                labels[i].Text = Translator.Translate(number, from, systems[i]);
            }
        }

        private void numberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Number = numberEntry.Text;
            CalculateResults(Number, System);
        }
    }
}
