using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public string Priority { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}