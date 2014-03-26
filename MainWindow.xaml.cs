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

            // change points
            OnChangePoints();
        }

        /// <summary>
        ///  method called on changed points
        /// </summary>
        public void OnChangePoints()
        {
            // set maximum points for country
            this.SetMaximumCountry();

            // set maximum points for economy
            this.SetMaximumEconomy();

            // set maximum points for terror
            this.SetMaximumTerror();

            // set maximum points for research
            this.SetMaximumResearch();

            // calculate sum
            int sum = this.PointsCountry.Value + this.PointsEconomy.Value + this.PointsTerror.Value + this.PointsResearch.Value;

            // auto disable or enable next round button
            this.ButtonStart.IsEnabled = sum == Points;
        }

        /// <summary>
        ///  method to set maximum points for country
        /// </summary>
        private void SetMaximumCountry()
        {
            // set maximum
            this.PointsCountry.Maximum = Points - (this.PointsEconomy.Value + this.PointsTerror.Value + this.PointsResearch.Value);
        }

        /// <summary>
        ///  method to set maximum points for economy
        /// </summary>
        private void SetMaximumEconomy()
        {
            // set maximum
            this.PointsEconomy.Maximum = Points - (this.PointsCountry.Value + this.PointsTerror.Value + this.PointsResearch.Value);
        }

        /// <summary>
        ///  method to set maximum points for terror
        /// </summary>
        private void SetMaximumTerror()
        {
            // set maximum
            this.PointsTerror.Maximum = Points - (this.PointsCountry.Value + this.PointsEconomy.Value + this.PointsResearch.Value);
        }

        /// <summary>
        ///  method to set maximum points for research
        /// </summary>
        private void SetMaximumResearch()
        {
            // set maximum
            this.PointsResearch.Maximum = Points - (this.PointsCountry.Value + this.PointsEconomy.Value + this.PointsTerror.Value);
        }

        /// <summary>
        ///  method called on button start clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            // call main
            // _run.Main((int)this.SliderCountry.Value, (int)this.SliderEconomy.Value, (int)this.SliderTerror.Value, (int)this.SliderResearch.Value);
        }
    }
}
