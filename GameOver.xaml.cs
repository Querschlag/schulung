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
using System.Windows.Shapes;

namespace Schulung
{
    /// <summary>
    /// Interaktionslogik für GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        /// <summary>
        ///  default constructor
        /// </summary>
        public GameOver()
        {
            // initialize components
            InitializeComponent();

            // deaktivate all events
            this.LabelGameOverAttack.Visibility = Visibility.Collapsed;
            this.LabelGameOverAssault.Visibility = Visibility.Collapsed;
            this.LabelGameOverEconomyCrises.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        ///  method to set game over information to attack
        /// </summary>
        public void SetInformationAttack()
        {
            // display attack information
            this.LabelGameOverAttack.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///  method to set game over information to assault
        /// </summary>
        public void SetInformationAssault()
        {
            // display assault information
            this.LabelGameOverAssault.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///  method to set game over information to economy crises
        /// </summary>
        public void SetInformationEconomyCrises()
        {
            // display economy crises information
            this.LabelGameOverEconomyCrises.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///  method called on windows closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // close application
            Application.Current.Shutdown();
        }
    }
}
