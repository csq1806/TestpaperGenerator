using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintDocument
{
	public abstract class PrintDocBase
	{
		public Func<string> GetExpression { get; set; }

		public abstract void Print(int pages);
	}
}
