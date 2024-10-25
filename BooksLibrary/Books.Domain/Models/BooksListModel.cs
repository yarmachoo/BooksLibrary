using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Models
{
    /// <summary>
    /// Получение данных в качестве списка объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BooksListModel<T>
    {
        public List<T> Items { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public BooksListModel(int count, int currentPage, int PageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        }
        public BooksListModel()
        { }
        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
    }
}
