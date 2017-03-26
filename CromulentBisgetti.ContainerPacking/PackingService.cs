using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CromulentBisgetti.ContainerPacking
{
	/// <summary>
	/// The container packing service.
	/// </summary>
	public static class PackingService
	{
		/// <summary>
		/// Attempts to pack the container with the specified items.
		/// </summary>
		/// <param name="container">The container to pack.</param>
		/// <param name="itemsToPack">The items to pack.</param>
		/// <param name="algorithm">The algorithm to use for packing.</param>
		/// <returns>A container packing result with lists of the packed and unpacked items.</returns>
<<<<<<< HEAD
		public static ContainerPackingResult Pack(Container container, List<Item> itemsToPack, List<int> algorithmTypeIDs)
=======
		public static ContainerPackingResult Pack(Container container, List<Item> itemsToPack, AlgorithmBase algorithm)
>>>>>>> parent of 684e6aa... Changed AlgorithmBase to IPackingAlgorithm.
		{
			Object sync = new Object { };
			ContainerPackingResult result = new ContainerPackingResult();
			result.ContainerID = container.ID;

			Parallel.ForEach(algorithmTypeIDs, algorithmTypeID =>
			{
				IPackingAlgorithm algorithm = PackingService.GetPackingAlgorithmFromTypeID(algorithmTypeID);

				// Until I rewrite the algorithm with no side effects, we need to clone the item list
				// so the parallel updates don't interfere with each other.
				List<Item> items = new List<Item>();

				itemsToPack.ForEach(item =>
				{
					items.Add(new Item(item.ID, item.Dim1, item.Dim2, item.Dim3, item.Quantity));
				});

				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				AlgorithmPackingResult algorithmResult = algorithm.Run(container, items);
				stopwatch.Stop();

				algorithmResult.PackTimeInMilliseconds = stopwatch.ElapsedMilliseconds;

				decimal containerVolume = container.Length * container.Width * container.Height;
				decimal volumePacked = 0;

				algorithmResult.PackedItems.ForEach(item =>
				{
					volumePacked += item.Volume;
				});

				algorithmResult.PercentContainerVolumePacked = (int)Math.Ceiling(volumePacked / containerVolume * 100);

				lock (sync)
				{
					result.AlgorithmPackingResults.Add(algorithmResult);
				}
			});

			result.AlgorithmPackingResults = result.AlgorithmPackingResults.OrderBy(r => r.AlgorithmName).ToList();
			return result;
		}

		/// <summary>
		/// Gets the packing algorithm from the specified algorithm type ID.
		/// </summary>
		/// <param name="algorithmTypeID">The algorithm type ID.</param>
		/// <returns>An instance of a packing algorithm implementing AlgorithmBase.</returns>
		/// <exception cref="System.Exception">Invalid algorithm type.</exception>
		public static AlgorithmBase GetPackingAlgorithmFromTypeID(int algorithmTypeID)
		{
			switch (algorithmTypeID)
			{
				case (int)AlgorithmType.EB_AFIT:
					return new EB_AFIT();

				default:
					throw new Exception("Invalid algorithm type.");
			}
		}
	}
}
