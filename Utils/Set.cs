// 
// Set.cs
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

using System.Text;

using System.Collections;
using System.Collections.Generic;

namespace LittleSharp.Utils
{
	public class Set<T> : IEnumerable<T>, ICollection<T>
	{
		private Hash<T> Table { set; get; }
	
		public Set ()
		{
			Table = new Hash<T>();
		}
		
		public void Add(T element)
		{
			Table.Add(element);
		}
		
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder();
			foreach(T element in Table) {
				builder.AppendFormat("{0},", element.ToString());
			}
			builder.Remove(builder.Length - 1, 1);
			
			return string.Format ("[HashSet: {0}]", builder.ToString());
		}
		
		public void Clear ()
		{
			Table.Clear();
		}

		public bool Contains (T item)
		{
			return Table.Contains(item);
		}

		public void CopyTo (T[] array, int arrayIndex)
		{
			Table.CopyTo(array, arrayIndex);
		}

		public bool Remove (T item)
		{
			return Table.Remove(item);
		}

		public int Count {
			get {
				return Table.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return Table.IsReadOnly;
			}
		}

		public System.Collections.Generic.IEnumerator<T> GetEnumerator ()
		{
			return Table.GetEnumerator();
		}
		
		IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
	}
}

