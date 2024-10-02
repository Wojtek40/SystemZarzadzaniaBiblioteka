using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1;

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        //private ObservableCollection<Book> libraryBooks;
        private readonly BooksContext _context = new BooksContext();
        private CollectionViewSource booksViewSource;


        public MainWindow()
        {
            InitializeComponent();
            //libraryBooks = new ObservableCollection<Book>
            //{
            //    new Book { Id = 1, Title = "Iliada", Author = "Homer", Category = "epopeja", Publisher = "Greg", YearPublished = 2021, ISBN="" },
            //    new Book { Id = 2, Title = "Sofokles", Author = "Eról Edyp", Category = "dramat klasyczny", Publisher = "Greg", YearPublished = 2019, ISBN="" }
            //};
            //lvBooks.ItemsSource = libraryBooks;
            booksViewSource = (CollectionViewSource)FindResource(nameof(booksViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Books.Load();

            // bind to the source
            booksViewSource.Source =
                _context.Books.Local.ToObservableCollection();
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow(this);
            addBookWindow.ShowDialog();
        }

        private void SearchBooks_Click(object sender, RoutedEventArgs e)
        {

            //SearchBooksWindow searchBooksWindow = new SearchBooksWindow(libraryBooks);
            //searchBooksWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void AddBook(Book newBook)
        {
            //newBook.Id = libraryBooks.Count + 1;
            //libraryBooks.Add(newBook);
            //lvBooks.ItemsSource = libraryBooks;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // clean up database connections
            _context.Dispose();
            base.OnClosing(e);
        }

    }
}
