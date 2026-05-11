//SortingAlgos
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justin_Marshall___Benchmark_Assignment
{
    public static class SortingAlgos
    {
        public static List<Movie> InsertionSort(List<Movie> movies)
        {
            //Insertion sort by year
            for (int i = 1; i < movies.Count; i++)
            {
                Movie currentMovie = movies[i];
                int j = i - 1;
                //larger year movies are moved to the right 1 position to make room
                while (j >= 0 && movies[j].Year > currentMovie.Year)
                {
                    movies[j + 1] = movies[j];
                    j--;
                    movies[j + 1] = currentMovie;
                }
            }
            return movies;
        }
        public static List<Movie> BubbleSort(List<Movie> movies)
        {
            //bubble sort movies by title in alphabetical order
            for (int i = 0; i < movies.Count - 1; i++)
            {
                for (int j = 0; j < movies.Count - i - 1; j++)
                {
                    //compares current movie title with the next
                    if (string.Compare(movies[j].Title, movies[j + 1].Title) > 0)
                    {
                        //swaps adjacent movies when they are out of aplhabetical order
                        Movie temp = movies[j];
                        movies[j] = movies[j + 1];
                        movies[j + 1] = temp;
                    }
                }
            }
            return movies ;
        }
    }
}
