using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFOMOD
{
    class FileIO
    {
        // Create the \fomod\images directory in the activeFolder if they don't already exist.
        public static void fileManipulation(string activeFolder, Mod mod)
        {
            // Checks if "\fomod" exists within the active directory. If not, create it.
            string fomodFolder = activeFolder + @"\fomod";
            bool fomodExists = System.IO.Directory.Exists(fomodFolder);
            if (!fomodExists)
            {
                Directory.CreateDirectory(fomodFolder);
            }

            // Checks if "fomod\images" exists within the active directory. If not, create it.
            string imageFolder = fomodFolder + @"\images\";
            bool imagesExists = System.IO.Directory.Exists(imageFolder);
            if (!imagesExists)
            {
                Directory.CreateDirectory(imageFolder);
            }

            // ---- Tentative rewrite of the file IO method, hopefully this should work because it's more or less the exact same setup as XMLGenerator ---- //

            // Creates directory structures and moves files into respective folders.
            foreach (var group in mod.Groups)
            {
                // Creates a directory inside the activeDirectory for each group.
                string tempGroupFolder = activeFolder + @"\" + group.GroupName;
                bool tempGroupFolderExists = System.IO.Directory.Exists(tempGroupFolder);
                if (!tempGroupFolderExists)
                {
                    Directory.CreateDirectory(tempGroupFolder);
                }

                // Creates subfolders in each group folder for all associated modules.
                foreach (var module in group.Modules)
                {
                    string tempModuleFolder = tempGroupFolder + @"\" + module.ModuleName;
                    bool tempModuleFolderExists = System.IO.Directory.Exists(tempModuleFolder);
                    if (!tempModuleFolderExists)
                    {
                        Directory.CreateDirectory(tempModuleFolder);
                    }

                    // Copies the selected image to the "fomod\images" folder.
                    if (module.LocalImagePath != "")
                    {
                        File.Copy(module.LocalImagePath, imageFolder + Path.GetFileName(module.LocalImagePath));
                    }

                    // Moves all associated files into module folders.
                    foreach (var file in module.Files)
                    {
                        string tempFileName = file.FileName;
                        File.Move(activeFolder + tempFileName, tempModuleFolder + tempFileName);
                    }
                }
            }

        }
    }
}