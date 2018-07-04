using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessLib.DataMapping
{
	public class LightsRegion
	{
		public int Id { get; set; }
		public string RegionName { get; set; }

		public float? TargetBrightness { get; set; }

		public bool IsDefault { get; set; }
	}
}
