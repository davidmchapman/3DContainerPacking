using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CromulentBisgetti.ContainerPackingTests
{
    [TestClass]
    public class ContainerPackingCancelTests
    {
        [TestMethod]
        public async Task LongRunningTest_CanBeCancelled()
        {
            // One of the longer-running 700 tests, #479
            var itemsToPack = new List<Item>
            {
                new Item(1, 64, 48, 64, 14),
                new Item(2, 79, 25, 79, 23),
                new Item(3, 89, 85, 89, 19),
                new Item(4, 79, 66, 79, 17),
                new Item(5, 79, 54, 79, 16),
                new Item(6, 115, 95, 115, 11),
                new Item(7, 76, 54, 76, 20),
                new Item(8, 80, 44, 80, 10),
                new Item(9, 66, 33, 66, 15),
                new Item(10, 50, 32, 50, 15),
                new Item(11, 116, 93, 116, 19),
                new Item(12, 113, 64, 113, 11),
            };
            var containers = new List<Container>
            {
                new Container(1, 587, 233, 220),
            };

            var source = new CancellationTokenSource();

            // start the packing task on another thread and give it a bit of time to start
            var packingTask = Task.Run(() =>
                        PackingService.Pack(containers, itemsToPack, new List<int> { (int)AlgorithmType.EB_AFIT }, source.Token));
            await Task.Delay(50);

            // then cancel it. Packing should return quickly
            source.Cancel();

            var result = await packingTask;

            var elapsedMilliSec = result[0].AlgorithmPackingResults[0].PackTimeInMilliseconds;
            Assert.IsTrue(elapsedMilliSec < 100, $"Expected elapsed time to be less than 100 but found {elapsedMilliSec} msec");
        }
    }
}
