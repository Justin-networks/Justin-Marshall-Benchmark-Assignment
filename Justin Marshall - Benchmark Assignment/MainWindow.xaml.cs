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

        //linked list to store movies
        private MovieLinkedList movieList = new MovieLinkedList();

        //map each movie ID to a queue of borrower names for waiting list for particular movie
        Dictionary<string, Queue<string>> waitQueue = new Dictionary<string, Queue<string>>();

        public MainWindow()
        {
            InitializeComponent();

            
            //dtgMovies.ItemsSource = movies;
            ////loads movie data
            //LoadMovies();

            //displays data in the data grid
            //dtgMovies.ItemsSource = movies;

            //add linked list movies to datagrid on launch
            dtgMovies.ItemsSource = movieList.ToList();
        }

        //private void LoadMovies()
        //{

        //    //List<Movie> movies = new List<Movie>();
        //    Movie movie1 = new() { ID = "M001", Title = "Shrek 2", Director = "Andrew Adamson", Genre = "Action", Year = 2000, Availability = true };
        //    Movie movie2 = new() { ID = "M002", Title = "The Dark Knight", Director = "Christopher Nolan", Genre = "Action", Year = 2008, Availability = true };
        //    Movie movie3 = new() { ID = "M003", Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", Year = 2010, Availability = true };
        //    Movie movie4 = new() { ID = "M004", Title = "Titanic", Director = "James Cameron", Genre = "Romance", Year = 1997, Availability = false };
        //    Movie movie5 = new() { ID = "M005", Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", Genre = "Sci-Fi", Year = 1999, Availability = true };
        //    Movie movie6 = new() { ID = "M006", Title = "Gladiator", Director = "Ridley Scott", Genre = "Action", Year = 2000, Availability = false };
        //    Movie movie7 = new() { ID = "M007", Title = "Finding Nemo", Director = "Andrew Stanton", Genre = "Animation", Year = 2003, Availability = true };
        //    //add each movie to the list out of order
        //    AddMovie(movie7);
        //    AddMovie(movie1);
        //    AddMovie(movie6);
        //    AddMovie(movie3);
        //    AddMovie(movie5);
        //    AddMovie(movie4);
        //    AddMovie(movie2);

        //    //sample data removed use import function to to laud movies
        //}
        //private void AddMovie(Movie movie)
        //{
        //    //adds movie to the hashtable using Movie ID as key
        //    movieHashTable.Insert(movie.ID, movie);

        //    //adds movies to list to be displayed
        //    movieList.Add(movie);

        //}
        //QUICK LOOKUP
        private void btnQuickLookup_Click(object sender, RoutedEventArgs e)
        {
            //gets entered Movie ID from user input
            string searchID = tbxQuickLookup.Text.Trim();
            //retrieve movie from hashtable using key
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
        //BINARY SEARCH
        private void btnSearchID_Click(object sender, RoutedEventArgs e)
        {
            //gets entered Movie ID from user input
            string searchID = tbxSearchID.Text.Trim();

            //binary search needs sorted data, sorting handled in FindMovieByBinarySearch method
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
        //LINEAR SEARCH
        private void btnSearchTitle_Click(object sender, RoutedEventArgs e)
        {
            //gets entered Title from user input as lowercase
            string searchTitle = tbxSearchTitle.Text.Trim().ToLower();

            //store matching movies in a List
            List<Movie> results = new List<Movie>();

            //search every movie in list for partial match
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
            //rfreshes movie list on datagrid
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = movieList.ToList();
        }

        private void btnInsertionSort_Click(object sender, RoutedEventArgs e)
        {
            //sort copy of list then rebuild linked list in new order from from copy
            List<Movie> sorted = SortingAlgos.InsertionSort(movieList.ToList());
            movieList.RebuildFrom(sorted);
            //refresh dtgMovies
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = movieList.ToList();
        }

        private void btnBubbleSort_Click(object sender, RoutedEventArgs e)
        {
            //sort copy of list then rebuild linked list in new order from from copy
            List<Movie> sorted = SortingAlgos.BubbleSort(movieList.ToList());
            movieList.RebuildFrom(sorted);
            //refresh dtgMovies
            dtgMovies.ItemsSource = null;
            dtgMovies.ItemsSource = movieList.ToList();

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            //load deserialized file from ImportList method into DTGMovies
            List<Movie> import = FileHandler.ImportList();
            if (import != null)
            {
                //replace link list with imported json contents
                movieList.RebuildFrom(import);
                //rebuild hastable with imported json
                movieHashTable = new MovieHashTable();
                foreach (Movie movie in import)
                {
                    movieHashTable.Insert(movie.ID, movie);
                }
                dtgMovies.ItemsSource = movieList.ToList();
            }
        }

        private void btnExport_click(object sender, RoutedEventArgs e)
        {
            //call to exportList
            FileHandler.ExportList(movieList.ToList());
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e)
        {
            //get selected movie
            Movie selectMovie = dtgMovies.SelectedItem as Movie;
            //check if there is text in field if there is message box
            if (tbxBorrowerName.Text == "")
            {
                MessageBox.Show("Please enter customer name.");
            }//check if movie is selected if not messagebox
            else if (selectMovie == null) 
            {
                MessageBox.Show("Please select a movie.");
            }//check if movie is available (bool true), give to borrower and mark unavailable
            else if (selectMovie.Availability == true)
            {
                selectMovie.Borrower = tbxBorrowerName.Text;
                selectMovie.Availability = false;
                //refresh dtgMovies
                dtgMovies.ItemsSource = null;
                dtgMovies.ItemsSource = movieList.ToList();
                MessageBox.Show($"{tbxBorrowerName.Text} has borrowed '{selectMovie.Title}'");
                tbxBorrowerName.Clear();
            }
            else
            {
                //check if movie isn't available and creates a queue for selected movie if on doesn't exist
                if (!waitQueue.ContainsKey(selectMovie.ID)) 
                {
                    waitQueue[selectMovie.ID] = new Queue<string>();
                }
                //ads borrower to the end of waiting queue
                waitQueue[selectMovie.ID].Enqueue(tbxBorrowerName.Text);
                MessageBox.Show($"{tbxBorrowerName.Text} has been added to '{selectMovie.Title}' waiting list.");
                tbxBorrowerName.Clear();
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            //gets selected movie
            Movie selectMovie = dtgMovies.SelectedItem as Movie;
            //check if movie is selected if not messagebox
            if (selectMovie == null)
            {
                MessageBox.Show("Please select a movie.");
            }//check if anyone os waiting for selected movie
            else if (waitQueue.ContainsKey(selectMovie.ID) && waitQueue[selectMovie.ID].Count > 0)
            {
                //give movie to next person in queue
                string Borrower = waitQueue[selectMovie.ID].Dequeue();
                selectMovie.Borrower = Borrower;
                //ensue movie stays available for next borrower in queue
                selectMovie.Availability = false;
                MessageBox.Show($"{selectMovie.Title} has been assigned to next in queue {Borrower}.");
                //refresh dtgMovies
                dtgMovies.ItemsSource = null;
                dtgMovies.ItemsSource = movieList.ToList();
            }
            else
            {
                //when there in no queue change movie to available and remove borrower
                selectMovie.Availability = true;
                selectMovie.Borrower = null;
                MessageBox.Show($"'{selectMovie.Title}' returned.");
                //refresh dtgMovies
                dtgMovies.ItemsSource = null;
                dtgMovies.ItemsSource = movieList.ToList();
            }
        }
    }
}