using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyCodeChallengeCore.GeneratorData
{
    public class GeneratorDayData
    {
        private string date;
        private double energy;
        private double price;
        private Generator generator; // back reference to owner
        private double emissions; // calculated

        public string Date { get { return date; } }
        public double Energy { get { return energy; } }
        public double Price { get { return price; } }
        public double Emissions { get { return emissions; } set { emissions = value; } }
        public string GeneratorName { get { return generator.Name; } }

        public GeneratorDayData(Generator owner, string genDate, string genEnergy, string genPrice)
        {
            this.generator = owner;
            this.date = genDate;
            this.energy = double.Parse(genEnergy);
            this.price = double.Parse(genPrice);
            this.emissions = 0; // will be calculated/set by client
        }
    }
}
