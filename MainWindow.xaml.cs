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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///  storage for the points
        /// </summary>
        private const int Points = 12;

        /// <summary>
        ///  storage for the game class
        /// </summary>
        private Run _run;

        /// <summary>
        ///  default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // create game class
            _run = new Run(Points);

            // set maximum of country bar
            this.SliderCountry.Maximum = Points;

            // set maximum of economy bar
            this.SliderEconomy.Maximum = Points;

            // set maximum of terror bar
            this.SliderTerror.Maximum = Points;

            // set maximum of personal bar
            this.SliderResearch.Maximum = Points;
            
            // calculate rest point
            CalculateRestPoints();
        }

        /// <summary>
        ///  method called on slider country value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderCountry_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // set text block
            this.LabelCountry.Text = this.SliderCountry.Value.ToString();

            // calculate rest points
            CalculateRestPoints();
        }

        /// <summary>
        ///  method to calculate rest point
        /// </summary>
        private void CalculateRestPoints()
        {
            // get used points
            int used = 0;

            // increase used points
            used += (int)this.SliderCountry.Value;
            used += (int)this.SliderEconomy.Value;
            used += (int)this.SliderTerror.Value;
            used += (int)this.SliderResearch.Value;

            // enable or disable button
            this.ButtonStart.IsEnabled = used == Points;
            
            // set points
            this.LabelPoints.Text = (Points - used).ToString();
        }

        /// <summary>
        ///  method called on slider economy value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderEconomy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // set text block
            this.LabelEconomy.Text = this.SliderEconomy.Value.ToString();

            // calculate rest points
            CalculateRestPoints();
        }

        /// <summary>
        ///  method called on slider terror value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderTerror_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // set text block
            this.LabelTerror.Text = this.SliderTerror.Value.ToString();

            // calculate rest points
            CalculateRestPoints();
        }

        /// <summary>
        ///  method called on slider personal value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderResearch_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // set text block
            this.LabelResearch.Text = this.SliderResearch.Value.ToString();

            // calculate rest points
            CalculateRestPoints();
        }

        /// <summary>
        ///  method called on button start clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            // call main
            _run.Main((int)this.SliderCountry.Value, (int)this.SliderEconomy.Value, (int)this.SliderTerror.Value, (int)this.SliderResearch.Value);
        }
    }
}
