using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDocument
{
	public abstract class GenerateDocBase<T>
	{
		public string MaxExpression { get; set; }
		public Func<string> GetExpression { get; set; }

		public abstract T GenerateDoc(int pages);
	}
}
