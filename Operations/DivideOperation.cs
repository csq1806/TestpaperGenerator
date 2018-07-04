using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
	public class DivideOperation : OperationBase
	{
		public DivideOperation()
		{
			IsChecked = false;
		}
		public override string DisplayName { get { return "除法"; } }
		public override string Operator { get { return "/"; } }
		public override int DisplayIndex { get { return 4; } }
		public override bool IsEnabled { get { return false; } }
		public override int GenerateNextNumber(int preResult)
		{
			int max = Maximum + 1;
			return random.Next(1, max / preResult);
		}
	}
}
