// 
// State.cs
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
using LittleSharp.Utils;
using LittleSharp.LTL;
namespace LittleSharp.Buchi
{
	public class Node
	{
		
		public string Name {
			get ;
			set ;
		}
		
		public bool Initial {
			get ;
			set ;
		}
		
		public bool Accepting {
			get ;
			set ;
		}
		
		/// <summary>
		/// A set of nodes representing the edges incoming to the current one.
		/// </summary>
		public Set<Node> Incoming {
			get ;
			private set ;
		}
		
		/// <summary>
		/// The set of temporal properties that must hold at the current state and have not yet been processed.
		/// </summary>
		public Set<LTLFormula> New {
			get ; 
			private set ;
		}
		
		/// <summary>
		/// The set of temporal properties that must hold in the node and have already been processed.
		/// </summary>
		public Set<LTLFormula> Old {
			get;
			private set;
		}
		
		/// <summary>
		/// A set of temporal properties that must hold in all states that are immediate successors of state
		/// satisfying the properties in <c>Old</c>.
		/// </summary>
		public Set<LTLFormula> Next  {
			get;
			private set;
		}
		
		public Node (string name)
		{
			Name = name;
			
			Incoming = new Set<Node>();
			New = new Set<LTLFormula>();
			Old = new Set<LTLFormula>();
			Next = new Set<LTLFormula>();
		}
	}
}

