using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for NewFolderWindow.xaml
    /// </summary>
    public partial class NewFolderWindow : Window
    {
        public NewFolderWindow()
        {
            InitializeComponent();
        }

        private void OnCreateNewFolder(object sender, RoutedEventArgs e)
        {
            string foldername = newFoldername_txtbox.Text;
            string directoryPath = Browser.Object.currentdirPath;
            var item = Browser.CurrentItem;

            if (string.IsNullOrEmpty(foldername))
            {
                System.Windows.MessageBox.Show("The folder name can't be empty. To create a folder please type its name.", "Empty Name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                DirectoryInfo dir = new DirectoryInfo(directoryPath);
                var subdir = dir.CreateSubdirectory(foldername);

                item.Name = subdir.Name;
                item._rootpath = directoryPath;
                item.Path = subdir.FullName;
                item.Type = "Folder";
                item._crudtype = "C";

                string json = item.ToElasticJSON();//Util.Parser.ParseObject(item);

                // TODO: Create the index
                Util.ElasticRequest.POST(json);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "oops something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // index the elastic search
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OnCreateNewFolder(sender, null);
            }
        }
    }
}
