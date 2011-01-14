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
		
	}
}

