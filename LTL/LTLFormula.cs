using System;

namespace LittleSharp.LTL
{
	public interface LTLFormula
	{
		bool IsImplied(LTLFormula[] old, LTLFormula[] next);		
		LTLFormula Negate();		
	}
}

