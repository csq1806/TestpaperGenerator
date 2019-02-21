using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
	public class MultipleOperation : OperationBase
	{
		public MultipleOperation()
		{
			IsChecked = false;
		}
		public override string DisplayName { get { return "乘法"; } }
		public override string Operator { get { return " x "; } }
		public override int DisplayIndex { get { return 3; } }
		public override bool IsEnabled { get { return true; } }
		public override int GenerateNextNumber(int preResult)
		{
			return random.Next(1, 10);
			int max = Maximum + 1;
			return random.Next(0, max / preResult);
		}
	}
}
