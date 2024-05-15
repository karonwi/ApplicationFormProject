using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Interfaces;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Infrastructure.Implementation
{
    public class ApplicationFormRepository : IApplicationFormRepository
    {
        private readonly Container _container;

        public ApplicationFormRepository(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("FormsDatabase", "ApplicationFormsContainer");
        }

        public async Task<ApplicationFormModel> AddApplicationFormAsync(ApplicationFormModel applicationForm)
        {
            try
            {
             
                Console.WriteLine(JsonConvert.SerializeObject(applicationForm));

                return await _container.CreateItemAsync(applicationForm, new PartitionKey(applicationForm.userId.ToString()));
            }
            catch (CosmosException ex)
            {
                throw new Exception("Failed to add application form.", ex);
            }
        }

        public async Task<ApplicationFormModel> UpdateApplicationFormAsync(ApplicationFormModel applicationForm)
        {
            try
            {
                return await _container.UpsertItemAsync(applicationForm, new PartitionKey(applicationForm.userId.ToString()));
            }
            catch (CosmosException ex)
            {
                throw new Exception("Failed to update application form.", ex);
            }
        }

        public async Task DeleteApplicationFormAsync(Guid id)
        {
            try
            {
                await _container.DeleteItemAsync<ApplicationFormModel>(id.ToString(), new PartitionKey(id.ToString()));
            }
            catch (CosmosException ex)
            {
                throw new Exception("Failed to delete application form.", ex);
            }
        }

        public async Task<ApplicationFormModel> GetApplicationFormByIdAsync(Guid id)
        {
            try
            {
                ItemResponse<ApplicationFormModel> response = await _container.ReadItemAsync<ApplicationFormModel>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No application form found with ID {id}.", ex);
            }
            catch (CosmosException ex)
            {
                throw new Exception("Error retrieving application form.", ex);
            }
        }

        public async Task<IEnumerable<ApplicationFormModel>> GetAllApplicationFormsAsync()
        {
            try
            {
                var query = "SELECT * FROM c";
                var iterator = _container.GetItemQueryIterator<ApplicationFormModel>(query);
                List<ApplicationFormModel> applicationForms = new List<ApplicationFormModel>();

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    applicationForms.AddRange(response);
                }

                return applicationForms;
            }
            catch (CosmosException ex)
            {
                throw new Exception("Failed to retrieve all application forms.", ex);
            }
        }
    }
    
}
