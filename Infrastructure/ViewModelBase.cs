﻿using Microsoft.Practices.Unity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		private SynchronizationContext current = SynchronizationContext.Current;
		public IEventAggregator EventAggregator;
		public IUnityContainer Container;

		public ViewModelBase(
			Window view,
			IUnityContainer container,
			IEventAggregator eventAggregator)
		{
			if (view != null) this.View = view;
			this.Container = container;
			this.EventAggregator = eventAggregator;
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var eventHandler = this.PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private Window view;

		public Window View
		{
			get { return view; }
			set
			{
				if (view != value)
				{
					view = value;
					view.DataContext = this;
				}
			}
		}

		public virtual void HideWindow()
		{
			view.Hide();
		}

		public virtual void ShowWindow()
		{
			View.Show();
		}

		protected void InvokeOnUIThread(Action action)
		{
			current.Send(new SendOrPostCallback(p => action.Invoke()), null);
		}

		protected void InvokeOnUIThreadAsync(Action action)
		{
			current.Post(new SendOrPostCallback(p => action.Invoke()), null);
		}

		protected void InvokeOnUIThread<T>(Action<T> action, T payload)
		{
			current.Send(new SendOrPostCallback(p => action.Invoke(payload)), payload);
		}

		protected void InvokeOnUIThreadAsync<T>(Action<T> action, T payload)
		{
			current.Post(new SendOrPostCallback(p => action.Invoke(payload)), payload);
		}
	}
}
