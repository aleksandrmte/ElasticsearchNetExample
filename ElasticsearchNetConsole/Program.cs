using System;
using System.Collections.Generic;
using DomainModel;
using ElasticNetLibrary;
using Nest;
using Page = DomainModel.Page;

namespace ElasticsearchNetConsole
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("ElasticSearch");
            var elasticService = new ElsaticQueryService<Document>(null, null);

            //Создание индекса
            //var resultCreated = elasticService.CreateIndex();
            //Console.WriteLine(resultCreated.IsSuccess ? resultCreated.Data : resultCreated.ErrorMessage);
            //Заполнение индекса документами
            //var i = 0;
            //while (i < 10000)
            //{
            //    elasticService.AddDocument(GetNewDocument());
            //    i++;
            //}

            //Запрос к эластику, найти документы у которых в имени автора 'motr' и в названии страны 'Russia'
            Func<QueryContainerDescriptor<Document>, QueryContainer> query = q =>
                q.Match(mq => mq.Field(f => f.Author.FullName).Query("motr")) && q.Match(mq => mq.Field(f => f.Author.Country.Name).Query("Russia"));
            var result = elasticService.FindData(query, 0, 10000);
            Console.WriteLine($"Found: {result.Data.Count}");
            Console.ReadKey();
        }

        /// <summary>
        /// GetNewDocument
        /// </summary>
        /// <returns></returns>
        private static Document GetNewDocument()
        {
            var rnd = new Random();
            var authors = new[] { "Ivan Petrov", "Max Ferstappen", "Fridrih Alonso", "Ive Lkein", "Torada Kinava", "Aurika Motr" };
            var countries = new[] { "Russia", "USA", "England", "Scotland", "China", "Australis" };
            var docTypes = new[] {DocumentType.Protocol, DocumentType.Act, DocumentType.Letter};

            return new Document
            {
                Id = Guid.NewGuid(),
                Author = new Author
                {
                    Id = 1,
                    BirthDate = DateTime.UtcNow,
                    FullName = authors[rnd.Next(0, authors.Length)],
                    Country = new Country
                    {
                        Id = 1,
                        Name = countries[rnd.Next(0, countries.Length)]
                    },
                    CountryId = 1
                },
                AuthorId = 1,
                Created = DateTime.UtcNow,
                CountPages = 3,
                Title = "New title " + new Random().Next(1000),
                DocumentType = docTypes[rnd.Next(0, docTypes.Length)],
                Pages = new List<Page>
                {
                    new Page
                    {
                        Id = 1,
                        Content = "text 1",
                        Number = 1
                    },
                    new Page
                    {
                        Id = 2,
                        Content = "text 2222",
                        Number = 2
                    },
                    new Page
                    {
                        Id = 3,
                        Content = "text 33333333333",
                        Number = 3
                    },
                }
            };
        }

    }
}
