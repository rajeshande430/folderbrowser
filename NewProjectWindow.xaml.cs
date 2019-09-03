using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        public NewProjectWindow()
        {
            InitializeComponent();
        }

        private void OnCreateNewProject(object sender, RoutedEventArgs e)
        {
            string projectname = newFoldername_txtbox.Text;
            string directoryPath = @"O:\IT\4th Door\Ali Asad\Example\";

            if (string.IsNullOrEmpty(projectname))
            {
                System.Windows.MessageBox.Show("The project name can't be empty. To create a project please type its name.", "Empty Name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var folderStructure = Util.Template.GetProjectTemplate_Folderstructure(projectname);
                Util.Folder.GenerateFolderStructure(folderStructure, directoryPath);
                Process.Start(directoryPath + "\\" + folderStructure.Name + "\\");



                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "oops something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OnCreateNewProject(sender, null);
            }
        }
    }
}
