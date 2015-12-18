using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SimpleFOMOD
{  
    public class Mod
    {
        public string ModName { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string URL { get; set; }
        public string Category { get; set; }
        public ObservableCollection<Group> Groups { get; set; }

        public Mod() { }

        public Mod(string modname, string author, string version, string url, string category, ObservableCollection<Group> groups)
        {
            ModName = modname;
            Author = author;
            Version = version;
            URL = url;
            Category = category;
            Groups = groups;
        }
        public Mod(string modname, string author, string version, string url, string category)
        {
            ModName = modname;
            Author = author;
            Version = version;
            URL = url;
            Category = category;
        }

        public class Group
        {
            public string GroupName { get; set; }
            public string Type { get; set; }
            public ObservableCollection<Module> Modules { get; set; }

            public Group() { }

            public Group(string groupname, string type, ObservableCollection<Module> modules)
            {     
                GroupName = groupname;
                Type = type;
                Modules = modules;
            }

            public class Module
            {
                public string ModuleName { get; set; }
                public ObservableCollection<mFile> Files { get; set; }
                public string Description { get; set; }
                public string LocalImagePath { get; set; }
                public string RelativeImagePath { get; set; }

                public Module() { }

                public Module(string modulename, ObservableCollection<mFile> files)
                {
                    ModuleName = modulename;
                    Files = files;
                }

                public Module( string modulename, ObservableCollection<mFile> files, string description, string imagePath)
                {                    
                    ModuleName = modulename;
                    Files = files;
                    Description = description;
                    LocalImagePath = imagePath;
                    RelativeImagePath = @"fomod\images\" + Path.GetFileName(imagePath);
                }

                public class mFile
                {
                    public string FileName { get; set; }
                    public string Destination { get; set; }

                    public mFile() { }

                    public mFile(string filename)
                    {
                        FileName = filename;
                    }

                    public mFile(string filename, string destination)
                    {
                        FileName = filename;
                        Destination = destination;
                    }
                }
            }
        }
    }
}
