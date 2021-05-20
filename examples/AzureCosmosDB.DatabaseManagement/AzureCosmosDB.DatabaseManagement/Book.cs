using System;
using System.Collections.Generic;
using System.Text;

namespace AzureCosmosDB.DatabaseManagement
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public List<string> Authors  { get; set; }
        public int PageCount { get; set; }
    }
}
