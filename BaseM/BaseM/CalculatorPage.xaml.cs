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
        public NumericSystem OriginalSystem { get; set; }

        List<Label> _resultLabels = new List<Label>();
        // Systems that the number must be converted to
        List<NumericSystem> _targetSystems = new List<NumericSystem>();

        public CalculatorPage()
        {
            InitializeComponent();

            OriginalSystem = NumericSystem.Decimal;
            Number = "0";
            SystemPicker.SelectedIndex = 2;
        }

        private void SystemPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            List<NumericSystem> systems = new List<NumericSystem> { NumericSystem.Binary, NumericSystem.Octal, NumericSystem.Decimal, NumericSystem.Hexadecimal };

            foreach (var sys in systems)
            {
                if (picker.Items[picker.SelectedIndex].ToLowerInvariant() == sys.ToString().ToLowerInvariant())
                    OriginalSystem = sys;
            }

            if (OriginalSystem == NumericSystem.Hexadecimal)
                numberEntry.Keyboard = Keyboard.Default;
            else
                numberEntry.Keyboard = Keyboard.Numeric;

            _targetSystems = GetTargetSystems(OriginalSystem);
            ResetLabels();
        }

        private void ResetLabels()
        {
            resultStack.Children.Clear();
            _resultLabels.Clear();

            for (int i = 0; i < _targetSystems.Count(); i++)
            {
                var name = _targetSystems[i].ToString().Substring(0, 3).ToUpper();
                var nameLabel = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    Text = name + ": ",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                };

                var valueLabel = new Label();
                valueLabel.Text = "0";
                valueLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                
                _resultLabels.Add(valueLabel);

                var grid = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = 50 }, new ColumnDefinition() },
                    Children = { nameLabel, valueLabel }
                };

                Grid.SetColumn(valueLabel, 1);
                resultStack.Children.Add(grid);
            }

            CalculateResults(Number, OriginalSystem);
        }

        private void CalculateResults(string number, NumericSystem from)
        {
            for (int i = 0; i < _targetSystems.Count; i++)
                _resultLabels[i].Text = Translator.Translate(number, from, _targetSystems[i]);
        }

        private void numberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Number = numberEntry.Text;
            CalculateResults(Number, OriginalSystem);
        }

        private List<NumericSystem> GetTargetSystems(NumericSystem selectedSystem)
        {
            List<NumericSystem> systems = new List<NumericSystem> { NumericSystem.Binary, NumericSystem.Octal, NumericSystem.Decimal, NumericSystem.Hexadecimal };
            var returnList = new List<NumericSystem>();

            foreach (var sys in systems)
            {
                if (sys == selectedSystem)
                    continue;
                returnList.Add(sys);
            }

            return returnList;
        }
    }
}
