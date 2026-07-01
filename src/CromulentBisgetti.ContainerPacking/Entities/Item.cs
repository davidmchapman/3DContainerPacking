using System.Runtime.Serialization;

namespace CromulentBisgetti.ContainerPacking.Entities
{
    /// <summary>
    /// An item to be packed. Also used to hold post-packing details for the item.
    /// </summary>
	public class Item
	{
		#region Private Variables

		private decimal volume;

		#endregion Private Variables

		#region Constructors

		public Item()
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the Item class.
		/// </summary>
		/// <param name="id">The item ID.</param>
		/// <param name="dim1">The length of one of the three item dimensions.</param>
		/// <param name="dim2">The length of another of the three item dimensions.</param>
		/// <param name="dim3">The length of the other of the three item dimensions.</param>
		/// <param name="quantity">The quantity of the item.</param>
		/// <param name="weight">The weight of the item (not used in calculations).</param>
		/// <param name="unitCost">The unit cost of the item (not used in calculations).</param>
		/// <param name="materialBuffer">The amount of buffer to apply to each dimension for packing materials.</param>
		/// <param name="alreadyPadded">If dimensions are already padded, and so should not be automatically padded.</param>
		public Item(
			string id, 
			decimal dim1, 
			decimal dim2, 
			decimal dim3, 
			int quantity,
			decimal weight,
			decimal unitCost,
			decimal materialBuffer,
			bool alreadyPadded = false
		)
		{
			this.ID = id;
			this.Dim1 = dim1;
			this.Dim2 = dim2;
			this.Dim3 = dim3;
			this.volume = dim1 * dim2 * dim3;
			this.Quantity = quantity;

			Weight = weight;
			UnitCost = unitCost;
			MaterialBuffer = materialBuffer;
			IsPadded = alreadyPadded;

			if (MaterialBuffer > 0m)
			{
				PadMeasurements();
			}
		}

		#endregion Constructors

		#region Public Properties
		
		public decimal Weight { get; set; }
		
		public decimal UnitCost { get; set; }
		
		public decimal MaterialBuffer { get; set; }
		
		public bool IsPadded { get; set; }
		
		/// <summary>
		/// Gets or sets the item ID.
		/// </summary>
		/// <value>
		/// The item ID.
		/// </value>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this item has already been packed.
		/// </summary>
		/// <value>
		///   True if the item has already been packed; otherwise, false.
		/// </value>
		public bool IsPacked { get; set; }

		/// <summary>
		/// Gets or sets the length of one of the item dimensions.
		/// </summary>
		/// <value>
		/// The first item dimension.
		/// </value>
		public decimal Dim1 { get; set; }

		/// <summary>
		/// Gets or sets the length another of the item dimensions.
		/// </summary>
		/// <value>
		/// The second item dimension.
		/// </value>
		public decimal Dim2 { get; set; }

		/// <summary>
		/// Gets or sets the third of the item dimensions.
		/// </summary>
		/// <value>
		/// The third item dimension.
		/// </value>
		public decimal Dim3 { get; set; }

		/// <summary>
		/// Gets or sets the x coordinate of the location of the packed item within the container.
		/// </summary>
		/// <value>
		/// The x coordinate of the location of the packed item within the container.
		/// </value>
		public decimal CoordX { get; set; }

		/// <summary>
		/// Gets or sets the y coordinate of the location of the packed item within the container.
		/// </summary>
		/// <value>
		/// The y coordinate of the location of the packed item within the container.
		/// </value>
		public decimal CoordY { get; set; }

		/// <summary>
		/// Gets or sets the z coordinate of the location of the packed item within the container.
		/// </summary>
		/// <value>
		/// The z coordinate of the location of the packed item within the container.
		/// </value>
		public decimal CoordZ { get; set; }

		/// <summary>
		/// Gets or sets the item quantity.
		/// </summary>
		/// <value>
		/// The item quantity.
		/// </value>
		/// 
		public int Quantity { get; set; }

		/// <summary>
		/// Gets or sets the x dimension of the orientation of the item as it has been packed.
		/// </summary>
		/// <value>
		/// The x dimension of the orientation of the item as it has been packed.
		/// </value>
		public decimal PackDimX { get; set; }

		/// <summary>
		/// Gets or sets the y dimension of the orientation of the item as it has been packed.
		/// </summary>
		/// <value>
		/// The y dimension of the orientation of the item as it has been packed.
		/// </value>
		public decimal PackDimY { get; set; }

		/// <summary>
		/// Gets or sets the z dimension of the orientation of the item as it has been packed.
		/// </summary>
		/// <value>
		/// The z dimension of the orientation of the item as it has been packed.
		/// </value>
		public decimal PackDimZ { get; set; }

		/// <summary>
		/// Gets the item volume.
		/// </summary>
		/// <value>
		/// The item volume.
		/// </value>
		public decimal Volume 
		{
			get
			{
				return volume;
			}
		}

		#endregion Public Properties
		
		
		public void PadMeasurements()
		{
			if (IsPadded)
			{
				return;
			}

			Dim1 += MaterialBuffer;
			Dim2 += MaterialBuffer;
			Dim3 += MaterialBuffer;
			volume = Dim1 * Dim2 * Dim3;

			IsPadded = true;
		}

		public void UnpadMeasurements()
		{
			if (!IsPadded)
			{
				return;
			}

			Dim1 -= MaterialBuffer;
			Dim2 -= MaterialBuffer;
			Dim3 -= MaterialBuffer;

			CoordX += MaterialBuffer / 2;
			CoordY += MaterialBuffer / 2;
			CoordZ += MaterialBuffer / 2;
			
			IsPadded = false;
		}
	}
}
