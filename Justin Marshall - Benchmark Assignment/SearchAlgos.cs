using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin_Marshall___Benchmark_Assignment
{
    public static class SearchAlgos
    {
        public static int BinarySearch(string[] arr, string key)
        {
            //sets left and right boundaries
            int left = 0;
            int right = arr.Length - 1;

            //search while range in valid
            while (left < right)
            {
                //find middle index
                int mid = left + (right - left) / 2;

                //key found now return index
                if (arr[mid] == key)
                {
                    return mid;
                }
                //if middle value is smaller search the right half, else search the left half
                if (string.Compare(arr[mid], key) < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            //returns negative 1 if not found
            return -1;
        }
        public static Movie? FindMovieByBinarySearch(string targetID, MovieLinkedList movieList)
        {
            ////makes an array to store Movie IDs
            //string[] ids = new string[movies.Count];
            List<Movie> currentMovies = movieList.ToList();
            currentMovies.Sort((a, b) => string.Compare(a.ID, b.ID));

            int left = 0;
            int right = currentMovies.Count;

            while (left <= right) 
            { 
                int mid = left + (right - left) / 2;
                int compare = string.Compare(currentMovies[mid].ID, targetID);

                if (compare == 0)
                {
                    return currentMovies[mid];
                }
                else if (compare < 0)
                {
                    left = mid + 1;
                }
                else
                { right = mid - 1; }
            }
            return null;

        }
    }
}
