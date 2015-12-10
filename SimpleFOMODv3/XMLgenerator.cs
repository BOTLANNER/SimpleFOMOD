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
        public static void GenerateInfoXML(string activeFolderText, string tAuthor, string tName, string tURL, string tVer, string tCategory)
        {
            // Creates info.xml if the input is correct. - Really should sort out proper error handling, this just acts as a temporary stopgap to ensure input when testing.
            if (tAuthor.Length > 0 && tName.Length > 0 && tURL.Length > 0 && tVer.Length > 0 && tCategory.Length > 0)
            {
                // Creates info.xml from user input.
                XDocument info = new XDocument(
                    new XDeclaration("1.0", "UTF-16", null),
                    new XElement("fomod",
                        new XElement("Name", tName),
                        new XElement("Author", tAuthor),
                        new XElement("Version", tVer),
                        new XElement("Website", tURL),
                        new XElement("Groups",
                            new XElement("element", tCategory)
                        )
                    )
                );

                // Saves the generated info.xml to the created fomod folder in the active directory.
                info.Save(activeFolderText + @"\fomod\" + @"\info.xml");
            }
        }

        public static void GenerateModuleConfigXML(string activeFolderText, string selectedImage, Mod mod)
        {
            // Creates the container to be inserted into moduleconfigXML XDoc.
            XElement xmlChunk = new XElement("optionalFileGroups", new XAttribute("order", "Explicit"));

            // Tentative as fuck moduleConfigXML generation - Can't test until all data stuff is squared away.
            foreach (var group in mod.Groups)
            {
                XElement tempGroup = new XElement("group", new XAttribute("name", group.GroupName), new XAttribute("type", group.Type));
                tempGroup.Add(new XElement("plugins", new XAttribute("order", "explicit")));
                xmlChunk.Add(tempGroup);

                foreach (var module in group.Modules)
                {
                    XElement tempModule = new XElement("plugin", new XAttribute("name", module.ModuleName));
                    tempModule.Add(new XElement("description",  module.Description));
                    tempModule.Add(new XElement("image", new XAttribute("path", module.RelativeImagePath)));
                    XElement tempModuleFiles = new XElement("files");
                    tempModule.Add(tempModuleFiles);
                    tempModule.Add(new XElement("typeDescriptor", new XElement("type", new XAttribute("name", "Optional"))));

                    foreach (var file in module.Files)
                    {
                        XElement tempFiles = new XElement("file", new XAttribute("destination", group.GroupName + @"\" + module.ModuleName + @"\" + file.FileName));
                        tempModuleFiles.Add(tempFiles);
                    }
                }
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
                    new XElement("moduleName", "NAME VARIABLE FOR MOD GOES HERE"),
                        new XElement("installSteps", new XAttribute("order", "Explicit"),
                            new XElement("installStep", new XAttribute("name", "Custom"),
                                xmlChunk // Inserts the huge chunk of XML I just spent a bunch of time making and I hope it fucking works or I'll just shoot myself now.
                            )
                        )
                    )
                );

                ModuleConfig.Save(xw);

            }


            /* ---- COMMENTED ALL THIS SHIT OUT BECAUSE HOPEFULLY MY REWRITE WORKS, IF IT DOESN'T I MIGHT NEED SOME OF THIS. -----

            // Construct the "Optional" group XElement
            XElement optionalGroup = new XElement("group", new XAttribute("name", "Optional Files"), new XAttribute("type", "SelectAny"),
                new XElement("plugins", new XAttribute("order", "Explicit"))
                );

            // Construct the "Required" group XElement
            XElement reqGroup = new XElement("group", new XAttribute("name", "Required"), new XAttribute("type", "SelectExactlyOne"));


            foreach (var module in modules)
            {
                if (module.Value.ModuleName == "Required")
                {
                    // Create "required" module files using data from "Required"
                    XElement reqModuleFiles = new XElement("files");
                    foreach (mFile file in module.Value.Files)
                    {
                        reqModuleFiles.Add(new XElement("file", new XAttribute("destination", file.Destination), new XAttribute("source", @"Required Files\" + file.FileName)));
                    }

                    // Construct the "Required" plugin element, so it can be given to "reqGroup" and fed into the XDoc.
                    XElement reqPlugin = new XElement("plugins", new XAttribute("order", "Explicit"),
                        new XElement("plugin", new XAttribute("name", "Required"),
                            new XElement("description", "temp description"),
                            new XElement("image", new XAttribute("path", @"fomod\images\" + selectedImage)),
                            reqModuleFiles,
                            new XElement("typeDescriptor",
                                new XElement("type", new XAttribute("name", "Required"))
                            )
                        )
                    );
                    reqGroup.Add(reqPlugin);

                }
                else
                {
                    XElement pluginNew = new XElement("plugin", new XAttribute("name", module.Value.ModuleName));
                    pluginNew.Add(new XElement("description", module.Value.Description "temp desc"));
                    XElement pluginFiles = new XElement("files");

                    foreach (mFile file in module.Value.Files)
                    {
                        if (module.Value.Files != null)
                        {
                            pluginFiles.Add(new XElement("file", new XAttribute("destination", file.Destination), new XAttribute("source", @"Optional Files\" + module.Value.ModuleName + @"\" + file.FileName)));
                        }
                    }
                    pluginNew.Add(pluginFiles);
                    pluginNew.Add(new XElement("typeDescriptor", new XElement("type", new XAttribute("name", "Optional"))));
                    optionalGroup.Add(pluginNew);
                }
            }
            */
        }
    }
}
