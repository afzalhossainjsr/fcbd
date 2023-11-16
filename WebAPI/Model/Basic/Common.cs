using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Basic
{
    public class CommonParameter
    {
        public int? Id { get; set; }
        public string? Value { get; set; }
        public string? Text { get; set; } 
        
    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // Total Paging need to show
            int _totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            //Current Page
            int _currentPage = page != null ? (int)page : 1;
            //Paging to be starts with
            int _startPage = _currentPage - 3;
            //Paging to be end with
            int _endPage = _currentPage + 3;
            if (_startPage <= 0)
            {
                _endPage -= (_startPage - 1);
                _startPage = 1;
            }
            if (_endPage > _totalPages)
            {
                _endPage = _totalPages;
                if (_endPage > 7)
                {
                    _startPage = _endPage - 4;
                }
            }
            //Setting up the properties
            TotalItems = totalItems;
            CurrentPage = _currentPage;
            PageSize = pageSize;
            TotalPages = _totalPages;
            StartPage = _startPage;
            EndPage = _endPage;
        }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
    public class Column1To7
    {
        public string? Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
        public string? Column4 { get; set; }
        public string? Column5 { get; set; }
        public string? Column6 { get; set; }
        public string? Column7 { get; set; } 
    }
    public class Column1To3 
    {
        public string? Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
    }
    public class Column1To12
    {
        public string? Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
        public string? Column4 { get; set; }
        public string? Column5 { get; set; }
        public string? Column6 { get; set; }
        public string? Column7 { get; set; }
        public string? Column8 { get; set; }
        public string? Column9 { get; set; }
        public string? Column10 { get; set; }
        public string? Column11 { get; set; }
        public string? Column12 { get; set; } 
    }
}
