using System;
using System.Windows;
using System.Windows.Input;

namespace SortingAlgorithmVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel(algoNameComboBox, progressBar);
            DataContext = mainWindowViewModel;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            mainWindowViewModel.DisplayArray(canvas);
        }

        private void ArraySizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mainWindowViewModel != null && !mainWindowViewModel.SortingEngine.BackgroundWorker.IsBusy)    
                mainWindowViewModel.DisplayArray(canvas);
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
            if (!mainWindowViewModel.SortingEngine.BackgroundWorker.IsBusy)
            {
                mainWindowViewModel.DisplayArray(canvas);
                mainWindowViewModel.Start(algoNameComboBox.SelectedItem.ToString());
            }
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
    }
}
/* ***TODO****
2) TODO: Report progress
2) TODO: pause
2) TODO: abort
 */