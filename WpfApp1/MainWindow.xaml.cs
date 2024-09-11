using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Book> libraryBooks;

        public MainWindow()
        {
            InitializeComponent();
            libraryBooks = new ObservableCollection<Book>
            {
                new Book { Id = 1, Title = "Iliada", Author = "Homer", Category = "epopeja", Publisher = "Greg", YearPublished = 2021, ISBN="" },
                new Book { Id = 2, Title = "Sofokles", Author = "Eról Edyp", Category = "dramat klasyczny", Publisher = "Greg", YearPublished = 2019, ISBN="" }
            };
            lvBooks.ItemsSource = libraryBooks;
        }


        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow(this);
            addBookWindow.ShowDialog();
        }

        private void SearchBooks_Click(object sender, RoutedEventArgs e)
        {

            SearchBooksWindow searchBooksWindow = new SearchBooksWindow(libraryBooks);
            searchBooksWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void AddBook(Book newBook)
        {
            newBook.Id = libraryBooks.Count + 1;
            libraryBooks.Add(newBook);
            lvBooks.ItemsSource = libraryBooks;

        }

    }
}
