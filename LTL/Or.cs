// 
// Or.cs
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
	public class Or : LTLFormula
	{
		
		public LTLFormula Left {
			get;
			set;
		}
		
		public LTLFormula Right {
			get;
			set;
		}
		
		public Or (LTLFormula left, LTLFormula right)
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
			
			return cond2 || cond1;
		}

		public LTLFormula Negate ()
		{
			return new And(Left.Negate(), Right.Negate());
		}

		public override string ToString ()
		{
			return string.Format ("({0} \\/ {1})", Left, Right);
		}
		
		
		public LTLFormula getSub1 ()
		{
			return Left;
		}

		public LTLFormula getSub2 ()
		{
			return Right;
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
			if (obj.GetType () != typeof(Or))
				return false;
			LittleSharp.LTL.Or other = (LittleSharp.LTL.Or)obj;
			return Left.Equals(other.Left) && Right.Equals(other.Right);
		}


		public override int GetHashCode ()
		{
			unchecked {
				return (Left != null ? Left.GetHashCode () : 0) ^ (Right != null ? Right.GetHashCode () : 0);
			}
		}

		
	}
}

