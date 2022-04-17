using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BradyCodeChallengeCore.GeneratorData
{
    internal class GeneratorDataSet
    {
        private List<Generator> generators;

        // TODO: provides no guarantee the days will be in ascending order. May need to use another structure such as a list of KeyValuePairs or similar
        private Dictionary<string, List<GeneratorDayData>> byDay;

        public List<Generator> Generators { get { return generators; } }
        public Dictionary<string, List<GeneratorDayData>> ByDay { get { return byDay; } }

        public GeneratorDataSet(XElement XMLDocOfInputFile, ReferenceData referenceData)
        {
            generators = new List<Generator>();

            byDay = new Dictionary<string, List<GeneratorDayData>>();

            //TODO: handle nulls for each query eg: https://stackoverflow.com/questions/14164974/how-to-concatenate-two-ienumerablet-into-a-new-ienumerablet
            IEnumerable<XElement> allGenerators = from item in XMLDocOfInputFile.Descendants("WindGenerator")
                                                 select item;
            allGenerators = allGenerators.Concat(from item in XMLDocOfInputFile.Descendants("GasGenerator")
                                                  select item);
            allGenerators = allGenerators.Concat(from item in XMLDocOfInputFile.Descendants("CoalGenerator")
                                 select item);

            foreach (XElement generatorXML in allGenerators)
            {
                Generator generator = GeneratorSupport.CreateGenerator(generatorXML, referenceData);
                generators.Add(generator);

                // get references to each data item to allow access by day
                foreach(GeneratorDayData dayData in generator.DayData)
                {
                    string currentDay = dayData.Date;
                    if (!byDay.ContainsKey(currentDay))
                    {
                        byDay.Add(currentDay, new List<GeneratorDayData>() { dayData });
                    }
                    else
                    {
                        byDay[currentDay].Add(dayData);
                    }

                }
            }
        }
    }
}
