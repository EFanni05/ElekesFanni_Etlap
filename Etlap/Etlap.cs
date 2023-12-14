using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Etlap
{
    public class Etlap
    {
        private int id;
        private string name;
        private string description;
        private int price;
        private string category;

        public Etlap(int id, string name, string description, int price, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public int Id { get => id; set => id = value; }
        //TODO: String check
        public string Name
        {
            get => name;
            set
            {
                string test = value;
                if (test.ToLower().Contains("drop") || test.ToLower().Contains("update")
                    || test.ToLower().Contains("insert") || test.ToLower().Contains("select"))
                {
                    MessageBox.Show($"{value} is not correct");
                }
                else
                {
                    name = test;
                }
            }
        }
        public string Description
        {
            get => description;
            set
            {
                string test = value;
                if (test.ToLower().Contains("drop") || test.ToLower().Contains("update")
                    || test.ToLower().Contains("insert") || test.ToLower().Contains("select"))
                {
                    MessageBox.Show($"{value} is not correct");
                }
                else
                {
                    description = test;
                }
            }
        }
        public int Price { get => price; set => price = value; }
        public string Category
        {
            get => category;
            set
            {
                string test = value;
                if (test.ToLower().Contains("drop") || test.ToLower().Contains("update")
                    || test.ToLower().Contains("insert") || test.ToLower().Contains("select"))
                {
                    MessageBox.Show($"{value} is not correct");
                }
                else
                {
                    category = test;
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} [{category}] - {price}ft";
        }
    }
}
