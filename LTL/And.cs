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

