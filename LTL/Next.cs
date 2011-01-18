// 
// Next.cs
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
	public class Next : LTLFormula
	{
		public LTLFormula Enclosed {
			get;
			set;
		}
		
		public Next (LTLFormula enclosed)
		{
			Enclosed = enclosed;
		}
		
		public bool IsImplied (LTLFormula[] old, LTLFormula[] next)
		{
			if (Array.Exists(old, formula => formula == this)) {
				return true;
			}
			
			if (this.Enclosed != null) {
				if (next != null) {
					return Array.Exists(next, formula => formula == this.Enclosed);
				} else {
					return false;
				}
			} else {
				return true;
			}
		}

		public LTLFormula Negate ()
		{
			return new Next(Enclosed.Negate());
		}

		public override string ToString ()
		{
			return string.Format ("o {0}", Enclosed);
		}
		
		
		public LTLFormula getSub1 ()
		{
			return Enclosed;
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
			if (obj.GetType () != typeof(Next))
				return false;
			LittleSharp.LTL.Next other = (LittleSharp.LTL.Next)obj;
			return Enclosed.Equals(other.Enclosed);
		}


		public override int GetHashCode ()
		{
			unchecked {
				return (Enclosed != null ? Enclosed.GetHashCode () : 0);
			}
		}

		
	}
}

