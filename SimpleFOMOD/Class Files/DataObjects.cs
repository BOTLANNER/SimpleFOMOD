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

                public Module(string modulename)
                {
                    ModuleName = modulename;
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


    



    //public class SubPropertyDescriptor : PropertyDescriptor
    //{
    //    private PropertyDescriptor _parent;
    //    private PropertyDescriptor _child;

    //    public SubPropertyDescriptor(PropertyDescriptor parent, PropertyDescriptor child, string propertyDescriptorName)
    //        : base(propertyDescriptorName, null)
    //    {
    //        _child = child;
    //        _parent = parent;
    //    }
    //    //in this example I have made this read-only, but you can set this to false to allow two-way data-binding
    //    public override bool IsReadOnly { get { return false; } }
    //    public override void ResetValue(object component) { }
    //    public override bool CanResetValue(object component) { return false; }
    //    public override bool ShouldSerializeValue(object component) { return true; }
    //    public override Type ComponentType { get { return _parent.ComponentType; } }
    //    public override Type PropertyType { get { return _child.PropertyType; } }
    //    //this is how the value for the property 'described' is accessed
    //    public override object GetValue(object component)
    //    {
    //        return _child.GetValue(_parent.GetValue(component));
    //    }
    //    /*My example has the read-only value set to true, so a full implementation of the SetValue() function is not necessary.  
    //    However, for two-day binding this must be fully implemented similar to the above method. */
    //    public override void SetValue(object component, object value)
    //    {
    //        //READ ONLY
    //        /*Example:  _child.SetValue(_parent.GetValue(component), value);
    //          Add any event fires or other additional functions here to handle a data update*/
    //        _child.SetValue(_parent.GetValue(component), value);
    //    }
    //}

    //public class ModTypeDescriptors : CustomTypeDescriptor
    //{
    //    Type typeProp;

    //    public ModTypeDescriptors(ICustomTypeDescriptor parent, Type type)
    //        : base(parent)
    //    {
    //        typeProp = type;
    //    }
    //    //This method will add the additional properties to the object.  
    //    //It helps to think of the various PropertyDescriptors are columns in a database table
    //    public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
    //    {
    //        PropertyDescriptorCollection cols = base.GetProperties(attributes);
    //        string propName = ""; //empty string to be populated later
    //                              //find the matching property in the type being called.
    //        foreach (PropertyDescriptor col in cols)
    //        {
    //            if (col.PropertyType.Name == typeProp.Name)
    //                propName = col.Name;
    //        }
    //        PropertyDescriptor pd = cols[propName];
    //        PropertyDescriptorCollection children = pd.GetChildProperties(); //expand the child object

    //        PropertyDescriptor[] propDescripts = new PropertyDescriptor[cols.Count + children.Count];
    //        int count = cols.Count; //start adding at the last index of the array
    //        cols.CopyTo(propDescripts, 0);
    //        //creation of the 'descriptor strings'
    //        foreach (PropertyDescriptor cpd in children)
    //        {
    //            propDescripts[count] = new SubPropertyDescriptor(pd, cpd, pd.Name + "_" + cpd.Name);
    //            count++;
    //        }

    //        PropertyDescriptorCollection newCols = new PropertyDescriptorCollection(propDescripts);
    //        return newCols;
    //    }
    //}

    //public class ModTypeDescProvider<T> : TypeDescriptionProvider
    //{
    //    private ICustomTypeDescriptor td;

    //    public ModTypeDescProvider()
    //    : this(TypeDescriptor.GetProvider(typeof(Mod)))
    //    { }

    //    public ModTypeDescProvider(TypeDescriptionProvider parent)
    //        : base(parent)
    //    { }

    //    public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
    //    {
    //        if (td == null)
    //        {
    //            td = base.GetTypeDescriptor(objectType, instance);
    //            td = new ModTypeDescriptors(td, typeof(T));
    //        }
    //        return td;
    //    }
    //}

    /*
    public void AddModuleToGroup(Mod mod, string groupName, Module module)
    {
        mod.Groups.Find(x => x.GroupName == groupName).Modules.Add(module);
    }
    */

}
