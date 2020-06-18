using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TheBestTable.Pages
{
    public class IndexModel : PageModel
    {
        private static int _personId = 0;
        private static readonly List<Person> Data = new Faker<Person>()
            .RuleFor(m => m.Id, f => _personId++)
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(m => m.Age, f => f.Random.Number(18, 65))
            .RuleFor(m => m.CompanyName, f => f.Company.CompanyName())
            .RuleFor(m => m.Country, f => f.Address.Country())
            .RuleFor(m => m.City, f => f.Address.City())
            .Generate(20);

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Person> People => Data;
        
        public RedirectToPageResult OnPostRemove(int id)
        {
            People.RemoveAll(p => p.Id == id);
            return RedirectToPage("Index");
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}