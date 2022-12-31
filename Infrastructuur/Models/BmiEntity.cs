using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Models
{
    public class BmiEntity
    {
        public double CalculateBmi() => Weight / ((Length * Length) / 100) * 100;
        public double Weight { get; set; }
        public double Length { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return CalculateBmi() switch
            {
                < 18.5 => $"{Name} bmi of {CalculateBmi():F1} = underweight.",
                >= 18.5 and <= 25 => $"{Name} bmi of {CalculateBmi():F1} = healthy weight.",
                >= 25 and <= 30 => $"{Name} bmi of {CalculateBmi():F1} = overweight.",
                _ => $"{Name} bmi of {CalculateBmi():F1} = obesitas."
            };
        }
    }
}
