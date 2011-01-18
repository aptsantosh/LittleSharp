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
using System.Collections.Generic;
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
		public Queue<Node> Incoming {
			get ;
			private set ;
		}
		
		/// <summary>
		/// The set of temporal properties that must hold at the current state and have not yet been processed.
		/// </summary>
		public Queue<LTLFormula> New {
			get ; 
			private set ;
		}
		
		/// <summary>
		/// The set of temporal properties that must hold in the node and have already been processed.
		/// </summary>
		public Queue<LTLFormula> Old {
			get;
			private set;
		}
		
		/// <summary>
		/// A set of temporal properties that must hold in all states that are immediate successors of state
		/// satisfying the properties in <c>Old</c>.
		/// </summary>
		public Queue<LTLFormula> Next  {
			get;
			private set;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class.
		/// </summary>
		public Node ()
		{
			Name = String.Format("Node {0}", nameCounter++);
			Incoming = new Queue<Node>();
			New = new Queue<LTLFormula>();
			Old = new Queue<LTLFormula>();
			Next = new Queue<LTLFormula>();
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
			ListUtils.AddsUnique(New, initialObligations);
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
		public Node (Node incoming, Queue<LTLFormula> initialObligations)
			: this()
		{
			foreach (LTLFormula obligation in initialObligations) {
				ListUtils.AddsUnique(New, obligation);
			}
			ListUtils.AddsUnique(Incoming, incoming);
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class with the given queues.
		/// </summary>
		/// <param name='name'>
		/// The name of the node.
		/// </param>
		/// <param name='incoming'>
		/// The set of edge incoming to the node, represented by nodes at the origin of the edges.
		/// </param>
		/// <param name='initialObligations'>
		/// The set of formula that must hold for that node and that have not already been processed.
		/// </param>
		/// <param name='old'>
		/// The set of formula that must hold for that node and that have already been processed.
		/// </param>
		/// <param name='next'>
		/// The set of formula that must hold for all succesor node.
		/// </param>
		public Node (String name, Queue<Node> incoming, Queue<LTLFormula> initialObligations, Queue<LTLFormula> old, Queue<LTLFormula> next)
			: this(incoming, initialObligations, old, next)
		{
			Name = name;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LittleSharp.Buchi.Node"/> class with the given queues.
		/// </summary>
		/// <param name='name'>
		/// The name of the node.
		/// </param>
		/// <param name='incoming'>
		/// The set of edge incoming to the node, represented by nodes at the origin of the edges.
		/// </param>
		/// <param name='initialObligations'>
		/// The set of formula that must hold for that node and that have not already been processed.
		/// </param>
		/// <param name='old'>
		/// The set of formula that must hold for that node and that have already been processed.
		/// </param>
		/// <param name='next'>
		/// The set of formula that must hold for all succesor node.
		/// </param>
		public Node (Queue<Node> incoming, Queue<LTLFormula> initialObligations, Queue<LTLFormula> old, Queue<LTLFormula> next)
			: this()
		{
			// Add all elements from all sets. Create new sets to avoid queue to be shared.
			foreach (Node n in incoming) {
				Incoming.Enqueue(n);
			}
			
			foreach (LTLFormula f in initialObligations) {
				New.Enqueue(f);
			}
			
			foreach (LTLFormula f in old) {
				Old.Enqueue(f);
			}
			
			foreach (LTLFormula f in next) {
				Next.Enqueue(f);
			}
		}
		
		/// <summary>
		/// Expand the current node into the given automaton.
		/// </summary>
		/// <param name='automaton'>
		/// A given automaton to expand incoming within.
		/// </param>
		public Automaton Expand(Automaton automaton)
		{
			if (this.New.Count == 0) {
				Node node = automaton.Similar(this);
				
				if (node != default(Node)) {
					foreach (Node incomingNode in Incoming) {
						ListUtils.AddsUnique(node.Incoming, incomingNode);
					}
					return automaton;
					
				} else {
					Node n = new Node(this, Next);
					automaton.Nodes.Add(n);
					
					return n.Expand(automaton);
				}
				
			} else {
				LTLFormula n = New.Dequeue();
				
				if (n is Literal) {
					LTLFormula notN = n.Negate();
					if (Old.Contains(notN)) {
						return automaton;
					} else {
						ListUtils.AddsUnique(Old, n);
						return this.Expand(automaton);
					}
				} else if (n is Until | n is Release | n is Or) {
					Node n1 = new Node(Incoming, New, Old, Next);
					Node n2 = new Node(Incoming, New, Old, Next);
					
					LTLFormula new1 = null;
					LTLFormula new2 = null;
					LTLFormula next = null;
					
					if (n is Until) {
						new1 = ((Until) n).Left;
						next = n;
						new2 = ((Until) n).Right;
						
					} else if (n is Release) {
						new1 = ((Release) n).Right;
						next = n;
						new2 = ((Release) n).Left;
												
					} else if (n is Or) {
						new1 = ((Or) n).Left;
						new2 = ((Or) n).Right;
						
					}
					
					if (!Old.Contains(new1)) {
						ListUtils.AddsUnique(n1.New, new1);
					}
					
					if (!Old.Contains(new2)) {
						ListUtils.AddsUnique(n2.New, new2);
					}
										
					ListUtils.AddsUnique(n1.Old, n);
					ListUtils.AddsUnique(n2.Old, n);
					
					ListUtils.AddsUnique(n1.Next, next);
					
					return n2.Expand(n1.Expand(automaton));
					
				} else if (n is And) {
					And andN = (And) n;
					
					Node newNode = new Node(Name, Incoming, New, Old, Next);
					if (!Old.Contains(andN.Left)) {
						ListUtils.AddsUnique(newNode.New, andN.Left);
					}
					if (!Old.Contains(andN.Right)) {
						ListUtils.AddsUnique(newNode.New, andN.Right);
					}
					ListUtils.AddsUnique(newNode.Old, n);
					
					return newNode.Expand(automaton);
				}
				return null;
			}
		}
		
	}
}

