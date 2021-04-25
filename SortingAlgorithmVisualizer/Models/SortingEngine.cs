﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace SortingAlgorithmVisualizer
{
    class SortingEngine : INotifyPropertyChanged
    {
        private int[] randomInts;
        private int arraySize;
        BackgroundWorker backgroundWorker;
        private string algoName;
        private MainWindowViewModel mainWindowViewModel;

        public int ArraySize
        {
            get { return arraySize; }
            set
            {
                if (value != arraySize && value > 0 && value < 101)
                {
                    arraySize = value;
                    NotifyPropertyChanged("ArraySize");
                }
            }
        }

        public int[] RandomInts
        {
            get { return randomInts; }
            set { randomInts = value; }
        }

        public SortingEngine()
        {
            arraySize = 10;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Finish);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(Progress);
        }

        private void Progress(object sender, ProgressChangedEventArgs e)
        {
            // report the progress here...
        }

        private void Finish(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("End...");
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Type type = Type.GetType(Assembly.GetEntryAssembly().GetName().Name + "." + algoName);
            ConstructorInfo[] ctors = type.GetConstructors();

            try
            {
                    ISortAlo se = (ISortAlo)ctors[0].Invoke(new object[] { mainWindowViewModel, randomInts });
                    
                    while (!se.IsSorted())//&& (!bgw.CancellationPending))
                    {
                        //Application.Current.Dispatcher.Invoke(new Action(() => {

                            se.NextStep();
                        //}));
                        //Thread.Sleep(1000);
                    }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void Start(string algoName, MainWindowViewModel mainWindowViewModel)
        {
            this.algoName = algoName;
            this.mainWindowViewModel = mainWindowViewModel;

            if(!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        internal void SetUpArray(System.Windows.Controls.Canvas canvas)
        {
            randomInts = new int[arraySize];
            Random random = new Random();
            for (int i = 0; i < randomInts.Length; i++)
                randomInts[i] = random.Next(0, (int)canvas.ActualHeight);
        }

        #region Property Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
