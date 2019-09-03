using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Browser
{
    public class Item : ObservableBase
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetField(nameof(Name), ref _name, value); }
        }

        private string _sentence = "";
        public string Sentence
        {
            get { return _sentence; }
            set { SetField(nameof(Sentence), ref _sentence, value); }
        }

        public string Type { get; set; }
        public string Tag { get; set; } = "";
        public string Description { get; set; } = "";
        public string FileExtension { get; set; } = "";

        public string _crudtype { get; set; } = "";
        public string _id { get; set; } = "";
        public string _index { get; set; } = "";
        public string _type { get; set; } = "";
        public string _rootpath { get; set; } = "";
        public string _oldname { get; set; } = "";

        public List<Item> SubItems { get; set; } = new List<Item>();

        public string ToElasticJSON()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(
                        new
                        {
                           _id = _id,
                           _index = _index,
                           _rootpath = _rootpath,
                            _crudtype = _crudtype,
                            Type = Type,
                            _type = _type,
                            Name = Name,
                            OldName = _oldname,
                            Sentence = Sentence,
                            Tag = Tag,
                            Description = Description,
                            FileExtension = FileExtension,
                            Path = Path,

                        });

            return json;
        }





        public string Icon
        {
            get
            {
                if(Type.Equals("Folder"))
                {
                    return "FolderOpenOutline";
                }

                return "FileOutline";
            }
        }

        public string Path { get; set; }

        public string PageNo { get; set; } = "1";
    }
}
