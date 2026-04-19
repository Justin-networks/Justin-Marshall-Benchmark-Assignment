using System;
using System.CodeDom.Compiler;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Justin_Marshall___Benchmark_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //creates list for storring movies and associated details
        private List<Movie> movies = new List<Movie>();

        //creating a hashtable for quick lookup of Movie ID
        private MovieHashTable movieHashTable = new MovieHashTable();

        public MainWindow()
        {
            InitializeComponent();

            //loads movie data
            LoadMovies();

            //displays data in the data grid
            dtgMovies.ItemsSource = movies;
        }
        private void LoadMovies()
        {
            //Creating movie objects
            Movie movie1 = new() { ID = "M001", Title = "Shrek 2", Director = "Andrew Adamson", Genre = "Action", Year = 2000, Availability = true };
            Movie movie2 = new() { ID = "M002", Title = "The Dark Knight", Director = "Christopher Nolan", Genre = "Action", Year = 2008, Availability = true };
            Movie movie3 = new() { ID = "M003", Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", Year = 2010, Availability = true };
            Movie movie4 = new() { ID = "M004", Title = "Titanic", Director = "James Cameron", Genre = "Romance", Year = 1997, Availability = false };
            Movie movie5 = new() { ID = "M005", Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", Genre = "Sci-Fi", Year = 1999, Availability = true };
            Movie movie6 = new() { ID = "M006", Title = "Gladiator", Director = "Ridley Scott", Genre = "Action", Year = 2000, Availability = false };
            Movie movie7 = new() { ID = "M007", Title = "Finding Nemo", Director = "Andrew Stanton", Genre = "Animation", Year = 2003, Availability = true };

            //add each movie to the list out of order
            AddMovie(movie7);
            AddMovie(movie1);
            AddMovie(movie6);
            AddMovie(movie3);
            AddMovie(movie5);
            AddMovie(movie4);
            AddMovie(movie2);
        }
        private void AddMovie(Movie movie)
        {
            //adds movie to the hashtable using Movie ID as key
            movieHashTable.Insert(movie.ID, movie);

            //adds movies to list to be displayed
            movies.Add(movie);
        }

        private void btnSearchID_Click(object sender, RoutedEventArgs e)
        {
            //gets entered Movie ID from user input
            string searchID = tbxSearchID.Text.Trim();

            //binary search for movie
            Movie? foundMovie = FindMovieByBinarySearch(searchID);

            if (foundMovie != null)
            {
                //shows found movies in datagrid
                dtgMovies.ItemsSource = null;
                dtgMovies.ItemsSource = new List<Movie> { foundMovie };
            }
            else
            {
                MessageBox.Show("Movie ID not found.");
            }
        }
        private void btnSearchTitle_Click(object sender, RoutedEventArgs e)
        {
            //gets entered Title from user input
            string searchTitle = tbxSearchTitle.Text.Trim().ToLower();

            //store matching movies in a List
            List<Movie> results = new List<Movie>();

            //linear search through list
            foreach (Movie movie in movies)
            {
                //checks title contains searched text
                if (movie.Title.ToLower().Contains(searchTitle))
                {
                    results.Add(movie);
                }
            }

            //display matching movies
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = results;

            if (results.Count == 0)
            {
                MessageBox.Show("No movies found matching that title");
            }
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            //resets movie list on datagrid
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = movies;
        }
        private Movie? FindMovieByBinarySearch(string targetID)
        {
            //makes an array to store Movie IDs
            string[] ids = new string[movies.Count];

            //Movie IDs from list to array
            for (int i = 0; i < movies.Count; i++)
            {
                ids[i] = movies[i].ID;
            }

            //sort made array before binary search
            Array.Sort(ids);

            //binary search on array
            int index = BinarySearch(ids, targetID);

            //return null when ID isn't found
            if (index == -1)
            {
                return null;
            }

            //gets matching ID from sorted array
            string foundID = ids[index];

            //uses the hashtable to get full movie object
            return movieHashTable.GetValue(foundID);
        }

        private int BinarySearch(string[] arr, string key)
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
    }
    public class MovieHashTable
    {
        //fixes the size of array for hashtable
        private int size = 10;
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

            //adds numeric value of each character
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
            //if empty or key already exists, stroe value
            if (keys[index] == null || keys[index] == key)
            {
                keys[index] = key;
                values[index] = value;
            }
            else
            {
                //throws exception collisions not handled correctly
                throw new InvalidOperationException("Has collision");
            }
        }
        public Movie GetValue(string key)
        {
            //finds array index from key
            int index = GetHash(key);

            //check if the key in slot matches
            if (keys[index] == key)
            {
                return values[index];
            }
            //returns null if isn't found
            return null;
        }
    }
    public class Movie
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public bool Availability { get; set; }
    }
}