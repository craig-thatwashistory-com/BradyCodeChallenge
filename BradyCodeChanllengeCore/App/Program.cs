using BradyCodeChallengeCore.Exceptions;
using BradyCodeChallengeCore.GeneratorData;
using System.Xml.Linq;

// TODO:
//  logging at key points for info/errors/??
//  use visitor pattern or similar to get data from GeneratorDataSet object
//  unit tests

namespace BradyCodeChallengeCore.App
{
    public class GeneratorProcessor
    {
        private const string configFileName = "ConfigData.xml";
        private const string referenceFilename = "ReferenceData.xml";
        private static ConfigData? configData;
        private static ReferenceData? referenceData;


        static void Main(string[] args)
        {
            Console.WriteLine("Launching the Generator Tech Test");

            // default location for config & reference files
            var configDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Config";

            if (args.Length == 0)
            {
                string programName = Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]);

                Console.WriteLine("No command line parameters supplied.");
                Console.WriteLine($"Expected: '{programName} \"<config folder>\"'");
                Console.WriteLine($"Default config file location will be assumed: '{configDirectory}'");
            }
            else
            {
                configDirectory = args[0];
            }

            //  load config file
            string configFilepath = Path.Combine(configDirectory, configFileName);
            XElement configDataXML = XElement.Load(configFilepath);

            configData = new ConfigData(configDataXML);

            string inputDirectory = configData.InputFolder;

            // load reference data
            string referenceFilepath = Path.Combine(configDirectory, referenceFilename);
            XElement referenceDataXML = XElement.Load(referenceFilepath);

            referenceData = new ReferenceData(referenceDataXML);

            // prepare listener for new input files
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = inputDirectory,
                Filter = "*.xml"
            };
            
            watcher.Created += new FileSystemEventHandler(OnNewInputFile);
            // Activate the watcher
            watcher.EnableRaisingEvents = true;

            // block/wait while events are processed
            Console.WriteLine("'x'-<enter> to exit");
            string userInput = string.Empty;
            do
            {
                userInput = Console.ReadLine();
            } while (userInput != "x");
        }

        private static void OnNewInputFile(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                string inputFilePath = e.FullPath;

                WaitForFile(inputFilePath); // wait for the file to be fully written & released before trying to process it

                try
                {
                    processGeneratorReportFile(inputFilePath, ref referenceData, ref configData);
                } catch (ErrorInGeneratorInputXMLException ex)
                {
                    Console.WriteLine($"Error processing file: {inputFilePath}\nFile skipped");
                }

                // move the input file to the archive folder
                string archiveFilePath = Path.Combine(configData.ArchiveFolder, Path.GetFileName(inputFilePath));
                System.IO.File.Move(inputFilePath, archiveFilePath, true);
            }
        }

        private static void WaitForFile(string fullPath)
        {
            while (true)
            {
                try
                {
                    using (StreamReader stream = new StreamReader(fullPath))
                    {
                        break;
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }
    
    private static void processGeneratorReportFile(string inputFilepath, ref ReferenceData referenceData, ref ConfigData configData)
        {

            Console.WriteLine($"Processing file: {inputFilepath}");

            XElement inputGeneratorData = XElement.Load(inputFilepath);

            GeneratorDataSet dataSet = new GeneratorDataSet(inputGeneratorData, referenceData);

            string reportFilePath = Path.Combine(configData.OutputFolder, Path.GetFileNameWithoutExtension(inputFilepath) + "_Report.xml");

            generateOutputXMLReport(reportFilePath, ref dataSet);
        }
        private static void generateOutputXMLReport(string outputReportFilePath, ref GeneratorDataSet dataSet)
        { 
            // TODO: implement visitor pattern to get details from dataSet rather than expose/access internal data structures

            XElement outputReportXML = new XElement("GenerationOutput",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"));

            XElement totalsElement = new XElement("Totals");

            outputReportXML.Add(totalsElement);

            foreach (Generator currentGenerator in dataSet.Generators)
            {
                totalsElement.Add(new XElement("Generator",
                    new XElement("Name", currentGenerator.Name),
                    new XElement("Total", currentGenerator.TotalGeneration)
                    ));
            }

            XElement maxEmissionsElement = new XElement("MaxEmissionGenerators");
            outputReportXML.Add(maxEmissionsElement);


            foreach (KeyValuePair<string, List<GeneratorDayData>> currentDay in dataSet.ByDay)
            {
                GeneratorDayData? maxEmissionsReference = null;

                foreach (GeneratorDayData currentDayData in currentDay.Value)
                {
                    if (maxEmissionsReference == null)
                    {
                        maxEmissionsReference = currentDayData;
                    }
                    else if (currentDayData.Emissions > maxEmissionsReference.Emissions)
                    {
                        maxEmissionsReference = currentDayData;
                    }
                }

                maxEmissionsElement.Add(new XElement("Day",
                    new XElement("Name", maxEmissionsReference.GeneratorName),
                    new XElement("Date", currentDay.Key),
                    new XElement("Emission", maxEmissionsReference.Emissions)
                    ));
            }

            XElement actualHeatRatesElement = new XElement("ActualHeatRates");
            outputReportXML.Add(actualHeatRatesElement);

            foreach (Generator currentGenerator in dataSet.Generators)
            {
                if (currentGenerator.GetType() == typeof(CoalGenerator))
                {

                    actualHeatRatesElement.Add(new XElement("ActualHeatRate",
                        new XElement("Name", currentGenerator.Name),
                        new XElement("HeatRate", ((CoalGenerator)currentGenerator).ActualHeatRate)
                        ));
                }
            }

            outputReportXML.Save(outputReportFilePath);
        }
    }
}