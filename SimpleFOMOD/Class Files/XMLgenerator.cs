using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SimpleFOMOD
{
    class XMLgenerator
    {
        public static void GenerateInfoXML(string activeFolderText, Mod mod)
        {
                // Creates info.xml from user input.
                XDocument info = new XDocument(
                    new XDeclaration("1.0", "UTF-16", null),
                    new XElement("fomod",
                        new XElement("Name", mod.ModName),
                        new XElement("Author", mod.Author),
                        new XElement("Version", mod.Version),
                        new XElement("Website", mod.URL),
                        new XElement("Groups",
                            new XElement("element", mod.Category)
                        )
                    )
                );

                // Saves the generated info.xml to the created fomod folder in the active directory.
                info.Save(activeFolderText + @"\fomod\" + @"\info.xml");
            
        }

        public static void GenerateModuleConfigXML(string activeFolderText, Mod mod)
        {
            // Creates the container to be inserted into moduleconfigXML XDoc.
            XElement xmlChunk = new XElement("optionalFileGroups", new XAttribute("order", "Explicit"));

            // Tentative as fuck moduleConfigXML generation - Can't test until all data stuff is squared away.
            foreach (var group in mod.Groups)
            {
                XElement tempGroup = new XElement("group", new XAttribute("name", group.GroupName), new XAttribute("type", group.Type));
                tempGroup.Add(new XElement("plugins", new XAttribute("order", "explicit")));
                
                foreach (var module in group.Modules)
                {
                    XElement tempModule = new XElement("plugin", new XAttribute("name", module.ModuleName));
                    if (module.Description != null)
                    {
                        tempModule.Add(new XElement("description", module.Description));
                    }
                    if (module.RelativeImagePath != null)
                    {
                        tempModule.Add(new XElement("image", new XAttribute("path", module.RelativeImagePath)));
                    }
                    XElement tempModuleFiles = new XElement("files");
                    tempModule.Add(tempModuleFiles);
                    tempModule.Add(new XElement("typeDescriptor", new XElement("type", new XAttribute("name", group.Type))));

                    foreach (var file in module.Files)
                    {
                        XElement tempFiles = new XElement("file", new XAttribute("source", group.GroupName + @"\" + module.ModuleName + @"\" + file.FileName));
                        if(file.Destination != null)
                        {
                            tempFiles.Add(new XAttribute("destination", file.Destination + @"\" + file.FileName));
                        }
                        tempModuleFiles.Add(tempFiles);
                    }
                    tempGroup.Add(tempModule);
                }
                xmlChunk.Add(tempGroup);
            }
        

            // XML Writer Settings
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            // Creates the XML file and opens a stream.
            string moduleconfigxml = activeFolderText + @"\fomod\" + "ModuleConfig.xml";
            using (var stream = File.Create(moduleconfigxml))
            using (XmlWriter xw = XmlWriter.Create(stream, xws))
            {

                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                // Creates moduleConfig.xml and writes it to the existing fomodfolder.
                XDocument ModuleConfig = new XDocument(
                    new XElement("config", new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"), new XAttribute(xsi + "noNamespaceSchemaLocation", "http://qconsulting.ca/fo3/ModConfig5.0.xsd"),
                    new XElement("moduleName", mod.ModName),
                        new XElement("installSteps", new XAttribute("order", "Explicit"),
                            new XElement("installStep", new XAttribute("name", "Custom"),
                                xmlChunk // Inserts the huge chunk of XML I just spent a bunch of time making and I hope it fucking works or I'll just shoot myself now.
                            )
                        )
                    )
                );

                ModuleConfig.Save(xw);
            }     
        }
    }
}
