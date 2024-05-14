using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using ApplicationForm.Domain.Interfaces;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Infrastructure.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly Microsoft.Azure.Cosmos.Container _container;
        public QuestionRepository(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("FormsDatabase", "QuestionsContainer");
        }
        public async Task<Question> AddQuestionAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            try
            {
                Console.WriteLine($"Partition Key: {question.GetPartitionKeyValue()}");
                Console.WriteLine($"Question Data: {JsonConvert.SerializeObject(question)}");
                return await _container.CreateItemAsync(question, new PartitionKey(question.GetPartitionKeyValue()));
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"CosmosException: {ex.StatusCode}, Message: {ex.Message}");
                throw new Exception($"Failed to add question: {ex.Message}", ex);
            }
        }

        public async Task DeleteQuestionAsync(Guid id, QuestionType type)
        {
            try
            {
  
                int partitionKeyValue = (int)type;

                await _container.DeleteItemAsync<Question>(id.ToString(), new PartitionKey(partitionKeyValue));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Question not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting the question", ex);
            }
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            try
            {
                var query = "SELECT * FROM c";
                var iterator = _container.GetItemQueryIterator<Question>(query);
                List<Question> questions = new List<Question>();

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    questions.AddRange(response.Resource);
                }

                return questions;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve questions", ex);
            }
        }

        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                    .WithParameter("@id", id.ToString());

                var iterator = _container.GetItemQueryIterator<Question>(query);
                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    foreach (var item in response)
                    {
                        return item; 
                    }
                }

                return null; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving the question by ID", ex);
            }
        }

        public async Task UpdateQuestionAsync(Question question)
        {

            try
            {
                if (question == null)
                    throw new ArgumentNullException(nameof(question));

                int partitionKeyValue = question.GetPartitionKeyValue();
                await _container.UpsertItemAsync(question, new PartitionKey(partitionKeyValue));
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Failed to update question: {ex.Message}", ex);
            }
        }
    }
}
