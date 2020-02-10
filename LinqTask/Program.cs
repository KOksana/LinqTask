using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTask
{
    class Program
    {
        class Student
        {
            public string Name { get; set; }
            public Dictionary<string, uint> Grades { get; set; }
        }

        class Customer
        {
            public int Id;
            public string Name;
        }
        class Product
        {
            public int Id;
            public string Name;
        }
        class Transaction
        {
            public DateTime Date;
            public int CustomerId;
            public int ProductId;
        }

        static void Main(string[] args)
        {
            // 1
            Console.WriteLine("***   task 1   ***");

            // int[] array1 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            int[] numberArray = {10, 20, 30, 40, 55, 66, 77, 88, 99, 100};
            Console.WriteLine("init array: " + string.Join(", ", numberArray));

            var evenNumbers = numberArray.Where(elem => elem % 2 == 0);
            Console.WriteLine("even numbers: " + string.Join(", ", evenNumbers));

            var oddSum = numberArray.Where(elem => elem % 2 == 1).Sum();
            Console.WriteLine("sum of odd numbers: " + oddSum);


            var firstOddNumbers = numberArray.TakeWhile(elem => elem % 2 == 0);
            Console.WriteLine("numbers before first even: " + string.Join(", ", firstOddNumbers));

            var arrGroup = numberArray.GroupBy(x => x % 2 == 0);
            Console.WriteLine("group of odd numbers: " + string.Join(", ", arrGroup.ToArray()[0]));
            Console.WriteLine("group of even numbers: " + string.Join(", ", arrGroup.ToArray()[1]));

            Console.WriteLine();

            // 2
            Console.WriteLine("***   task 2   ***");

            string[] stringArray =
                {"There", "are", "many", "variations", "of", "passages", "of", "Lorem", "Ipsum", "available"};

            Console.WriteLine("init array: " + string.Join(", ", stringArray));

            var endChar = "e";
            var resArr1 = stringArray.Where(elem => elem.EndsWith(endChar));
            Console.WriteLine("elements that end with 'e': " + string.Join(", ", resArr1));

            var ch = "s";
            var res = stringArray.Where(elem => elem.Contains(ch)).Count() > 1
                ? "ERROR"
                : stringArray.FirstOrDefault(elem => elem.Contains(ch));
            Console.WriteLine($"element that contains '{ch}' : " + res);

            Console.WriteLine();

            // 3
            Console.WriteLine("***   task 3   ***");

            string row = "a345bBh$tz";

            var resRow = string.Join(" ",
                row.ToLower().Select(c => Convert.ToInt16(c) - 96).Where(x => x > 0 && x < 27).ToArray());
            Console.WriteLine(resRow);

            Console.WriteLine();

            //4
            Console.WriteLine("***   task 4   ***");

            string[] arr4 = {"a6s7d", "qw09e", "11zxc0", "asdf"};
            Console.WriteLine("init array: " + string.Join(", ", arr4));

            var res41 = string.Join("; ", arr4.Select((e, i) => string.Format(i + ":" + e)));
            Console.WriteLine("row number + value: " + res41);

            var res42 = arr4.Select(s => s.Where(char.IsNumber).Select(char.GetNumericValue).DefaultIfEmpty(0).Sum());
            Console.WriteLine("sum of numbers in rows: " + string.Join(", ", res42));

            Console.WriteLine();

            //5 
            Console.WriteLine("***   task 5   ***");
            Student[] studentArr =
            {
                new Student
                {
                    Name = "Student1",
                    Grades = new Dictionary<string, uint> {{"mathan", 97}, {"philosophy", 92}, {"pedagogy", 97}}
                },
                new Student
                {
                    Name = "Student2",
                    Grades = new Dictionary<string, uint> {{"mathan", 61}, {"philosophy", 60}, {"pedagogy", 65}}
                },
                new Student
                {
                    Name = "Student3",
                    Grades = new Dictionary<string, uint> {{"mathan", 88}, {"philosophy", 87}, {"pedagogy", 86}}
                },
                new Student
                {
                    Name = "Student4",
                    Grades = new Dictionary<string, uint> {{"mathan", 77}, {"philosophy", 78}, {"pedagogy", 79}}
                }
            };

            var res51 = studentArr.OrderBy(st => st.Grades.Average(x => x.Value));

            foreach (var student in res51)
            {
                Console.WriteLine(student.Name);
            }

            //var res52 = studentArr.GroupBy(st => st.Grades.Average(x => x.Value) > 80);

            //studentArr.Select(st => st.Grades.Average(x => x.Value));

            Console.WriteLine();

            //6 
            Console.WriteLine("***   task 6   ***");

            Customer[] customers =
            {
                new Customer
                {
                    Id = 1,
                    Name = "Customer 1"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Customer 2"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Customer 3"
                },
                new Customer
                {
                    Id = 4,
                    Name = "Customer 4"
                },
                new Customer
                {
                    Id = 5,
                    Name = "Customer 5"
                }
            };

            Product[] products =
            {
                new Product
                {
                    Id = 1,
                    Name = "Product 1"
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2"
                }
            };

            Transaction[] transactions =
            {
                new Transaction
                {
                    Date = DateTime.Today,
                    CustomerId = 1,
                    ProductId = 1
                },
                new Transaction
                {
                    Date = DateTime.Today,
                    CustomerId = 1,
                    ProductId = 2
                },
                new Transaction
                {
                    Date = DateTime.Today,
                    CustomerId = 2,
                    ProductId = 1
                },
                new Transaction
                {
                    Date = new DateTime(2017, 1, 18),
                    CustomerId = 1,
                    ProductId = 2
                },
                new Transaction
                {
                    Date = new DateTime(2018, 11, 22),
                    CustomerId = 2,
                    ProductId = 2
                },
                new Transaction
                {
                    Date = new DateTime(2018, 11, 22),
                    CustomerId = 5,
                    ProductId = 2
                }
            };

            var resJoin = customers.Join(transactions, (c => c.Id), (t => t.CustomerId), (cust, tr) => new {cust, tr})
                .Join(products, custtr => custtr.tr.ProductId, p => p.Id, (custtr, product) => new {custtr, product});

            var res61 = resJoin.Select(x =>
                new {name = x.custtr.cust.Name, product = x.product.Name, date = x.custtr.tr.Date}).OrderByDescending(x => x.date).ThenBy(y => y.name).ThenBy(z => z.product);

            foreach (var tr in res61)
            {
                Console.WriteLine(tr.date + " " + tr.name + " " + tr.product);
            }

            Console.WriteLine();
           
            var res62 = customers.Where(c => transactions.All(c2 => c2.CustomerId != c.Id));
            foreach (var customer in res62)
            {
                Console.WriteLine(customer.Name);
            }

            Console.WriteLine();

            var res63 = customers.Where(c => transactions.All(c2 => c2.CustomerId != c.Id || c2.Date < DateTime.Today.AddDays(-3)));
            foreach (var customer in res63)
            {
                Console.WriteLine(customer.Name);
            }

            Console.ReadLine();
        }
    }
}
