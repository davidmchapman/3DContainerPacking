using CromulentBisgetti.ContainerPacking.Entities;
using System.Collections.Generic;

namespace CromulentBisgetti.ContainerPacking.Algorithms
{
	public abstract class AlgorithmBase
	{
		public abstract ContainerPackingResult Run(Container container, List<Item> items);
	}
}