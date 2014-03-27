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
using Schulung.Logic;
using System.Windows.Media.Animation;

namespace Schulung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///  flag to test button responses
        /// </summary>
        private bool pointsControlResponseEnabled;

        /// <summary>
        ///  index for last test response output
        /// </summary>
        private int lastTestResponseIndex;

        /// <summary>
        ///  storage for the points
        /// </summary>
        private const int Points = 11;

        /// <summary>
        ///  storage for the year
        /// </summary>
        private int _year;

        /// <summary>
        ///  storage for the game class
        /// </summary>
        private IGame _game;

        /// <summary>
        ///  storage for the check class
        /// </summary>
        private Check _check;

        /// <summary>
        ///  storage for the game over dialog
        /// </summary>
        private GameOver _gameOverDialog;

        /// <summary>
        ///  default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // set default year
            _year = DateTime.Now.Year;

            // create game class
            _game = new Game(Points);

            // set event hanlder
            _game.Message += OnMessage;

            // create check class
            _check = new Check();

            // set events
            _check.Attack += OnAttack;
            _check.Assault += OnAssault;
            _check.EconomyCrises += OnEconomyCrises;

            // change points
            OnChangePoints();
        }

        /// <summary>
        ///  method called on message
        /// </summary>
        /// <param name="message"> message to show </param>
        private void OnMessage(string message)
        {
            // show message in message box
            MessageBox.Show(message, "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        ///  method called on economy crises
        /// </summary>
        private void OnEconomyCrises()
        {
            // check if game over dialog exist
            if (this._gameOverDialog == null)
            {
                // create game over dialog
                this._gameOverDialog = new GameOver();

                // show dialog
                this._gameOverDialog.Show();

                // hide current window
                this.Hide();
            }

            // set economy crises information
            this._gameOverDialog.SetInformationEconomyCrises();
        }

        /// <summary>
        ///  method called on assault
        /// </summary>
        private void OnAssault()
        {
            // check if game over dialog exist
            if (this._gameOverDialog == null)
            {
                // create game over dialog
                this._gameOverDialog = new GameOver();

                // show dialog
                this._gameOverDialog.Show();

                // hide current window
                this.Hide();
            }

            // set assault information
            this._gameOverDialog.SetInformationAssault();
        }

        /// <summary>
        ///  method called on attack
        /// </summary>
        private void OnAttack()
        {
            // check if game over dialog exist
            if (this._gameOverDialog == null)
            {
                // create game over dialog
                this._gameOverDialog = new GameOver();

                // show dialog
                this._gameOverDialog.Show();

                // hide current window
                this.Hide();
            }

            // set attack information
            this._gameOverDialog.SetInformationAttack();
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

            // set points left
            this.LabenPoints.Text = (Points - sum).ToString("0");

            // set current year
            this.LabelYear.Text = this._year.ToString("0000");

            // auto disable or enable next round button
            this.ButtonStart.IsEnabled = sum <= Points;
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

            if (this.pointsControlResponseEnabled)
            {
                // update label to test input events
                String response = "";
                //new Random().Next(0, 4)
                switch (this.lastTestResponseIndex)
                {
                    case 0: response = "BÄM! Event!";
                        break;
                    case 1: response = "Du hast mich geklickt?";
                        break;
                    case 2: response = "Can't touch this";
                        break;
                    case 3: response = "He, lass das!";
                        break;
                    case 4: response = "Autsch";
                        break;
                }

                if (this.lastTestResponseIndex < 4)
                {
                    this.lastTestResponseIndex++;
                }
                else
                {
                    this.lastTestResponseIndex = 0;
                }
                this.PointsControlTestLabel.Text = response;
                
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.AutoReverse = false;
                da.Duration = new Duration(TimeSpan.FromSeconds(0.3));
                this.PointsControlTestTextBlock.BeginAnimation(OpacityProperty, da);
            }
        }

        /// <summary>
        ///  method called on button start clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            // calculate sum
            int sum = this.PointsCountry.Value + this.PointsEconomy.Value + this.PointsTerror.Value + this.PointsResearch.Value;

            // check sum 
            if (sum == Points)
            {
                // go to next round
                NextRound();
            }
            else if(MessageBox.Show("Sie haben ihr Budget noch nicht komplett verteilt!\nWollen Sie trotzdem fortfahren?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                // go to next round
                NextRound();
            }
        }

        /// <summary>
        ///  method to go to next round
        /// </summary>
        private void NextRound()
        {
            // call main
            _game.Run(PointsCountry.Value, PointsEconomy.Value, PointsTerror.Value, PointsResearch.Value);

            // check current state
            _check.CheckCurrentState(_game);

            // reset points country
            this.PointsCountry.Reset();

            // reset points economy
            this.PointsEconomy.Reset();

            // reset points terror
            this.PointsTerror.Reset();

            // reset points research
            this.PointsResearch.Reset();

            // increase year
            this._year++;

            // change points
            OnChangePoints();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PointsControlTestLabel.Text = "";
            this.pointsControlResponseEnabled = !this.pointsControlResponseEnabled;
        }
    }
}
