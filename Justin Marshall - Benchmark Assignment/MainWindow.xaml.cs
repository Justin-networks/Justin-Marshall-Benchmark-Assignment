//MainWindow.xaml.cs
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
        //private List<Movie> movies = new List<Movie>();

        //creating a hashtable for quick lookup of Movie ID
        private MovieHashTable movieHashTable = new MovieHashTable();

        private MovieLinkedList movieList = new MovieLinkedList();
        public MainWindow()
        {
            InitializeComponent();

            
            //dtgMovies.ItemsSource = movies;
            //loads movie data
            LoadMovies();

            //displays data in the data grid
            //dtgMovies.ItemsSource = movies;
            dtgMovies.ItemsSource = movieList.ToList();
        }

        private void LoadMovies() {

            //List<Movie> movies = new List<Movie>();
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
            movieList.Add(movie);

        }
        private void btnQuickLookup_Click(object sender, RoutedEventArgs e)
        {
            string searchID = tbxQuickLookup.Text.Trim();
            Movie? foundMovie = movieHashTable.GetValue(searchID);

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
        private void btnSearchID_Click(object sender, RoutedEventArgs e)
        {
            //gets entered Movie ID from user input
            string searchID = tbxSearchID.Text.Trim();

            //binary search for movie
            Movie? foundMovie = SearchAlgos.FindMovieByBinarySearch(searchID, movieList);

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
            foreach (Movie movie in movieList.ToList())
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
            dtgMovies.ItemsSource = movieList.ToList();
        }

        private void btnInsertionSort_Click(object sender, RoutedEventArgs e)
        {
            List<Movie> sorted = SortingAlgos.InsertionSort(movieList.ToList());
            movieList.RebuildFrom(sorted);
            //refresh dtgMovies
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = movieList.ToList();
        }

        private void btnBubbleSort_Click(object sender, RoutedEventArgs e)
        {
            List<Movie> sorted = SortingAlgos.BubbleSort(movieList.ToList());
            movieList.RebuildFrom(sorted);
            //refresh dtgMovies
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = movieList.ToList();

        }
    }
}