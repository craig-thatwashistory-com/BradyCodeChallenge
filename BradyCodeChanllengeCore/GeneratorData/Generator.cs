using BradyCodeChallengeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BradyCodeChallengeCore.GeneratorData.GeneratorSupport;

namespace BradyCodeChallengeCore.GeneratorData
{
    public abstract class Generator
    {

        private string name;
        private GeneratorType type;
        private List<GeneratorDayData> dayData;
        private double totalGeneration;
        private double emissionsRating; // from generation report
        private double referenceValueFactor;
        private double referenceEmissionsFactor;

        public string Name
        {
            get => name ?? String.Empty;
        }
        public GeneratorType Type
        {
            get => type;
            protected set { type = value; }
        }
        public double TotalGeneration { get => totalGeneration; }
        public List<GeneratorDayData> DayData { get => dayData; }

        public Generator()
        {
            name = String.Empty;
            dayData = new List<GeneratorDayData>();
            totalGeneration = 0;
        }

        abstract public void Initialise(XElement generatorXMLElement, ReferenceData referenceData);

        protected void initialiseBaseItems(XElement generatorXMLElement, double emissionsRating, double referenceValueFactor, double referenceEmissionsFactor)
        {
            //TODO: validation of the XML structure ... or just assume it is valid & run linq queries to extract the useful elements??
            try
            {
                name = generatorXMLElement.Element("Name").Value;

            }
            catch
            {
                //TODO: throw an exception due badly formatted xml
                throw new NotImplementedException();
            }

            // TODO check if name is empty string

            this.referenceEmissionsFactor = referenceEmissionsFactor;
            this.referenceValueFactor = referenceValueFactor;
            this.emissionsRating = emissionsRating;

            IEnumerable<XElement> generatorDayDetails = from item in generatorXMLElement.Descendants("Day")
                                                        select item;
            foreach (XElement dayDetails in generatorDayDetails)
            {
                //TODO: handle nulls & signal invalid XML format
                string genDate;
                string genEnergy;
                string genPrice;
                try
                {
                    genDate = dayDetails.Element("Date").Value;
                    genEnergy = dayDetails.Element("Energy").Value;
                    genPrice = dayDetails.Element("Price").Value;
                }
                catch
                {
                    throw new ErrorInGeneratorInputXMLException("Error reading Generator input file");
                }

                GeneratorDayData dayDataItem = new GeneratorDayData(this, genDate, genEnergy, genPrice);

                dayDataItem.Emissions = dayDataItem.Energy * this.emissionsRating * this.referenceEmissionsFactor; // doesn't apply to all generator types, but will evaluate to zero where it doesn't apply.

                dayData.Add(dayDataItem);

                try
                {
                    totalGeneration += double.Parse(genEnergy) * double.Parse(genPrice) * this.referenceValueFactor;
                }
                catch
                {
                    throw new ErrorInGeneratorInputXMLException("Invalid values in generator input file");
                }
            }
        }
    }
}
