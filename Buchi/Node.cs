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
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name {
			get ;
			set ;
		}
		
		/// <summary>
		/// The name counter (used to generate unique names)
		/// </summary>
		private static int nameCounter = 0;
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="LittleSharp.Buchi.Node"/> is an initial node.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is initial; otherwise, <c>false</c>.
		/// </value>
		public bool Initial {
			get ;
			set ;
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="LittleSharp.Buchi.Node"/> is accepting.
		/// </summary>
		/// <value>
		/// <c>true</c> if accepting; otherwise, <c>false</c>.
		/// </value>
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
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class.
		/// </summary>
		public Node ()
		{
			Name = String.Format("Node {0}", nameCounter++);
			Incoming = new Set<Node>();
			New = new Set<LTLFormula>();
			Old = new Set<LTLFormula>();
			Next = new Set<LTLFormula>();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class with the given name.
		/// </summary>
		/// <param name='name'>
		/// The name of the node.
		/// </param>
		public Node (string name)
			: this()
		{
			Name = name;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class with the given name and initial obligations.
		/// </summary>
		/// <param name='name'>
		/// The name of the node.
		/// </param>
		/// <param name='initialObligations'>
		/// The initial obligations.
		/// </param>
		public Node (string name, LTLFormula initialObligations)
			: this(name)
		{
			New.Add(initialObligation);
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class with the given incoming edge and the initial obligations.
		/// </summary>
		/// <param name='incoming'>
		/// A node representing the incoming edge.
		/// </param>
		/// <param name='initialObligations'>
		/// The initial obligations.
		/// </param>
		public Node (Node incoming, Set<LTLFormula> initialObligations)
			: this()
		{
			New.Add(initialObligations);
			Incoming.Add(incoming);
		}
		
		/// <summary>
		/// Expand the current node into the given automaton.
		/// </summary>
		/// <param name='automaton'>
		/// A given automaton to expand nodes within.
		/// </param>
		public Automaton Expand(Automaton automaton)
		{
			if (this.New.Count == 0) {
				Node node = automaton.Similar(this);
				
				if (node != default(Node)) {
					foreach (Node incomingNode in Incoming) {
						node.Incoming.Add(incomingNode);
					}
					return automaton;
					
				} else {
					Node n = new Node(this, Next);
					automaton.Nodes.Add(n);
					
					return n.Expand(automaton);
				}
				
			} else {
				// TODO
				
			}
		}
		
	}
}

