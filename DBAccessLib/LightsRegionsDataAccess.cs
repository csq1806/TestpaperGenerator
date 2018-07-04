using DaiKin.DBDataAccessLib.Helper;
using DBAccessLib.DataMapping;
using DBDataAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessLib
{
	public abstract class LightsRegionsDataAccess
	{
		public static List<LightsRegion> GetAllRegions()
		{
			List<LightsRegion> regions = new List<LightsRegion>();
			//DbDataReader reader = SqlHelper.ExecuteReader(
			//		SqlHelper.ConnectionStringLocalTransaction,
			//		CommandType.Text,
			//		SqlProvider.SelectAllRegions);
			//while (reader.Read())
			//{
			//	LightsRegion region = new LightsRegion();
			//	region.Id = Convert.ToInt32(reader["Id"]);
			//	region.RegionName = reader["RegionName"].ToString();
			//	region.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
			//	if (reader["TargetBrightness"] != DBNull.Value)
			//		region.TargetBrightness = Convert.ToSingle(reader["TargetBrightness"]);
			//	regions.Add(region);
			//}
			for (int i = 0; i < 3; i++)
			{
				LightsRegion region = new LightsRegion();
				region.Id = i;
				if (i == 0)
				{
					region.RegionName = "流水线区域";
					region.IsDefault = true;
				}
				else if (i == 1)
				{
					region.RegionName = "R4铜管配料";
					region.IsDefault = false;
				}
				else
				{
					region.RegionName = "冲床、涂装、R6配料";
					region.IsDefault = false;
				}
				region.TargetBrightness = 160;
				regions.Add(region);
			}
			return regions;
		}
	}
}
