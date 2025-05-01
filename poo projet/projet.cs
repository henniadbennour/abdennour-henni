// ---------------------------
// Library Management System (Console App)
// By: ChatGPT (Based on your project instructions)
// ---------------------------

using System;
using System.Collections.Generic;

// Interface defining basic library operations
interface ILibraryOperations
{
    void AddBook(Book book);
    void RemoveBook(string isbn);
    void BorrowBook(string isbn);
    void ReturnBook(string isbn);
    void SearchBook(string keyword);
    void ListAllBooks();
}

// Base class: Book
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsBorrowed { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsBorrowed = false;
    }

    public virtual void Display()
    {
        string status = IsBorrowed ? "(Borrowed)" : "(Available)";
        Console.WriteLine($"Title: {Title}, Author: {Author}, ISBN: {ISBN} {status}");
    }
}

// Derived class: BorrowableBook (demonstrates inheritance + polymorphism)
class BorrowableBook : Book
{
    public BorrowableBook(string title, string author, string isbn)
        : base(title, author, isbn) { }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("This book can be borrowed.");
    }
}

// Library class implementing ILibraryOperations
class Library : ILibraryOperations
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine("Book added successfully.");
    }

    public void RemoveBook(string isbn)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine("Book removed successfully.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void BorrowBook(string isbn)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null && !book.IsBorrowed)
        {
            book.IsBorrowed = true;
            Console.WriteLine("Book borrowed successfully.");
        }
        else
        {
            Console.WriteLine("Book not available.");
        }
    }

    public void ReturnBook(string isbn)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null && book.IsBorrowed)
        {
            book.IsBorrowed = false;
            Console.WriteLine("Book returned successfully.");
        }
        else
        {
            Console.WriteLine("Book not found or not borrowed.");
        }
    }

    public void SearchBook(string keyword)
    {
        var results = books.FindAll(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        if (results.Count > 0)
        {
            Console.WriteLine("Search results:");
            foreach (var book in results)
            {
                book.Display();
            }
        }
        else
        {
            Console.WriteLine("No books found matching the keyword.");
        }
    }

    public void ListAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        Console.WriteLine("List of all books:");
        foreach (var book in books)
        {
            book.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("\n===== Library Management System =====");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Search Book");
            Console.WriteLine("6. List All Books");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option (1-7): ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter ISBN: ");
                    string isbn = Console.ReadLine();
                    library.AddBook(new BorrowableBook(title, author, isbn));
                    break;

                case "2":
                    Console.Write("Enter ISBN of book to remove: ");
                    library.RemoveBook(Console.ReadLine());
                    break;

                case "3":
                    Console.Write("Enter ISBN of book to borrow: ");
                    library.BorrowBook(Console.ReadLine());
                    break;

                case "4":
                    Console.Write("Enter ISBN of book to return: ");
                    library.ReturnBook(Console.ReadLine());
                    break;

                case "5":
                    Console.Write("Enter keyword to search: ");
                    library.SearchBook(Console.ReadLine());
                    break;

                case "6":
                    library.ListAllBooks();
                    break;

                case "7":
                    Console.WriteLine("Exiting application...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please select 1-7.");
                    break;
            }
        }
    }
}
