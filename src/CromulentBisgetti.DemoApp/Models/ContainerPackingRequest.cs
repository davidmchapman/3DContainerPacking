using System.Collections.Generic;
using CromulentBisgetti.ContainerPacking.Entities;

namespace CromulentBisgetti.DemoApp.Models
{
    public class ContainerPackingRequest
    {
        public List<Container> Containers { get; set; }

        public List<Item> ItemsToPack { get; set; }

        public List<int> AlgorithmTypeIDs { get; set; }
    }
}