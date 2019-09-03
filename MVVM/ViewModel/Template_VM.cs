using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Browser
{
    public class Template_VM : ObservableBase
    {

        private string _nametemplate;
        public string NameTemplate
        {
            get { return _nametemplate; }
            set { SetField(nameof(NameTemplate), ref _nametemplate, value); }
        }
        private ObservableCollection<Folder> _folderstructure;
        public ObservableCollection<Folder> FolderStructure
        {
            get { return _folderstructure; }
            set { SetField(nameof(FolderStructure), ref _folderstructure, value); }
        }



        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set { SetField(nameof(Items), ref items, value); }
        }




        public Template_VM()
        {
            FolderStructure = new ObservableCollection<Folder>();
        }

        public void InsertNestedFolder(string foldername, Folder parentfolder)
        {
            //var nestedfolder = new Folder { NameFolder = foldername };
            //parentfolder.NestedFolders.Add(nestedfolder);
        }

        /// <summary>
        /// Remove folder recursively
        /// </summary>
        /// <param name="folder"></param>
        private void RemoveFolderRecusively(ObservableCollection<Folder> folders, Folder folderToBeRemove)
        {
            if (folders.Any())
            {
                folders.Remove(folderToBeRemove);
                foreach (var folder in folders)
                {
                    //RemoveFolderRecusively(folder.NestedFolders, folderToBeRemove);
                }

            }
        }

        public void RemoveFolder(Folder folderToBeRemove)
        {
            RemoveFolderRecusively(FolderStructure, folderToBeRemove);
        }

        public void GenerateFolders(string directory)
        {
            //GenerateFoldersRecursively(FolderStructure, directory);
            System.Windows.MessageBox.Show("Folder structure has been created succussfully", "Succussful", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private void GenerateFoldersRecursively(List<Folder> folders, string directory)
        {
            if (folders.Any())
            {
                foreach (var folder in folders)
                {
                   // Util.Folder.GenerateFolder(directory, folder.Name);
                }
                foreach (var folder in folders)
                {
                    var subfolders = folder.NestedFolders;
                    var subdirectory = directory + "\\" + folder.Name;

                    GenerateFoldersRecursively(subfolders, subdirectory);
                }


            }
        }

        public void InsertFolder(string foldername)
        {
            //var folder = new Folder { NameFolder = foldername };
            //FolderStructure.Add(folder);
        }

    }
}
