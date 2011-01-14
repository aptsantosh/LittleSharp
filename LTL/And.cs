// 
// And.cs
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
	public class And : LTLFormula
	{
		
		public LTLFormula Left {
			get;
			set;
		}
		
		public LTLFormula Right {
			get;
			set;
		}
		
		public And (LTLFormula left, LTLFormula right)
		{
			Left = left; Right = right;
		}
		
		public bool IsImplied (LTLFormula[] old, LTLFormula[] next)
		{
			if (Array.Exists(old, formula => formula == this)) {
				return true;
			}
			
			bool cond1, cond2 = false;
			cond2 = this.Right.IsImplied(old, next);
			cond1 = this.Left.IsImplied(old, next);
			
			return cond1 && cond2;
		}

		public LTLFormula Negate ()
		{
			return new Or(Left.Negate(), Right.Negate());
		}

		public override string ToString ()
		{
			return string.Format ("({0} /\\ {1})", Left, Right);
		}
		
	}
}

