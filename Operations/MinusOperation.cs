using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
	public class MinusOperation : OperationBase
	{
		public MinusOperation()
		{
			IsChecked = true;
		}
		public override string DisplayName { get { return "减法"; } }
		public override string Operator { get { return "-"; } }
		public override int DisplayIndex { get { return 2; } }
		public override int GenerateNextNumber(int preResult)
		{
			int max = Maximum + 1;
			return random.Next(0, preResult);
		}
	}
}
