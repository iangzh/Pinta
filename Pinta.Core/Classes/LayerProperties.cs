// 
// LayerProperties.cs
//  
// Author:
//       Greg Lowe <greg.lowe@gmail.com>
// 
// Copyright (c) 2010 Greg Lowe
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

namespace Pinta.Core
{

	public class LayerProperties
	{

		public LayerProperties (string name, bool hidden, double opacity, BlendMode blendmode)
		{
			this.Opacity = opacity;
			this.Hidden = hidden;
			this.Name = name;
			this.BlendMode = blendmode;
		}

		public string Name { get; private set; }
		public bool Hidden { get; private set; }
		public double Opacity { get; private set; }
		public BlendMode BlendMode { get; private set; }

		public void SetProperties (Layer layer)
		{
			layer.Name = Name;
			layer.Opacity = Opacity;
			layer.Hidden = Hidden;
			layer.BlendMode = BlendMode;
		}
	}
}
