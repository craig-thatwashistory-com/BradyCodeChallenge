using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BradyCodeChallengeCore.GeneratorData
{
    public class CoalGenerator : Generator
    {
        private double totalHeatInput = 0;
        private double actualNetGeneration =0;

        public double ActualHeatRate { get => actualNetGeneration == 0 ? 0 : totalHeatInput / actualNetGeneration; }

        public override void Initialise(XElement generatorXMLElement, ReferenceData referenceData)
        {
            string totalHeatInputString;
            string actualNetGenerationString;
            string emissionsRatingString;

            double referenceValueFactor = referenceData.ValueFactorMedium;
            double referenceEmissionsFactor = referenceData.EmissionsFactorHigh;
            double generatorEmissionsRating;
            try
            {
                totalHeatInputString = generatorXMLElement.Element("TotalHeatInput").Value;
                actualNetGenerationString = generatorXMLElement.Element("ActualNetGeneration").Value;
                emissionsRatingString = generatorXMLElement.Element("EmissionsRating").Value;
            }
            catch
            {
                throw new NotImplementedException("Invalid XML input");
            }

            try
            {
                totalHeatInput = Convert.ToDouble(totalHeatInputString);
                actualNetGeneration = Convert.ToDouble(actualNetGenerationString);
                generatorEmissionsRating = Convert.ToDouble(emissionsRatingString);
            }
            catch
            {
                throw new NotImplementedException("Invalid value in XML input");
            }

            initialiseBaseItems(generatorXMLElement, generatorEmissionsRating, referenceValueFactor, referenceEmissionsFactor);
        }
    }
}
