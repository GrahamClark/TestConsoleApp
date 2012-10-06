using System;

namespace TestConsoleApp.LinqGrouping
{
    class Quote
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
