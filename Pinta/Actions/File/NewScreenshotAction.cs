// 
// NewScreenshotAction.cs
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Copyright (c) 2010 Jonathan Pobst
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
using Gdk;
using Pinta.Core;

namespace Pinta.Actions
{
	class NewScreenshotAction : IActionHandler
	{
		#region IActionHandler Members
		public void Initialize ()
		{
			PintaCore.Actions.File.NewScreenshot.Activated += Activated;
		}

		public void Uninitialize ()
		{
			PintaCore.Actions.File.NewScreenshot.Activated -= Activated;
		}
		#endregion

		private void Activated (object sender, EventArgs e)
		{
			int delay = PintaCore.Settings.GetSetting<int> ("screenshot-delay", 0);

			using var dialog = new SpinButtonEntryDialog (Translations.GetString ("Take Screenshot"),
					PintaCore.Chrome.MainWindow, Translations.GetString ("Delay before taking a screenshot (seconds):"), 0, 300, delay);

			if (dialog.Run () == (int) Gtk.ResponseType.Ok) {
				delay = dialog.GetValue ();

				PintaCore.Settings.PutSetting ("screenshot-delay", delay);

				GLib.Timeout.Add ((uint) delay * 1000, () => {
					Screen screen = Screen.Default;
					var root_window = screen.RootWindow;
					Document doc = PintaCore.Workspace.NewDocument (new Size (root_window.Width, root_window.Height), new Cairo.Color (1, 1, 1));

					using (var pb = new Pixbuf (screen.RootWindow, 0, 0, root_window.Width, root_window.Height)) {
						using (Cairo.Context g = new Cairo.Context (doc.Layers.UserLayers[0].Surface)) {
							CairoHelper.SetSourcePixbuf (g, pb, 0, 0);
							g.Paint ();
						}
					}

					doc.IsDirty = true;

					if (!PintaCore.Chrome.MainWindow.IsActive) {
						PintaCore.Chrome.MainWindow.UrgencyHint = true;

						// Don't flash forever
						GLib.Timeout.Add (3 * 1000, () => PintaCore.Chrome.MainWindow.UrgencyHint = false);
					}

					return false;
				});
			}
		}
	}
}
