using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Browser
{
    public static class Data
    {
        public static List<Template_VM> Templates { get; set; }
        //public static List<FolderTemplate> FolderTemplates { get; set; }

        public static void RefreshData()
        {
            //FolderTemplates = Util.DB.GetAllFolderTemplatesDB();
            //Templates = Util.DB.GetAllTemplates_VM(FolderTemplates);
        }

        public static List<string> Name_Templates
        {
            get
            {
                var names = new List<string> { "New Template" };

                //if (Templates != null)
                //{
                //    names.AddRange(Templates.Select(_ => _.NameTemplate).ToList());
                //}


                return names;
            }
        }

        public static int SelectedTemplateIndex { get; set; } = -1;
        public static Template_VM SelectedTemplate { get; set; }



        public static List<Drive> GetAllDrives()
        {
            var drives = new List<Drive>
            {
                new Drive { Name = "P", Path =@"\\\\acdxbfs1\\Projects\\"},
                new Drive { Name = "O", Path =@"\\\\acdxbfs1\\Organisation\\"},
                new Drive { Name = "Q", Path =@"\\\\acdxbfs1\\Enquiries\\"},
                new Drive { Name = "H", Path =@"\\\\acdxbfs1\\Home\\"},
            };

            return drives;
        }

       
        public static System.Collections.ObjectModel.ObservableCollection<Item> GetDirectory(string path)
        {
            var items = Util.ElasticRequest.GetDirectoryTree(path);


            return new System.Collections.ObjectModel.ObservableCollection<Item>(items);
        }
    }
}
