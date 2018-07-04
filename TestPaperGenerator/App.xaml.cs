﻿using LightsManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using TestPaperGenerator;

namespace IlluminationManagement
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public App()
		{

		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ev = (Exception)e.ExceptionObject;
			string errorMsg = ev.ToString();
			//errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

			logger.Fatal("Unhandled Exception: " + errorMsg);

			MessageBox.Show("未处理的异常:\r\n" + errorMsg);
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			StyleManager.ApplicationTheme = new Windows8Theme();

			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}
	}
}
