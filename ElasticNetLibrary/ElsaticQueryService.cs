using Nest;
using System;
using System.Collections.Generic;
using ElasticDomain;

namespace ElasticNetLibrary
{
    /// <summary>
    /// ElsaticQueryService
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElsaticQueryService<T> where T: BaseDocument<Guid>
    {
        private readonly string _url = "http://localhost:9200";
        private readonly string _indexName = "elasticindex";
        private readonly IElasticClient _client;

        public ElsaticQueryService(string url, string indexName)
        {
            if (!string.IsNullOrEmpty(url))
                _url = url;
            if (!string.IsNullOrEmpty(indexName))
                _indexName = indexName;
            var node = new Uri(_url);
            var settings = new ConnectionSettings(node);
            _client = new ElasticClient(settings);
        }

        public ElasticQueryResult<string> CreateIndex()
        {
            var model = new ElasticQueryResult<string>();
            var result = _client.CreateIndex(_indexName);
            if (!result.IsValid)
            {
                model.ErrorMessage = result.OriginalException.Message;
                return model;
            }
            model.IsSuccess = true;
            model.Data = "Index created";
            return model;
        }

        public ElasticQueryResult<string> AddDocument(T document)
        {
            var model = new ElasticQueryResult<string>();
            var result = _client.Index(document, idx => idx.Index(_indexName));
            if (!result.IsValid)
            {
                model.ErrorMessage = result.OriginalException.Message;
                return model;
            }
            model.IsSuccess = true;
            model.Data = "Document added";
            return model;
        }

        public ElasticQueryResult<T> GetDocumentById(Guid id)
        {
            var model = new ElasticQueryResult<T>();
            var response = _client.Get<T>(id, idx => idx.Index(_indexName)); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
            var document = response.Source; // the original document
            model.IsSuccess = true;
            model.Data = document;
            return model;
        }

        public ElasticQueryResult<string> DeleteDocumentById(Guid id)
        {
            var model = new ElasticQueryResult<string>();
            var response = _client.Delete<T>(id, idx => idx.Index(_indexName));
            var result = response.Result;
            model.Data = result.ToString();
            model.IsSuccess = true;
            return model;
        }

        public ElasticQueryResult<IReadOnlyCollection<IHit<T>>> FindData(Func<QueryContainerDescriptor<T>, QueryContainer> query, int  skip = 0, int take = 10)
        {
            var model = new ElasticQueryResult<IReadOnlyCollection<IHit<T>>>();
            var response = _client.Search<T>(s => s.Index(_indexName)
                .From(skip)
                .Size(take)
                .Query(query)
            );

            //var response = client.Search<Document>(s => s.Index(indexName)
            //    .From(0)
            //    .Size(10)
            //    .Query(q => q
            //                    .Term(t => t.User, "kimchy") || q
            //                    .Match(mq => mq.Field(f => f.Text).Query("nest"))
            //    )
            //);

            model.Data = response.Hits;
            model.IsSuccess = true;
            return model;
        }
    }
}
