using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDocument
{
	public class GenerateCSVDoc : GenerateDocBase<string>
	{
		public override string GenerateDoc(int pages)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 30; i++)
			{
				for (int j = 1; j <= 2; j++)
				{
					sb.Append(GetExpression());
					if (j % 2 != 0) sb.Append(",,,,,");
					else sb.AppendLine();
				}
			}
			return sb.ToString();
		}

		//public override void Print(int pages)
		//{

		//	if (File.Exists(@"C:\temp\testpaper.csv")) File.Delete(@"C:\temp\testpaper.csv");
		//	using (FileStream fs = new FileStream(@"C:\temp\testpaper.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite))
		//	{
		//		byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
		//		fs.Write(bytes, 0, bytes.Length);
		//		fs.Close();
		//	}
		//	Process.Start(@"C:\temp\testpaper.csv");
		//}
	}
}
