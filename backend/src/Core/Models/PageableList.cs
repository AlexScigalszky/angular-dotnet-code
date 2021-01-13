﻿using System.Collections.Generic;

namespace Core.Models
{
    public class PageableList<T>
    {
        public int CountTotal { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public ICollection<T> List { get; set; }

        public PageableList()
        {
            List = new List<T>();
        }
        public PageableList(int CountTotal, int PageNumber, int PageSize, string OrderBy, ICollection<T> List)
        {
            this.CountTotal = CountTotal;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.OrderBy = OrderBy;
            this.List = List;
        }

    }
}