using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Events;
using TestPaperGenerator.Views;
using TestPaperGenerator.ViewModels;

namespace TestPaperGenerator
{
	class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			Container.Resolve<IEventAggregator>();
			RegisterTypes();
			var shell = Container.Resolve<MainWindow>();
			var vm = Container.Resolve<MainWindowViewModel>();
			vm.View = shell;
			return shell;
		}

		private void RegisterTypes()
		{
			Container.RegisterType<EnviormentVariable>(new ContainerControlledLifetimeManager());
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow.Show();
		}
	}
}
