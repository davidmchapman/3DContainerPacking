using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CromulentBisgetti.ContainerPacking.Entities
{
	[DataContract]
	public class AlgorithmPackingResult
	{
		#region Constructors

		public AlgorithmPackingResult()
		{
			this.PackedItems = new List<Item>();
			this.UnpackedItems = new List<Item>();
		}

		#endregion Constructors

		#region Public Properties

		[DataMember]
		public int AlgorithmID { get; set; }

		[DataMember]
		public string AlgorithmName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether all of the items are packed in the container.
		/// </summary>
		/// <value>
		/// True if all the items are packed in the container; otherwise, false.
		/// </value>
		[DataMember]
		public bool IsCompletePack { get; set; }

		/// <summary>
		/// Gets or sets the list of packed items.
		/// </summary>
		/// <value>
		/// The list of packed items.
		/// </value>
		[DataMember]
		public List<Item> PackedItems { get; set; }

		/// <summary>
		/// Gets or sets the elapsed pack time in milliseconds.
		/// </summary>
		/// <value>
		/// The elapsed pack time in milliseconds.
		/// </value>
		[DataMember]
		public long PackTimeInMilliseconds { get; set; }

		/// <summary>
		/// Gets or sets the percent of container volume packed.
		/// </summary>
		/// <value>
		/// The percent of container volume packed.
		/// </value>
		[DataMember]
		public decimal PercentContainerVolumePacked { get; set; }

		/// <summary>
		/// Gets or sets the percent of item volume packed.
		/// </summary>
		/// <value>
		/// The percent of item volume packed.
		/// </value>
		[DataMember]
		public decimal PercentItemVolumePacked { get; set; }

		/// <summary>
		/// Gets or sets the list of unpacked items.
		/// </summary>
		/// <value>
		/// The list of unpacked items.
		/// </value>
		[DataMember]
		public List<Item> UnpackedItems { get; set; }

		#endregion Public Properties
	}
}
