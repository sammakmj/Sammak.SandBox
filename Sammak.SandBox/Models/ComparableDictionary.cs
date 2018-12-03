using System;
using System.Collections.Generic;

namespace Sammak.SandBox.Models
{
    /// <summary>
    /// A derrived class of Dictionary<<see cref="TKey"/>, <see cref="TValue"/>>.  
    /// The instances of this class can be properly compared to each other.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ComparableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        #region  Equals and Hashcode Overrides

        /// <summary>
        /// The Equals override compares all items of the dictionaries for their type equality as well as their keys and values equalities.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var other = (ComparableDictionary<TKey, TValue>)obj;
            if (Count != other.Count)
                return false;

            // check keys are the same
            foreach (TKey key in Keys)
                if (!other.ContainsKey(key))
                    return false;

            // check if all items values of both dictionaries are the same
            foreach (TKey key in Keys)
            {
                // both null considered to be the same
                if (this[key] == null && other[key] == null)
                    continue;

                if (this[key] == null || !this[key].Equals(other[key]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// The == override compares the two objects of this type for equality
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        public static bool operator ==(ComparableDictionary<TKey, TValue> item1, ComparableDictionary<TKey, TValue> item2)
        {
            if (ReferenceEquals(item1, item2))
                return true;
            if (item1 is null || item2 is null)
                return false;

            return item1.Equals(item2);
        }

        /// <summary>
        /// The == override compares the two objects of this type for inequality
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        public static bool operator !=(ComparableDictionary<TKey, TValue> item1, ComparableDictionary<TKey, TValue> item2)
        {
            return !(item1 == item2);
        }

        /// <summary>
        /// The GetHashCode override prepares a complex hash for the object of this type
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                int key = pair.Key.GetHashCode(); // key cannot be null
                int value = pair.Value != null ? pair.Value.GetHashCode() : 0;
                hash ^= ShiftAndWrap(key, 2) ^ value;
            }
            return hash;
        }

        #endregion

        #region Private Helper methods

        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);

            // Preserve the bits to be discarded. 
            uint wrapped = number >> (32 - positions);

            // Shift and wrap the discarded bits. 
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

        #endregion
    }
}