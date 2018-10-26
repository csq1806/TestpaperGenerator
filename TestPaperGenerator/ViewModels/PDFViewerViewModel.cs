using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;
using System.Windows;
using TestPaperGenerator.Views;
using Telerik.Windows.Documents.Fixed.Model;

namespace TestPaperGenerator.ViewModels
{
	class PDFViewerViewModel : ViewModelBase
	{
		public PDFViewerViewModel(PDFViewerView view, IUnityContainer container, IEventAggregator eventAggregator) :
			base(view, container, eventAggregator)
		{
		}

		private RadFixedDocument document;

		public RadFixedDocument Document
		{
			get { return document; }
			set
			{
				document = value;
				OnPropertyChanged();
			}
		}

	}
}
