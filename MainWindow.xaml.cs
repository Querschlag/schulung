﻿using System;
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

            // create game class
            _game = new Game(Points);

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
        }

        /// <summary>
        ///  method called on button start clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
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

            // change points
            OnChangePoints();
        }
    }
}
