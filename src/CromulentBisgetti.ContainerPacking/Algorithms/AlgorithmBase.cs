using System.Collections.Generic;
using CromulentBisgetti.ContainerPacking.Entities;

namespace CromulentBisgetti.ContainerPacking.Algorithms
{
    public abstract class AlgorithmBase
    {
        public abstract ContainerPackingResult Run(Container container, List<Item> items);
    }
}