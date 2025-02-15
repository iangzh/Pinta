// 
// WindowShell.cs
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Copyright (c) 2011 Jonathan Pobst
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using Gtk;

namespace Pinta
{
	public class WindowShell : ApplicationWindow
	{
		private VBox shell_layout;
		private VBox menu_layout;
		private HBox? workspace_layout;

		private Toolbar? main_toolbar;

		public WindowShell (Application app, string name, string title, int width, int height, bool maximize) : base (app)
		{
			Name = name;
			Title = title;
			DefaultWidth = width;
			DefaultHeight = height;

			WindowPosition = WindowPosition.Center;
			Resizable = true;

			if (maximize)
				Maximize ();

			shell_layout = new VBox () { Name = "shell_layout" };
			menu_layout = new VBox () { Name = "menu_layout" };

			shell_layout.PackStart (menu_layout, false, false, 0);

			Add (shell_layout);

			shell_layout.ShowAll ();
		}

		public Toolbar CreateToolBar (string name)
		{
			main_toolbar = new Toolbar ();
			main_toolbar.Name = name;

			menu_layout.PackStart (main_toolbar, false, false, 0);
			main_toolbar.Show ();

			return main_toolbar;
		}

		public Statusbar CreateStatusBar (string name)
		{
			var statusbar = new Statusbar {
				Name = name,
				Padding = 0,
				Margin = 0
			};

			// Remove the default text area
			var child = statusbar.Children.FirstOrDefault ();

			if (child != null)
				statusbar.Remove (child);

			shell_layout.PackEnd (statusbar, false, false, 0);
			statusbar.Show ();

			return statusbar;
		}

		public HBox CreateWorkspace ()
		{
			workspace_layout = new HBox ();
			workspace_layout.Name = "workspace_layout";

			shell_layout.PackStart (workspace_layout, true, true, 0);
			workspace_layout.ShowAll ();

			return workspace_layout;
		}

		public void AddDragDropSupport (params TargetEntry[] entries)
		{
			Gtk.Drag.DestSet (this, Gtk.DestDefaults.Motion | Gtk.DestDefaults.Highlight | Gtk.DestDefaults.Drop, entries, Gdk.DragAction.Copy);
		}
	}
}
