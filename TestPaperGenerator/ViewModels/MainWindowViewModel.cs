using GenerateDocument;
using Infrastructure;
using Microsoft.Practices.Unity;
using Operations;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Documents.Fixed.FormatProviders;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Fonts;

namespace TestPaperGenerator.ViewModels
{
	class MainWindowViewModel : ViewModelBase
	{
		private EnviormentVariable enviorment;
		private Random random;
		private SynchronizationContext context = SynchronizationContext.Current;
		public ICommand GenerateTestPaperCommand { get; private set; }

		public MainWindowViewModel(
			EnviormentVariable enviorment,
			IUnityContainer container, IEventAggregator eventAggregator) :
			base(null, container, eventAggregator)
		{
			this.enviorment = enviorment;
			this.Maximum = 1000;
			this.CurrentValue = 100;
			this.MaximumNumbers = 5;
			this.CurrentNumber = 3;
			this.MaximumPages = 500;
			this.CurrentPage = 3;
			this.random = new Random();
			this.Operations = new ObservableCollection<OperationBase>();

			GenerateOperations();

			this.GenerateTestPaperCommand = new DelegateCommand<object>(OnGenerateTestPaperCommand);
		}

		private void GenerateOperations()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Operations")).SelectMany(t => t.GetTypes()).ToList();
			List<OperationBase> temp = new List<OperationBase>();
			foreach (Type type in types)
			{
				if (type.BaseType == typeof(OperationBase))
				{
					OperationBase instance = Activator.CreateInstance(type) as OperationBase;
					temp.Add(instance);
				}
			}
			Operations = temp.OrderBy(p => p.DisplayIndex).ToObservableCollection();
		}

		private ObservableCollection<OperationBase> operations;

		public ObservableCollection<OperationBase> Operations
		{
			get { return operations; }
			set
			{
				operations = value;
				OnPropertyChanged();
			}
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

		private int currentValue;

		public int CurrentValue
		{
			get { return currentValue; }
			set
			{
				currentValue = value;
				OnPropertyChanged();
			}
		}

		private int maximumNumbers;

		public int MaximumNumbers
		{
			get { return maximumNumbers; }
			set
			{
				maximumNumbers = value;
				OnPropertyChanged();
			}
		}

		private int currentNumber;

		public int CurrentNumber
		{
			get { return currentNumber; }
			set
			{
				currentNumber = value;
				OnPropertyChanged();
			}
		}

		private int maximumPages;

		public int MaximumPages
		{
			get { return maximumPages; }
			set
			{
				maximumPages = value;
				OnPropertyChanged();
			}
		}


		private int currentPage;

		public int CurrentPage
		{
			get { return currentPage; }
			set
			{
				currentPage = value;
				OnPropertyChanged();
			}
		}

		private bool isBusy;

		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				isBusy = value;
				OnPropertyChanged();
			}
		}



		private void OnGenerateTestPaperCommand(object obj)
		{
			IsBusy = true;
			PDFViewerViewModel pdfviewerViewModel = Container.Resolve<PDFViewerViewModel>();
			ThreadPool.QueueUserWorkItem(new WaitCallback(p =>
			{
				List<OperationBase> checkedOperations = new List<OperationBase>();
				foreach (var operation in Operations)
				{
					operation.Maximum = this.CurrentValue;
					if (operation.IsChecked) checkedOperations.Add(operation);
				}

				GenerateDocBase<RadFixedDocument> doc = new GeneratePDFDoc();
				doc.MaxExpression = GetMaxExpression();
				doc.GetExpression = () => GetExpression(checkedOperations, CurrentNumber);

				pdfviewerViewModel.Document = doc.GenerateDoc(CurrentPage);

				context.Send(new SendOrPostCallback(state => { pdfviewerViewModel.ShowWindow(); }), null);
				IsBusy = false;
			}));
		}

		private string GetMaxExpression()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < CurrentNumber; i++)
			{
				sb.Append(CurrentValue - 1 + "+");
			}
			return sb.ToString();
		}

		private string GetExpression(List<OperationBase> checkedOperations, int numbers)
		{
			string exp = string.Empty;
			int[] arr = new int[numbers];
			for (int i = 0; i < numbers; i++)
			{
				OperationBase operation = GetOperation(checkedOperations, checkedOperations.Count);
				if (i == 0)
				{
					arr[i] = operation.GenerateFirstNumber();
					exp += arr[i];
					continue;
				}

				int start = arr[i - 1];
				int value = operation.GenerateNextNumber(start);
				exp += (operation.Operator + value);
				arr[i] = Convert.ToInt32(new DataTable().Compute(exp, null));

				if (i == numbers - 1) exp += "=";
			}

			return exp;
		}

		private OperationBase GetOperation(List<OperationBase> checkedOperations, int numbers)
		{
			int r = random.Next(0, numbers);
			return checkedOperations[r];
		}
	}
}
