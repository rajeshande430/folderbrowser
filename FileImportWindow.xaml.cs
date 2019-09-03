using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Folder_Browser
{
    /// <summary>
    /// Interaction logic for FileImportWindow.xaml
    /// </summary>
    public partial class FileImportWindow : Window
    {
       
        public FileImportWindow(string filepath, string currentDirectory)
        {
            InitializeComponent();

            filename_textblock.Text = System.IO.Path.GetFileName(filepath);
            directorypath_textblock.Text = currentDirectory;
        }
       

        private void OnUploadfile(object sender, RoutedEventArgs e)
        {

        }
    }
}
