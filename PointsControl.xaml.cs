using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schulung
{
    /// <summary>
    ///  event delegate for the value change event
    /// </summary>
    public delegate void OnValueChanged();

    /// <summary>
    /// Interaktionslogik für Points.xaml
    /// </summary>
    public partial class PointsControl : UserControl
    {
        /// <summary>
        ///  storage for the value
        /// </summary>
        private int _value = 0;

        /// <summary>
        ///  storage for the value
        /// </summary>
        public int Value
        {
            get { return this._value; }
            private set
            {
                // store value
                this._value = value;

                // initialize points control new
                Init();
            }
        }

        /// <summary>
        ///  storage for the maximum
        /// </summary>
        private int _maximum = 0;

        /// <summary>
        ///  storage for the maximum
        /// </summary>
        public int Maximum 
        {
            get { return this._maximum; }
            set
            {
                // store value
                this._maximum = value;
                
                // initialize points control new
                Init();
            }
        }

        /// <summary>
        ///  default constructor
        /// </summary>
        public PointsControl()
        {
            // initialize components
            InitializeComponent();

            // initialize points control new
            Init();
        }

        /// <summary>
        ///  event for the value changed event handler
        /// </summary>
        public event OnValueChanged ValueChanged;

        /// <summary>
        ///  method called on button plus clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            // check value
            if ((Value + 1) <= Maximum)
            {
                // increase value
                Value++;

                // check and raise event handler
                if (this.ValueChanged != null) this.ValueChanged();
            }
        }

        /// <summary>
        ///  method called on button minus clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            // check value
            if ((Value - 1) >= 0)
            {
                // decrease value
                Value--;

                // check and raise event handler
                if (this.ValueChanged != null) this.ValueChanged();
            }
        }

        /// <summary>
        ///  method to initialize points control new
        /// </summary>
        private void Init()
        {
            // auto disable or enable buttons
            this.ButtonMinus.IsEnabled = Value > 0;
            this.ButtonPlus.IsEnabled = Value < Maximum;

            // set value
            this.LabelPoints.Text = Value.ToString("0");
        }

        /// <summary>
        ///  method to reset all
        /// </summary>
        public void Reset()
        {
            // reset value
            Value = 0;

            // reset maximum
            Maximum = 0;
        }
    }
}
