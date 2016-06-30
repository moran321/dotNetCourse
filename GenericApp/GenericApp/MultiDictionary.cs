/* Lab 7.2 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    public class MultiDictionary<K, V> : IMultiDictionary<K, V> , IEnumerable<KeyValuePair<K, V>>
    {

        Dictionary<K, LinkedList<V>> dictionary;

        //ctor
        public MultiDictionary()
        {
            dictionary = new Dictionary<K, LinkedList<V>>();
        }

        
        /// implement IMultiDictionary methods ///

        public int Count
        {
            get
            {
               return this.Values.Count();
            }
        } /****************************/

        public ICollection<K> Keys
        {
            get
            {
                return dictionary.Keys;
            }
        } /****************************/

        public ICollection<V> Values
        {
            get
            {
                ICollection<V> col = new List<V>();
                foreach (K key in this.Keys)
                {
                    foreach (V val in dictionary[key])
                    {
                        col.Add(val);
                    }
                }
                return col;
            }
        } /****************************/

        public void Add(K key, V value)
        {
            //if key doesn't exist in dictionary
           if (!dictionary.ContainsKey(key))
            {
                LinkedList<V> list = new LinkedList<V>();
                dictionary.Add(key, list);
            }

            dictionary[key].AddLast(value); //add value to dictionary
            
        } /****************************/

        public void Clear()
        {
            dictionary.Clear();
        } /****************************/


        //remove all values of key in dictionary
        public bool Remove(K key)
        {
            if (this.ContainsKey(key))
            {
                dictionary.Remove(key);
                return true;
            }
            return false;
          
        } /****************************/

        //remove specific value from key in dictionary
        public bool Remove(K key, V value)
        {
            if (this.Contains(key,value))
            {
                 dictionary[key].Remove(value);
                 return true;
            }
            return false;
        } /****************************/

        public bool Contains(K key, V value)
        {
            if (dictionary.ContainsKey(key))
            {
                if (dictionary[key].Contains(value))
                {
                    return true;
                }
                return false;
            }
            return false;
        } /****************************/

        public bool ContainsKey(K key)
        {
            if (dictionary.ContainsKey(key))
            {
                return true;
            }
            return false;
        } /****************************/

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            List<KeyValuePair<K,V>> list = new List<KeyValuePair<K, V>>();
           
            foreach (K key in this.Keys)
            {
                foreach (V val in dictionary[key])
                {
                    list.Add(new KeyValuePair<K,V>(key,val));
                }
            }

            return list.GetEnumerator();
            
        } /****************************/

        // implement IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        } /****************************/



    }//end class
}
