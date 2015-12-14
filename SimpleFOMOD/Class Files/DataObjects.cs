using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

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
        /*
        public void AddFileToMod(Mod mod, string groupName, string moduleName, mFile file)
        {
            mod.Groups.Find(x => x.GroupName == groupName).Modules.Find(x => x.ModuleName == moduleName).Files.Add(file);
        }

        public Module GetModuleFromMod(Mod mod, string groupName, string moduleName)
        {
            return mod.Groups.Find(x => x.GroupName == groupName).Modules.Find(x => x.ModuleName == moduleName);
        }

        public void SaveModuleToMod(string groupName, Module module)
        {
            var group = Groups.Find(x => x.GroupName == groupName);
            var oldModule = group.Modules.Find(x => x.ModuleName == module.ModuleName);

            group.Modules.Remove(oldModule);
            group.Modules.Add(module);
        }
        */
    }

    public class Group
    {
        public string GroupName { get; set; }
        public string Type { get; set; }
        public ObservableCollection<Module> Modules { get; set; }

        public Group() { }

        public Group(string groupname)
        {
            GroupName = groupname;
        }

        public Group(string groupname, string type)
        {
            Type = type;
            GroupName = groupname;
        }

        public Group(string groupname, string type, ObservableCollection<Module> modules)
        {
            GroupName = groupname;
            Type = type;
            Modules = modules;
        }
        /*
        public void AddModuleToGroup(Mod mod, string groupName, Module module)
        {
            mod.Groups.Find(x => x.GroupName == groupName).Modules.Add(module);
        }
        */
    }

    public class Module
    {
        public string ModuleName { get; set; }
        public ObservableCollection<mFile> Files { get; set; } 
        public string Description { get; set; }
        public string LocalImagePath { get; set; }
        public string RelativeImagePath { get; set; }

        public Module() { }

        public Module(string modulename)
        {
            ModuleName = modulename;
        }

        public Module(string modulename, ObservableCollection<mFile> files, string description, string imagePath)
        {
            ModuleName = modulename;
            Files = files;
            Description = description;
            LocalImagePath = imagePath;
            RelativeImagePath = @"fomod\images\" + Path.GetFileName(imagePath);
        }
    }

    public class mFile
    {
        public string FileName { get; set; }
        public string Destination { get; set; }

        public mFile() { }

        public mFile(string filename, string destination)
        {
            FileName = filename;
            Destination = destination;
        }
    }
}
