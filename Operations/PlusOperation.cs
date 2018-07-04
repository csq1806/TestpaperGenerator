using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
	public class PlusOperation : OperationBase
	{
		public PlusOperation()
		{
			IsChecked = true;
		}
		public override string DisplayName { get { return "加法"; } }
		public override string Operator { get { return "+"; } }

		public override int DisplayIndex { get { return 1; } }
		public override int GenerateNextNumber(int preResult)
		{
			int max = Maximum + 1;
			return random.Next(1, max - preResult);
		}
	}
}
