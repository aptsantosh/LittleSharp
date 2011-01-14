using System;

namespace LittleSharp.LTL
{
	public class Proposition : LTLFormula, Literal
	{
		
		public string Name {
			get;
			set;
		}
		
		public Proposition (string name)
		{
			Name = name;
		}
		
		public override string ToString ()
		{
			return string.Format ("{0}", Name);
		}

		public bool IsImplied (LTLFormula[] old, LTLFormula[] next)
		{
			return (Array.Exists(old, formula => formula == this));
		}

		public LTLFormula Negate ()
		{
			return new Not(this);
		}
		
	}
}

