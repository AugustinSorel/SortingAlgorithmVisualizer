﻿using System;
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
    public partial class MainWindow : Window
    {
        private int[] randomInts;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetUpArray()
        {
            randomInts = new int[10];
            Random random = new Random();
            for (int i = 0; i < randomInts.Length; i++)
                randomInts[i] = random.Next(0, (int)canvas.ActualHeight);
        }

        #region Key Down Event
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
        }
        #endregion

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SetUpArray();
            DisplayTheArrayWithRectangles();
        }

        private void DisplayTheArrayWithRectangles()
        {
            for (int i = 0; i < randomInts.Length; i++)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Height = randomInts[i],
                    Width = canvas.ActualWidth / randomInts.Length,
                    Fill = Brushes.White,
                    StrokeThickness = 1,
                    Stroke = Brushes.Green,
                };

                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, i * rectangle.Width);
                Canvas.SetTop(rectangle, canvas.ActualHeight - rectangle.Height);
            }
        }
    }
}