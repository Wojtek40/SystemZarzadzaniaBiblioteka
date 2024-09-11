using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LibraryManagement
{
    public partial class SearchBooksWindow : Window
    {
        ObservableCollection<Book> libraryBooks;
        public SearchBooksWindow(ObservableCollection<Book> books)
        {
            libraryBooks = books;
            InitializeComponent();
        }

        // Event handlers for button clicks
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                MessageBox.Show("Please enter a search query.");
                return;
            }

            // Perform search
            List<Book> searchResults = libraryBooks.Where(book =>
                book.Title.ToLower().Contains(searchQuery.ToLower()) ||
                book.Author.ToLower().Contains(searchQuery.ToLower()) ||
                book.Category.ToLower().Contains(searchQuery.ToLower()) ||
                book.Publisher.ToLower().Contains(searchQuery.ToLower()) ||
                book.YearPublished.ToString().Contains(searchQuery) ||
                book.ISBN.ToString().Contains(searchQuery)
            ).ToList();

            // Display search results or message if no results found
            if (searchResults.Any())
            {
                // Display search results in a new window
                SearchResultsWindow resultsWindow = new SearchResultsWindow(searchResults);
                resultsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No matching books found.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Handle cancel button click
            Close(); // Close the window
        }
    }
}
