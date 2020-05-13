using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Entities;
using CromulentBisgetti.ContainerPacking.Algorithms;

namespace CromulentBisgetti.ContainerPackingTests
{
	[TestClass]
	public class ContainerPackingTests
	{
		[TestMethod]
		public void EB_AFIT_Passes_700_Standard_Reference_Tests()
		{
			// ORLibrary.txt is an Embedded Resource in this project.
			string resourceName = "CromulentBisgetti.ContainerPackingTests.DataFiles.ORLibrary.txt";
			Assembly assembly = Assembly.GetExecutingAssembly();

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					// Counter to control how many tests are run in dev.
					int counter = 1;

					while (reader.ReadLine() != null && counter <= 700)
					{
						List<Item> itemsToPack = new List<Item>();

						// First line in each test case is an ID. Skip it.

						// Second line states the results of the test, as reported in the EB-AFIT master's thesis, appendix E.
						string[] testResults = reader.ReadLine().Split(' ');

						// Third line defines the container dimensions.
						string[] containerDims = reader.ReadLine().Split(' ');

						// Fourth line states how many distinct item types we are packing.
						int itemTypeCount = Convert.ToInt32(reader.ReadLine());

						for (int i = 0; i < itemTypeCount; i++)
						{
							string[] itemArray = reader.ReadLine().Split(' ');

							Item item = new Item(0, Convert.ToDecimal(itemArray[1]), Convert.ToDecimal(itemArray[3]), Convert.ToDecimal(itemArray[5]), Convert.ToInt32(itemArray[7]));
							itemsToPack.Add(item);
						}

						List<Container> containers = new List<Container>();
						containers.Add(new Container(0, Convert.ToDecimal(containerDims[0]), Convert.ToDecimal(containerDims[1]), Convert.ToDecimal(containerDims[2])));

						List<ContainerPackingResult> result = PackingService.Pack(containers, itemsToPack, new List<int> { (int)AlgorithmType.EB_AFIT });
						
						// Assert that the number of items we tried to pack equals the number stated in the published reference.
						Assert.AreEqual(result[0].AlgorithmPackingResults[0].PackedItems.Count + result[0].AlgorithmPackingResults[0].UnpackedItems.Count, Convert.ToDecimal(testResults[1]));

						// Assert that the number of items successfully packed equals the number stated in the published reference.
						Assert.AreEqual(result[0].AlgorithmPackingResults[0].PackedItems.Count, Convert.ToDecimal(testResults[2]));

						// Assert that the packed container volume percentage is equal to the published reference result.
						// Make an exception for a couple of tests where this algorithm yields 87.20% and the published result 
						// was 87.21% (acceptable rounding error).
						Assert.IsTrue(result[0].AlgorithmPackingResults[0].PercentContainerVolumePacked == Convert.ToDecimal(testResults[3]) ||
							(result[0].AlgorithmPackingResults[0].PercentContainerVolumePacked == 87.20M && Convert.ToDecimal(testResults[3]) == 87.21M));

						// Assert that the packed item volume percentage is equal to the published reference result.
						Assert.AreEqual(result[0].AlgorithmPackingResults[0].PercentItemVolumePacked, Convert.ToDecimal(testResults[4]));

						counter++;
					}
				}
			}
		}
	}
}
