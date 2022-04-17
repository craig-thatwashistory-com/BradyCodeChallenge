using BradyCodeChallengeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BradyCodeChallengeCore.App
{
    public class ConfigData
    {
        private string inputFolder;
        private string archiveFolder;
        private string outputFolder;

        public string InputFolder { get => inputFolder;  }
        public string ArchiveFolder { get => archiveFolder; }
        public string OutputFolder { get => outputFolder; }

        public ConfigData(XElement configDataXML)
        {
            try
            {
                inputFolder = configDataXML.Element("Paths").Element("InputFolder").Value;
                archiveFolder = configDataXML.Element("Paths").Element("ArchiveFolder").Value;
                outputFolder = configDataXML.Element("Paths").Element("OutputFolder").Value;
            }
            catch
            {
                throw new ErrorInConfigXMLException("Error reading Config Data file");
            }
        }
        public ConfigData(string inputFolderPath, string outputFolderpath, string archiveFolderPath)
        {
                inputFolder = inputFolderPath;
                archiveFolder = archiveFolderPath;
                outputFolder = outputFolderpath;
        }
    }
}
