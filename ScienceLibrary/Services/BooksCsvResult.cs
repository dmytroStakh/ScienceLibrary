using Microsoft.AspNetCore.Mvc;
using ScienceLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Services
{
    public class BooksCsvResult : FileResult
    {
        private readonly IEnumerable<Book> _bookData;
        public BooksCsvResult(IEnumerable<Book> employeeData, string fileDownloadName) : base("text/csv")
        {
            _bookData = employeeData;
            FileDownloadName = fileDownloadName;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
            using (var streamWriter = new StreamWriter(response.Body))
            {
                await streamWriter.WriteLineAsync(
                  $"ID, Title, Author"
                );
                foreach (var book in _bookData)
                {
                    await streamWriter.WriteLineAsync(
                      $"{book.Id}, {book.Title}, {book.Author}"
                    );
                    await streamWriter.FlushAsync();
                }
                await streamWriter.FlushAsync();
            }
        }

    }
}
