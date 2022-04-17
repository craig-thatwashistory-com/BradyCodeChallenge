using BradyCodeChallengeCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BradyCodeChallengeCore.GeneratorData
{
    public class ReferenceData
    {
        private double valueFactorHigh;
        private double valueFactorMedium;
        private double valueFactorLow;

        private double emissionsFactorHigh;
        private double emissionsFactorMedium;
        private double emissionsFactorLow;

        public double ValueFactorHigh { get => valueFactorHigh;  }
        public double ValueFactorMedium { get => valueFactorMedium; }
        public double ValueFactorLow { get => valueFactorLow; }


        public double EmissionsFactorHigh { get => emissionsFactorHigh; }
        public double EmissionsFactorMedium { get => emissionsFactorMedium; }
        public double EmissionsFactorLow { get => emissionsFactorLow; }

        public ReferenceData(XElement referenceDataXML)
        {
            try
            {
                this.valueFactorHigh = Convert.ToDouble(referenceDataXML.Element("Factors").Element("ValueFactor").Element("High").Value);
                this.valueFactorMedium = Convert.ToDouble(referenceDataXML.Element("Factors").Element("ValueFactor").Element("Medium").Value);
                this.valueFactorLow = Convert.ToDouble(referenceDataXML.Element("Factors").Element("ValueFactor").Element("Low").Value);

                this.emissionsFactorHigh = Convert.ToDouble(referenceDataXML.Element("Factors").Element("EmissionsFactor").Element("High").Value);
                this.emissionsFactorMedium = Convert.ToDouble(referenceDataXML.Element("Factors").Element("EmissionsFactor").Element("Medium").Value);
                this.emissionsFactorLow = Convert.ToDouble(referenceDataXML.Element("Factors").Element("EmissionsFactor").Element("Low").Value);
            }
            catch
            {
                //TODO: improve error reporting
                throw new ErrorInReferenceXMLException("Reference Data XML is not in correct format");

            }
        }
    }
}
