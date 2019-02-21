using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
	public abstract class OperationBase : ModelBase
	{
		protected Random random;

		public OperationBase()
		{
			random = new Random();
		}

		public abstract string DisplayName { get; }

		public abstract string Operator { get; }
		public abstract int DisplayIndex { get; }

		public virtual bool IsEnabled { get { return true; } }

		private bool isChecked = false;

		public bool IsChecked
		{
			get { return isChecked; }
			set { isChecked = value; }
		}


		private int maximum;

		public int Maximum
		{
			get { return maximum; }
			set
			{
				maximum = value;
				OnPropertyChanged();
			}
		}


		public abstract int GenerateNextNumber(int preResult);

		public int GenerateFirstNumber()
		{
			//return random.Next(1, 10);
			return random.Next(5, Maximum);
		}
	}
}
