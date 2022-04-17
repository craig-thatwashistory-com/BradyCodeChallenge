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
    public class WindGenerator : Generator
    {
        private WindGeneratorLocation location;

        public override void Initialise(XElement generatorXMLElement, ReferenceData referenceData)
        {
            double referenceValueFactor;
            double referenceEmissionsFactor = 0;
            double generatorEmissionsRating = 0;

            string locationString = string.Empty;
            try
            {
                locationString = generatorXMLElement.Element("Location").Value.ToLower();
            }
            catch
            {
                throw new NotImplementedException("Invalid XML input");
            }

            if (locationString == "offshore")
            {
                location = WindGeneratorLocation.offshore;
                referenceValueFactor = referenceData.ValueFactorLow;

            }
            else if (locationString == "onshore")
            {
                location = WindGeneratorLocation.onshore;
                referenceValueFactor = referenceData.ValueFactorHigh;
            }
            else
            {
                throw new ErrorInGeneratorInputXMLException("Invalid location in XML input file");
            }

            initialiseBaseItems(generatorXMLElement, generatorEmissionsRating, referenceValueFactor, referenceEmissionsFactor);
        }
    }
}
