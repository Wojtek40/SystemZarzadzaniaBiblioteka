using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows;

namespace LibraryManagement
{
    public partial class AddBookWindow : Window
    {
        private MainWindow mainWindow;

        public AddBookWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private async void FillFieldsWithBookDataByISBNAsync(string isbn)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"http://data.bn.org.pl/api/institutions/bibs.json?isbnIssn={isbn}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        JObject jsonResponse = JObject.Parse(responseContent);

                        string title = jsonResponse["bibs"][0]["title"].ToString();
                        string author = jsonResponse["bibs"][0]["author"].ToString();
                        string category = jsonResponse["bibs"][0]["genre"].ToString();
                        string publisher = jsonResponse["bibs"][0]["publisher"].ToString();
                        string yearPublished = jsonResponse["bibs"][0]["publicationYear"].ToString();

                        txtTitle.Text = title;
                        txtAuthor.Text = author;
                        txtCategory.Text = category;
                        txtPublisher.Text = publisher;
                        txtYearPublished.Text = yearPublished;
                    }
                    else
                    {
                        MessageBox.Show($"Failed to fetch book data. Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void DownloadDataByISBN_Click(object sender, RoutedEventArgs e)
        {
            string isbn = txtISBN.Text.Trim();

            if (!string.IsNullOrEmpty(isbn))
            {
                FillFieldsWithBookDataByISBNAsync(isbn);
            }
            else
            {
                MessageBox.Show("Please enter an ISBN.");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Book newBook = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Category = txtCategory.Text,
                Publisher = txtPublisher.Text,
                YearPublished = int.Parse(txtYearPublished.Text), // Assuming year is a number
                ISBN = txtISBN.Text
            };

            mainWindow.AddBook(newBook);

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
