﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortingAlgorithmVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int[] randomInts;
        private int arraySize;

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

        public MainWindow()
        {
            InitializeComponent();
            PopulateComboBox();
            DataContext = this;
            arraySize = 10;
        }

        private void PopulateComboBox()
        {
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IToDelete).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name)
                .ToList();
            ClassList.Sort();

            foreach (string entry in ClassList)
                algoNameComboBox.Items.Add(entry);

            algoNameComboBox.SelectedIndex = 0;
        }

        private void SetUpArray()
        {
            randomInts = new int[arraySize];
            Random random = new Random();
            for (int i = 0; i < randomInts.Length; i++)
                randomInts[i] = random.Next(0, (int)canvas.ActualHeight);
        }

        #region Key Down Event
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
            else if (e.Key == Key.P)
                PauseButton_Click(null, null);
            else if (e.Key == Key.A)
                AbortButton_Click(null, null);
            else if (e.Key == Key.S)
                StartButton_Click(null, null);
            else if (e.Key == Key.Left)
                arraySizeSlider.Value -= 1;
            else if (e.Key == Key.Right)
                arraySizeSlider.Value += 1;
        }
        #endregion

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DisplayArray();
        }

        private void DisplayArray()
        {
            SetUpArray();
            AddRectanglesToCanvas();
        }

        private void AddRectanglesToCanvas()
        {
            canvas.Children.Clear();
            for (int i = 0; i < randomInts.Length; i++)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Height = randomInts[i],
                    Width = canvas.ActualWidth / randomInts.Length,
                    Fill = new BrushConverter().ConvertFromString(GlobalColors.BackgroundColor) as SolidColorBrush,
                    StrokeThickness = 1,
                    Stroke = new BrushConverter().ConvertFromString(GlobalColors.StripsColor) as SolidColorBrush,
                };

                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, i * rectangle.Width);
                Canvas.SetTop(rectangle, canvas.ActualHeight - rectangle.Height);
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pause");
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Abort");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //DisplayArray();
            MessageBox.Show("Start");
        }

        #region Property Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void ArraySizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DisplayArray();
        }
    }
}