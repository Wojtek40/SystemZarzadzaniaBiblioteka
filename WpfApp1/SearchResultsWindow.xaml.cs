using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Windows;

namespace LibraryManagement
{
    public partial class SearchResultsWindow : Window
    {
        private List<Book> searchResults;

        public SearchResultsWindow(List<Book> results)
        {
            InitializeComponent();
            searchResults = results;
            lvSearchResults.ItemsSource = searchResults;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
