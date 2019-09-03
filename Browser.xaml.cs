using Microsoft.Win32;
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
using static System.Net.WebRequestMethods;

namespace Folder_Browser
{
    /// <summary>
    /// Interaction logic for Browser.xaml
    /// </summary>
    public partial class Browser : Window
    {
        public static Browser Object;
        public string currentdirPath = "";
        public static Item CurrentItem = null;

        public Browser()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            Object = this;
            drive_cmbx.ItemsSource = Data.GetAllDrives();



        }
        private void OnDriveChange(object sender, SelectionChangedEventArgs e)
        {
            Drive drive = (sender as ComboBox).SelectedItem as Drive;
            if (drive == null) return;

            currentdirPath = drive.Path;
            filesfolders_listview.ItemsSource = Data.GetDirectory(drive.Path);



            CurrentItem = BrowserHelper.CloneCurrentDirectoryInfo();



            dirAddress_txtblock.Text = BrowserHelper.GetDirectoryAddress();
        }



        private void OnDirectoryChange(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (sender as ListView).SelectedItem as Item;
            if (selectedItem == null) return;

            if (selectedItem.Path.EndsWith(@"\\"))
            {
                currentdirPath = selectedItem.Path + selectedItem.Name;
                filesfolders_listview.ItemsSource = Data.GetDirectory(currentdirPath);
                dirAddress_txtblock.Text = BrowserHelper.GetDirectoryAddress();
            }
            else if (selectedItem.Type.Equals("Folder"))
            {

                currentdirPath = selectedItem.Path + @"\\" + selectedItem.Name;
                filesfolders_listview.ItemsSource = Data.GetDirectory(currentdirPath);
                CurrentItem = BrowserHelper.CloneCurrentDirectoryInfo(selectedItem);
                dirAddress_txtblock.Text = BrowserHelper.GetDirectoryAddress();
            }
            else
            {
                // Open the file with a respective application
                // Remove this code later from below line: .Replace(@"O:\", @"\\acdxbfs1\Organisation\").Replace(selectedItem.Name + "\\", "");
                string filepath = (selectedItem.Path + @"\\" + selectedItem.Name).Replace(@"\\", @"\").Replace(@"O:\", @"\\acdxbfs1\Organisation\").Replace(selectedItem.Name + "\\", "");
                try
                {

                    // to open specific page on pdf
                    // process.StartInfo.Arguments = "/A \"page=n\" \"F:\\STAGE\\test.pdf"";

                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                    myProcess.StartInfo.Arguments = string.Format("/A \"page={0}\" \"{1}\"", selectedItem.PageNo, filepath);
                    myProcess.Start();


                    // System.Diagnostics.Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnNavigateToPreviousDirectory(object sender, RoutedEventArgs e)
        {

            if (currentdirPath.EndsWith(@"\\"))
            {
                filesfolders_listview.ItemsSource = Data.GetDirectory(currentdirPath);
                return;
            }


            currentdirPath = currentdirPath.Remove(currentdirPath.LastIndexOf('\\'));
            var drive = BrowserHelper.GetCurrentDrive();


            // check if the currentdirectory is the drive directory path
            if ((currentdirPath + @"\").Equals((drive).Path))
            {
                currentdirPath = currentdirPath + @"\";
            }
            else
            {
                currentdirPath = currentdirPath.Remove(currentdirPath.LastIndexOf('\\'));
            }

            filesfolders_listview.ItemsSource = Data.GetDirectory(currentdirPath);
            dirAddress_txtblock.Text = BrowserHelper.GetDirectoryAddress();
        }

        private void OnSearchFileFolder(object sender, KeyEventArgs e)
        {
            string searchword = CommentTextBox.Text;
            if (e.Key == System.Windows.Input.Key.Enter)
            {

                if (searchtype_cmbx.SelectedIndex <= 0) // File/Folder search
                {
                    filesfolders_listview.ItemsSource = Util.ElasticRequest.GetSearchResult(currentdirPath, searchword);
                }
                else // Content Search
                {
                    filesfolders_listview.ItemsSource = Util.ElasticRequest.GetContentSearchResult(currentdirPath, searchword);
                }
            }
            else if (e.Key == System.Windows.Input.Key.Back)
            {
                if (string.IsNullOrEmpty(searchword))
                {
                    filesfolders_listview.ItemsSource = Data.GetDirectory(currentdirPath);
                    return;
                }
            }
        }


        private void OnOpenCreateNewFolderWindow(object sender, RoutedEventArgs e)
        {
            var window = new NewFolderWindow();
            window.ShowDialog();
        }

        private void OnOpenFolderPermissionWindow(object sender, RoutedEventArgs e)
        {
            var directory = dirAddress_txtblock.Text;
            Util.Access.ShowAccessPermissionWindow(directory);
        }

        private void OnOpenCreateNewProjectWindow(object sender, RoutedEventArgs e)
        {


            //string domain = "archcorpdxb";
            //string userName = "ali.asad";//"dms.archcorp";
            //string password = "Aasad1017";

            //string filepath = "\\\\acdxbfs1\\Organization\\Admin\\Advertisement\\Archcorp ad format\\2010\\Ads.jpg";
            //try
            //{

            //    using (var impersonation = new Util.ImpersonateUser(userName, domain, password, Util.ImpersonateUser.LOGON32_LOGON_NEW_CREDENTIALS))
            //    {
            //        System.IO.FileInfo oFileInfo = new System.IO.FileInfo(filepath);

            //        System.Security.AccessControl.FileSecurity fileSecurity = System.IO.File.GetAccessControl(filepath);
            //        System.Security.Principal.IdentityReference sid = fileSecurity.GetOwner(typeof(System.Security.Principal.SecurityIdentifier));
            //        System.Security.Principal.NTAccount ntAccount = sid.Translate(typeof(System.Security.Principal.NTAccount)) as System.Security.Principal.NTAccount;
            //        string owner = ntAccount.Value;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            var window = new NewProjectWindow();
            window.ShowDialog();
        }

        private void OnOpenImportFileWindow(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.
            if (openFileDialog.ShowDialog() == true)
            {
                var window = new FileImportWindow((openFileDialog.FileName), BrowserHelper.GetDirectoryAddress());
                window.ShowDialog();

            }
        }

        private void OnOpenRenameWindow(object sender, RoutedEventArgs e)
        {
            if (CurrentItem == null) return;

            var window = new RenameWindow();
            window.Item = CurrentItem;
            window.ShowDialog();
        }
    }


    public class BrowserHelper
    {
        public static Item CloneCurrentDirectoryInfo(Item item = null)
        {
            var items = Browser.Object.filesfolders_listview.Items;

            if (items.Count < 1) return null;

            var firstItem = (Item)Browser.Object.filesfolders_listview.Items[0];

            var cloned = new Item
            {
                _id = firstItem._id,
                _rootpath = firstItem._rootpath,
                _index = firstItem._index,
                _type = firstItem._type,
            };


            if (item != null)
            {
                cloned.Name = item.Name;
                cloned._rootpath = item.Path;
                cloned.Path = item.Path + @"\\" + item.Name;
                cloned.Description = item.Description;
                cloned.FileExtension = item.FileExtension;
                cloned.PageNo = item.PageNo;
                cloned.Sentence = item.Sentence;
                cloned.SubItems = item.SubItems;
            }

            return cloned;


        }
        public static string GetDirectoryAddress()
        {
            var drive = GetCurrentDrive();
            string directoryaddress = Browser.Object.currentdirPath.Replace(drive.Path, drive.Name + @":\").Replace(@"\\", "\\");
            return directoryaddress;
        }

        public static Drive GetCurrentDrive()
        {
            return (Browser.Object.drive_cmbx.SelectedItem as Drive) ?? Browser.Object.drive_cmbx.Items[1] as Drive;
        }
    }

    #region Resource

    [ValueConversion(typeof(string), typeof(object))]
    public sealed class StringHighlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            if (input != null)
            {
                var textBlock = new TextBlock();
                textBlock.TextWrapping = TextWrapping.Wrap;


                var startingtext = "<em>";
                var endingtext = "</em>";
                while (input.IndexOf(startingtext) != -1)
                {
                    //up to startingtext is normal
                    textBlock.Inlines.Add(new Run(input.Substring(0, input.IndexOf(startingtext))));
                    //between startingtext and endingtext is highlighted
                    textBlock.Inlines.Add(new Run(input.Substring(input.IndexOf(startingtext) + 4,
                                              input.IndexOf(endingtext) - (input.IndexOf(startingtext) + 4)))
                    { FontWeight = FontWeights.Bold, Background = Brushes.Yellow });
                    //the rest of the string (after the endingtext)
                    input = input.Substring(input.IndexOf(endingtext) + 5);
                }

                if (input.Length > 0)
                {
                    textBlock.Inlines.Add(new Run(input));
                }
                return textBlock;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("This converter cannot be used in two-way binding.");
        }

    }

    #endregion

}
