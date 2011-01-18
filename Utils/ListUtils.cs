// 
// ListUtils.cs
//  
// Author:
//       acailliau <${AuthorEmail}>
// 
// Copyright (c) 2011 acailliau
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
using System.Collections.Generic;
using System.Text;

namespace LittleSharp.Utils
{
	public class ListUtils
	{
		
		/// <summary>
		/// Deeply compare two given lists.
		/// </summary>
		/// <returns>
		/// Whether the list are equals.
		/// </returns>
		/// <param name='l1'>
		/// A list to compare with.
		/// </param>
		/// <param name='l2'>
		/// A list to compage with.
		/// </param>
		/// <typeparam name='T'>
		/// The type of value contained in lists.
		/// </typeparam>
		public static bool AreListEquals<T> (List<T> l1, List<T> l2)
		{
			// Fast-exit, this is the same list
			if (l1 == l2) {
				return true;
			}
			
			// Fast-exit, one of the list is empty
			if (l1 == null | l2 == null) {
				return false;
			}
			
			if (l1.Count == l2.Count) {
				for (int i = 0; i < l1.Count; i++) {
					if (!l1[i].Equals(l2[i])) {
						return false;
					}
				}
				return true;
			}
			
			return false;
		}
		
		/// <summary>
		/// Adds the given element in the list, iff the element is not yet in the list.
		/// </summary>
		/// <param name='list'>
		/// The list.
		/// </param>
		/// <param name='element'>
		/// The element to add.
		/// </param>
		/// <typeparam name='T'>
		/// The type of the list, and of the element
		/// </typeparam>
		public static void AddsUnique<T> (List<T> list, T element)
		{
			foreach (T e in list) {
				if (e.Equals(element)) {
					return;
				}
			}
			list.Add(element);
		}
		
		/// <summary>
		/// Adds the given element in the queue, iff the element is not yet in the queue.
		/// </summary>
		/// <param name='list'>
		/// The queue.
		/// </param>
		/// <param name='element'>
		/// The element to add.
		/// </param>
		/// <typeparam name='T'>
		/// The type of the queue, and of the element
		/// </typeparam>
		public static void AddsUnique<T> (Queue<T> queue, T element)
		{
			foreach (T e in queue) {
				if (e.Equals(element)) {
					return;
				}
			}
			queue.Enqueue(element);
		}
		
		/// <summary>
		/// Deeply compare two given queue.
		/// </summary>
		/// <returns>
		/// Whether the queue are equals.
		/// </returns>
		/// <param name='l1'>
		/// A queue to compare with.
		/// </param>
		/// <param name='l2'>
		/// A queue to compage with.
		/// </param>
		/// <typeparam name='T'>
		/// The type of value contained in queue.
		/// </typeparam>
		public static bool AreListEquals<T> (Queue<T> q1, Queue<T> q2)
		{
			// Fast-exit, this is the same list
			if (q1 == q2) {
				return true;
			}
			
			// Fast-exit, one of the list is empty
			if (q1 == null | q2 == null) {
				return false;
			}
			
			if (q1.Count == q2.Count) {
				T[] l1 = q1.ToArray();
				T[] l2 = q2.ToArray();
			
				for (int i = 0; i < l1.Length; i++) {
					if (!l1[i].Equals(l2[i])) {
						return false;
					}
				}
				return true;
			}
			
			return false;
		}
		
		public static string QueueToString<T> (Queue<T> queue)
		{
			if (queue.Count == 0) {
				return "[]";
			}
			
			StringBuilder builder = new StringBuilder("[");
			foreach (T e in queue) {
				builder.AppendFormat("{0},", e);
			}
			builder.Remove(builder.Length-1, 1);
			builder.Append("]");
			return builder.ToString();
		}
		
	}
}

