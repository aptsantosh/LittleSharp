using System;

namespace LittleSharp.LTL
{
	public class Until : LTLFormula
	{
		
		public LTLFormula Left {
			get;
			set;
		}
		
		public LTLFormula Right {
			get;
			set;
		}
		
		public Until (LTLFormula left, LTLFormula right)
		{
			Left = left; Right = right;
		}
		
		public bool IsImplied (LTLFormula[] old, LTLFormula[] next)
		{
			if (Array.Exists(old, formula => formula == this)) {
				return true;
			}
			
			bool cond1, cond2, cond3 = false;
			cond2 = this.Right.IsImplied(old, next);
			cond1 = this.Left.IsImplied(old, next);
			if (next != null) {
				cond3 = Array.Exists(next, formula => formula == this);
			}
			
			return cond2 || (cond1 && cond3);
		}

		public LTLFormula Negate ()
		{
			return new Release(Left.Negate(), Right.Negate());
		}

		public override string ToString ()
		{
			return string.Format ("({0} U {1})", Left, Right);
		}
	}
}

