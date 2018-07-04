using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LightsManagement.AttachedProperties
{
	public class PermissionControlAP : DependencyObject
	{
		// Using a DependencyProperty as the backing store for CurrentPermission.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CurrentPermissionProperty =
			DependencyProperty.Register("CurrentPermission", typeof(int), typeof(PermissionControlAP), new PropertyMetadata(0));



		public static int GetPermission(DependencyObject obj)
		{
			return (int)obj.GetValue(PermissionProperty);
		}

		public static void SetPermission(DependencyObject obj, int value)
		{
			obj.SetValue(PermissionProperty, value);
		}

		// Using a DependencyProperty as the backing store for Permission.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PermissionProperty =
			DependencyProperty.RegisterAttached("Permission", typeof(int), typeof(PermissionControlAP), new PropertyMetadata(-1, new PropertyChangedCallback(OnPermissionChangedCallback)));

		private static void OnPermissionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				FrameworkElement fe = d as FrameworkElement;

				int permission = (int)e.NewValue;
				if (permission == 0)
				{
					fe.IsEnabled = false;
					return;
				}

				int currentPermission = (int)d.GetValue(CurrentPermissionProperty);
				if ((permission & currentPermission) == currentPermission) fe.IsEnabled = true;
				else fe.IsEnabled = false;
			}
		}


	}
}
