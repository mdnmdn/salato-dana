using System.Linq;
using System.Windows;
using BlueChips.DanaManager.MainApp.Libs;
using GalaSoft.MvvmLight.Messaging;
using BlueChips.DanaManager.MainApp.Models;


namespace BlueChips.DanaManager.MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<ErrorMessage>(this,m => ShowError(m.Message));
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true)) {
                var files = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                var excel = files.FirstOrDefault(f => System.IO.Path.GetExtension(f).ToLower().Contains("xls"));
                if (excel != null){
                    Messenger.Default.Send<ImportMessage>(new ImportMessage { FilePath =excel });
                }
            }
        }

        private void OnFileDragging(object sender, DragEventArgs e)
        {
            var dropEnabled = false;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true)) {
                var files = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                dropEnabled = files.Any(f => System.IO.Path.GetExtension(f).ToLower().Contains("xls"));
            }

            if (!dropEnabled) {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
            
        }

        
    }
}
