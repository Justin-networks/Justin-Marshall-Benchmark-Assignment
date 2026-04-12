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
        public MainWindow()
        {
            InitializeComponent();

            List<Movie> movies = new List<Movie>();
            movies.Add(new Movie() { ID = 1, Title = "Shrek 2", Director = "Andrew Adamson", Genre = "Action", Year = 2000, Availability = true });
            movies.Add(new Movie() { ID = 2, Title = "The Dark Knight", Director = "Christopher Nolan", Genre = "Action", Year = 2008, Availability = true });
            movies.Add(new Movie() { ID = 3, Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", Year = 2010, Availability = true });
            movies.Add(new Movie() { ID = 4, Title = "Titanic", Director = "James Cameron", Genre = "Romance", Year = 1997, Availability = false });
            movies.Add(new Movie() { ID = 5, Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", Genre = "Sci-Fi", Year = 1999, Availability = true });
            movies.Add(new Movie() { ID = 6, Title = "Gladiator", Director = "Ridley Scott", Genre = "Action", Year = 2000, Availability = false });
            movies.Add(new Movie() { ID = 7, Title = "Finding Nemo", Director = "Andrew Stanton", Genre = "Animation", Year = 2003, Availability = true });
            dtgMovies.ItemsSource = movies;
        }

     
    }
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public bool Availability { get; set; }
    }
}