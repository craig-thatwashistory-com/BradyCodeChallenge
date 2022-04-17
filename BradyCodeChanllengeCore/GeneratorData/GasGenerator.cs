using BradyCodeChallengeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BradyCodeChallengeCore.GeneratorData
{
    public class GasGenerator : Generator
    {
        public override void Initialise(XElement generatorXMLElement, ReferenceData referenceData)
        {
            string emissionsRatingString;

            double referenceValueFactor = referenceData.ValueFactorMedium;
            double referenceEmissionsFactor = referenceData.EmissionsFactorMedium;
            double generatorEmissionsRating;

            try
            {
                emissionsRatingString = generatorXMLElement.Element("EmissionsRating").Value;
            }
            catch
            {
                throw new ErrorInGeneratorInputXMLException("Invalid XML input");
            }

            try
            {
                generatorEmissionsRating = Convert.ToDouble(emissionsRatingString);
            }
            catch
            {
                throw new ErrorInGeneratorInputXMLException("Invalid value in XML input");
            }

            initialiseBaseItems(generatorXMLElement, generatorEmissionsRating, referenceValueFactor, referenceEmissionsFactor);
        }
    }
}
