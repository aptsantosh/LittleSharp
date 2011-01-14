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
		
	}
}

