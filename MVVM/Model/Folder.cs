using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Browser
{
    public class Folder
    {
        public List<string> ReadonlyAccessGroup { get; set; }
        public List<string> ModifyonlyAccessGroup { get; set; }
        public List<string> ListAccessGroup { get; set; }
        public string Name { get; set; }
        public Folder()
        {
            NestedFolders = new List<Folder>();


            ReadonlyAccessGroup = new List<string>();
            ModifyonlyAccessGroup = new List<string>();
            ListAccessGroup = new List<string>();

        }

        public List<Folder> NestedFolders { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }

    }


    public enum ArchcorpDirectoryGroups
    {
        Architects,
        Mechanical,
        Electrical,
        Structures,
        Intr,
        PMs,
        Architectural_CAD,
        Mechanical_CAD,
        Electrical_CAD,
        Dubai_Office_Staff,
        QS,
        Structural_CAD,
        Admin,
        Systems_Administrator,
    }















    //public class Folder : ObservableBase
    //{
    //    public List<string> ReadonlyAccessGroup { get; set; }
    //    public List<string> WriteonlyAccessGroup { get; set; }
    //    public List<string> ListAccessGroup { get; set; }


    //    public Folder()
    //    {
    //        NestedFolders = new ObservableCollection<Folder>();


    //        ReadonlyAccessGroup = new List<string>();
    //        WriteonlyAccessGroup = new List<string>();
    //        ListAccessGroup = new List<string>();

    //    }

    //    private string _namefolder;
    //    public string NameFolder
    //    {
    //        get { return _namefolder; }
    //        set { SetField(nameof(NameFolder), ref _namefolder, value); }
    //    }

    //    private ObservableCollection<Folder> _nestedfolder;
    //    public ObservableCollection<Folder> NestedFolders
    //    {
    //        get { return _nestedfolder; }
    //        set { SetField(nameof(NestedFolders), ref _nestedfolder, value); }
    //    }

    //    public override string ToString()
    //    {

    //        return string.Format($"{NameFolder}");
    //    }
    //}


}
