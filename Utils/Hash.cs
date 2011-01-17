// 
// Hash.cs
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
using System.Collections;
using System.Collections.Generic;

namespace LittleSharp.Utils
{
	public class Hash<T> : ICollection<T>, IEnumerable<T>
	{
	
		protected class Bucket {
		
			public int Key { get; set; }
			
			public T Value { get; set; }
		
			public Bucket (int key, T value)
			{
				this.Key = key;
				this.Value = value;
			}

			public override bool Equals (object obj)
			{
				if (obj ==  null)
					return false;
				
				if (ReferenceEquals (this, obj))
					return true;
				
				if (obj.GetType () != typeof(Bucket))
					return false;
					
				Bucket other = (Bucket) obj;
				return Key == other.Key && Value.Equals( other.Value );
			}

			public override int GetHashCode ()
			{
				return Key;
			}
		
		}
		
		public int Size { get; protected set; }
		
		protected int PrimeFactor;
		
		protected int Capacity;
		
		protected Bucket[] Table;
		
		protected long ScaleFactor;
		
		protected long ShiftFactor;
	
	
		public Hash()
			: this (10)
		{
		}
	
		public Hash (int capacity)
			: this (109345121, capacity)
		{
		}
		
		public Hash (int primeFactor, int capacity)
		{
			PrimeFactor = primeFactor;
			Capacity = capacity;
			
			Table = new Bucket[Capacity];
			
			Random randomNumberGenerator = new Random();
			ScaleFactor = randomNumberGenerator.Next(PrimeFactor - 1) + 1;
			ShiftFactor = randomNumberGenerator.Next(PrimeFactor);
		}
		
		/// <summary>
		/// Returns the compressed hash that can be used as a key of the table. This method is applying the MAD
		/// method (Multiply Add and Divide) to default hash code.
		/// </summary>
		/// <param name="value">
		/// A <see cref="T"/> representing the value with want to use as a key
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/> representing the index in the array.
		/// </returns>
		public int GetHashValueOf(T value)
		{
			return (int) (Math.Abs(value.GetHashCode() * ScaleFactor + ShiftFactor) % PrimeFactor) % Capacity;
		}
		
		public bool isEmpty()
		{
			return Size == 0;
		}
		
		protected int FindEntry (T value)
		{
			int available = -1;
			
			if (value == null) {
				throw new System.ArgumentNullException();
			}
			
			int k = value.GetHashCode ();
			int i = GetHashValueOf(value);
			int j = i;
			
			do {
			
				Bucket e = Table[i];
				if (e == null) {
					if (available < 0) {
						available = i;
					}
					break;
				}
				
				if (k == e.Key) {
					return i;
				}
				
				i = (i + 1) % Capacity;
			
			} while (i != j);
			
			return - ( available + 1 );
			
		}
		
		public bool Contains ( T value )
		{
			int i = FindEntry(value);
			return i >= 0;
		}
		
		public void Add ( T value )
		{
			int i = FindEntry(value);
			if (i >= 0) {
				Table[i].Value = value;
				
			} else {
			
				if ( Size >= Capacity / 2) {
					Resize();
					i = FindEntry(value);
				}
				
				Table[-i-1] = new Bucket(value.GetHashCode(), value);
				Size ++;
				
			}
		}

		protected void Resize ( )
		{
		
			Capacity = Capacity * 2;
			Bucket[] oldTable = Table;
			
			Table = new Bucket[Capacity];
		
			Random randomNumberGenerator = new Random();
			ScaleFactor = randomNumberGenerator.Next(PrimeFactor - 1) + 1;
			ShiftFactor = randomNumberGenerator.Next(PrimeFactor);
		
			for ( int i = 0 ; i < oldTable.Length ; i ++ ) {
				
				Bucket e = oldTable[i];
				if (e != null) {
					int j = - 1 - FindEntry(e.Value);
					Table[j] = e;
				}
			
			}
		
		}
	
		public bool Remove(T value)
		{
			int i = FindEntry(value);
			if (i < 0) {
				return false;
			}
			
			Table[i] = null;
			Size --;	
				
			return true;	
		}
		
		public int Count {
			get {
				return this.Size;
			}
		}
		
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		
		public IEnumerator<T> GetEnumerator ()
		{
			if (Count > 0) {
				foreach (Bucket bucket in Table) {
					if (bucket != null) {
						T element = bucket.Value;
						yield return element;
					}
				}
			}
		}
		
		IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

		public void Clear ()
		{
			for (int i = 0; i < Table.Length; i++) {
				Table[i] = null;
			}
		}

		public void CopyTo (T[] array, int arrayIndex)
		{
			if (array == null) {
				throw new System.ArgumentNullException ();
			}
			
			if (arrayIndex < 0) {
				throw new System.ArgumentOutOfRangeException ();
			}
			
			if (arrayIndex >= array.Length || (arrayIndex + Count) > array.Length) {
				throw new System.ArgumentException ();
			}
			
			for (int i = 0; i < Table.Length ; i++) {
				array[i] = Table[i].Value;
			}
		}

		public T[] ToArray()
		{
			T[] temp = new T[Size];
			int i = 0;
			foreach(T item in this) {
				temp[i] = item;
				i++;
			}

			return temp;
		}
		
				
	}
}

