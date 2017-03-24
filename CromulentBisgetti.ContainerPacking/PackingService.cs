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
		public static ContainerPackingResult Pack(Container container, List<Item> itemsToPack, IPackingAlgorithm algorithm)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			ContainerPackingResult result = algorithm.Run(container, itemsToPack);
			stopwatch.Stop();

			result.PackTimeInMilliseconds = stopwatch.ElapsedMilliseconds;

			decimal containerVolume = container.Length * container.Width * container.Height;
			decimal volumePacked = 0;

			result.PackedItems.ForEach(item =>
			{
				volumePacked += item.Volume;
			});

			result.PercentContainerVolumePacked = (int)Math.Ceiling(volumePacked / containerVolume * 100);

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
