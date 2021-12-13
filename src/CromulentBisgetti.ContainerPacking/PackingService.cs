using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CromulentBisgetti.ContainerPacking
{
    /// <summary>
    /// The container packing service.
    /// </summary>
    public static class PackingService
	{
		/// <summary>
		/// Attempts to pack the specified containers with the specified items using the specified algorithms.
		/// </summary>
		/// <param name="containers">The list of containers to pack.</param>
		/// <param name="itemsToPack">The items to pack.</param>
		/// <param name="algorithmTypeIDs">The list of algorithm type IDs to use for packing.</param>
		/// <returns>A container packing result with lists of the packed and unpacked items.</returns>
		public static List<ContainerPackingResult> Pack(List<Container> containers, List<Item> itemsToPack, List<int> algorithmTypeIDs)
		{
			Object sync = new Object { };
			List<ContainerPackingResult> result = new List<ContainerPackingResult>();

			Parallel.ForEach(containers, container =>
			{
				ContainerPackingResult containerPackingResult = new ContainerPackingResult();
				containerPackingResult.ContainerID = container.ID;

				Parallel.ForEach(algorithmTypeIDs, algorithmTypeID =>
				{
					IPackingAlgorithm algorithm = GetPackingAlgorithmFromTypeID(algorithmTypeID);

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
					decimal itemVolumePacked = algorithmResult.PackedItems.Sum(i => i.Volume);
					decimal itemVolumeUnpacked = algorithmResult.UnpackedItems.Sum(i => i.Volume);

					algorithmResult.PercentContainerVolumePacked = Math.Round(itemVolumePacked / containerVolume * 100, 2);
					algorithmResult.PercentItemVolumePacked = Math.Round(itemVolumePacked / (itemVolumePacked + itemVolumeUnpacked) * 100, 2);

					lock (sync)
					{
						containerPackingResult.AlgorithmPackingResults.Add(algorithmResult);
					}
				});

				containerPackingResult.AlgorithmPackingResults = containerPackingResult.AlgorithmPackingResults.OrderBy(r => r.AlgorithmName).ToList();

				lock (sync)
				{
					result.Add(containerPackingResult);
				}
			});
			
			return result;
		}

		/// <summary>
		/// Gets the packing algorithm from the specified algorithm type ID.
		/// </summary>
		/// <param name="algorithmTypeID">The algorithm type ID.</param>
		/// <returns>An instance of a packing algorithm implementing AlgorithmBase.</returns>
		/// <exception cref="System.Exception">Invalid algorithm type.</exception>
		public static IPackingAlgorithm GetPackingAlgorithmFromTypeID(int algorithmTypeID)
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
