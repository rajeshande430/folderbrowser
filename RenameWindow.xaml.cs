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
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        public Item Item { get; set; }
        public RenameWindow()
        {
            InitializeComponent();
        }

        private void OnRename(object sender, RoutedEventArgs e)
        {
            var renamedText = rename_txtbox.Text;
            if (string.IsNullOrEmpty(rename_txtbox.Text))
            {
                System.Windows.MessageBox.Show("The rename text can't be empty. To rename please type the text", "Empty Name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var fullpath = (Item.Path).Replace(@"\\", @"\");
          //  System.IO.Directory.Move(fullpath, fullpath.Replace(Item.Name, renamedText));

            Item._crudtype = "U";
            Item.Path = fullpath;
            Item._oldname = Item.Name;
            Item.Name = renamedText;
            Item._rootpath = Item._rootpath.Replace(@"\\", @"\");
            var json = Item.ToElasticJSON();
            Util.ElasticRequest.POST(json);
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OnRename(sender, e);
            }
        }
    }
}
