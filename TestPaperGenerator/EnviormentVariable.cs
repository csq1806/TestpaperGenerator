
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace TestPaperGenerator
{
	public class EnviormentVariable
	{
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public EnviormentVariable()
		{
			this.InitalizeEnviorment();
		}

		public void InitalizeEnviorment()
		{
		}
	}
}
