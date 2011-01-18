using System;
using LittleSharp.LTL;
using LittleSharp.Buchi;

namespace LittleSharp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			
			LTLFormula formula = new Until(new Proposition("P1"), new Proposition("P2"));
			Automaton automaton = new Automaton();
			
			Node initialNode = new Node();
			initialNode.Incoming.Enqueue("init");
			initialNode.New.Enqueue(formula);
			
			automaton = initialNode.Expand(automaton);
			
			System.Console.WriteLine ("Automaton has {0} states", automaton.Nodes.Count);
			
			System.Console.WriteLine ("digraph G {");
			System.Console.WriteLine ("\tinit [shape=plaintext label=\"\"];");
			foreach (Node n1 in automaton.Nodes) {
				System.Console.WriteLine ("\t{0};", n1.Name);
			}
			foreach (Node n1 in automaton.Nodes) {
				foreach (string n2 in n1.Incoming) {
					System.Console.WriteLine ("\t{0} -> {1};", n2, n1.Name);
				}
			}
			System.Console.WriteLine ("}");
		}
	}
}

