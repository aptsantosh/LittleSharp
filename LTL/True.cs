// 
// True.cs
//  
// Author:
//       Antoine Cailliau <antoine.cailliau@uclouvain.be>
// 
// Copyright (c) 2011 UCLouvain
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

namespace LittleSharp.LTL
{
	public class True : LTLFormula, Literal
	{
		public True ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format ("True");
		}

		public bool IsImplied (LTLFormula[] old, LTLFormula[] next)
		{
			return true;
		}

		public LTLFormula Negate ()
		{
			return new Not(this);
		}
		
		
		public LTLFormula getSub1 ()
		{
			return default(LTLFormula);
		}

		public LTLFormula getSub2 ()
		{
			return default(LTLFormula);
		}

		public LTLFormula getNext ()
		{
			return default(LTLFormula);
		}
		
		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Until))
				return false;
			
			return true;
		}

		public override int GetHashCode ()
		{
			unchecked {
				return 0;
			}
		}
	}
}

