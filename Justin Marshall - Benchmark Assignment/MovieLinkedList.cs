//MovieLinkedList.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Justin_Marshall___Benchmark_Assignment
{
    //holds single movie and next node
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
        //entry point to list
        private MovieNode head;
        //new movie to end of list
        public void Add(Movie movie)
        {
            MovieNode newMovie = new MovieNode(movie);
            //if list is empty new node becomes the head
            if (head == null)
            {
                head = newMovie;
            }
            else
            {
                //search last node and attach to new node
                MovieNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newMovie;
            }
        }
        //search linked list and return all movies as list for display and sort
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
        //clear list and rebuild from sorted movie list
        //used after sorting to keep oreder in new linked list
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

