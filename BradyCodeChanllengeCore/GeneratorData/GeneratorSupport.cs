using BradyCodeChallengeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BradyCodeChallengeCore.GeneratorData
{
    public static class GeneratorSupport
    {
        public enum GeneratorType
        {
            //onshoreWind = 0,
            //offshoreWind = 1,
            wind = 2,
            gas = 3,
            coal = 4

        }

        public enum WindGeneratorLocation
        {
            onshore = 0,
            offshore = 1
        }

        // Factory method to create appropriate Generator object & initialise it from the supplied XML element.
        public static Generator CreateGenerator(XElement generatorElement, ReferenceData referenceData)
        {

            Generator newGenerator;
            string generatorTypeTag = generatorElement.Name.LocalName.ToLower();

            if (generatorTypeTag == "windgenerator")
            {
                newGenerator = new WindGenerator();

            }
            else if (generatorTypeTag == "gasgenerator")
            {
                newGenerator = new GasGenerator();
            }
            else if (generatorTypeTag == "coalgenerator")
            {
                newGenerator = new CoalGenerator();
            }
            else
            {
                throw new ErrorInGeneratorInputXMLException("Unrecognised Generator type");
            }

            newGenerator.Initialise(generatorElement, referenceData);

            return newGenerator;
        }
    }
}
