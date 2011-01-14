using System;

namespace LittleSharp.LTL
{
	public class Not : LTLFormula, Literal
	{
		
		public LTLFormula Enclosed {
			get;
			set;
		}
		
		public Not (LTLFormula enclosed)
		{
			Enclosed = enclosed;
		}
		
		public bool IsImplied (LTLFormula[] old, LTLFormula[] next)
		{
			if (Array.Exists(old, formula => formula == this)) {
				return true;
			}
			
			return false;
		}

		public LTLFormula Negate ()
		{
			return this.Enclosed;
		}

		public override string ToString ()
		{
			return string.Format ("~ {0}", Enclosed);
		}
	}
}

