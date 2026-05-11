using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin_Marshall___Benchmark_Assignment
{
    public class MovieHashTable
    {
        //fixes the size of array for hashtable, large size an attempt to reduce collisions
        private int size = 100;
        //arrays that stores Movie IDs
        private string[] keys;
        //array to store movie objects
        private Movie[] values;

        public MovieHashTable()
        {
            //creates arrays when the hashtable is made
            keys = new string[size];
            values = new Movie[size];
        }
        private int GetHash(string key)
        {
            //Starts hash at 0
            int hash = 0;

            //adds numeric (ASCII) value of each character
            foreach (char c in key)
            {
                hash += c;
            }
            //ensures hash within range on indices
            return hash % size;
        }
        public void Insert(string key, Movie value)
        {
            int index = GetHash(key);
            while (keys[index] != null && keys[index] != key)
            {
                index = (index + 1) % size;
                
            }
            keys[index] = key;
            values[index] = value;

        }
        public Movie GetValue(string key)
        {
            //finds array index from key
            int index = GetHash(key);

            while (keys[index] != null && keys[index] != key)
            {
                index = (index + 1) % size;

            }

            //check if the key in slot matches
            if (keys[index] == key)
            {
                return values[index];
            }
            //returns null if isn't found
            return null;
        }
    }
}
