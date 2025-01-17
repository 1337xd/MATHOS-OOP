﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Model
{
    public class Sorting : ISorting
    {
        public string SortOrder { get; set; }
        public string SortBy { get; set; }

        public Sorting(string sortBy, string sortOrder)
        {
            SortBy = sortBy;
            SortOrder = sortOrder ?? "ASC";
        }
    }
}
