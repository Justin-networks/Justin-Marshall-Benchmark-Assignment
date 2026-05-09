//MovieLinkedList.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Justin_Marshall___Benchmark_Assignment
{
    public class MovieNode
    {
        //stores a movie object
        public Movie Data;
        public MovieNode Next;
        public MovieNode(Movie data)
        {
            Data = data;
            Next = null;
        }
    }
    public class MovieLinkedList
    {
        private MovieNode head;
        public void Add(Movie movie)
        {
            MovieNode newMovie = new MovieNode(movie);
            if (head == null)
            {
                head = newMovie;
            }
            else
            {
                MovieNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newMovie;
            }
        }
        public List<Movie> ToList()
        {
            List<Movie> movies = new List<Movie>();
            MovieNode current = head;
            while (current != null)
            {
                movies.Add(current.Data);
                current = current.Next;
            }
            return movies;
        }
        public void RebuildFrom(List<Movie> sortedMovies)
        {
            head = null;
            foreach (Movie movie in sortedMovies)
            {
                Add(movie);
            }
        }
    }
}

