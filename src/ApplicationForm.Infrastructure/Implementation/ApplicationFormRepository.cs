using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Interfaces;
using Microsoft.Azure.Cosmos;
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
            return await _container.CreateItemAsync(applicationForm, new PartitionKey(applicationForm.Id.ToString()));
        }

        public async Task<ApplicationFormModel> UpdateApplicationFormAsync(ApplicationFormModel applicationForm)
        {
            return await _container.UpsertItemAsync(applicationForm, new PartitionKey(applicationForm.Id.ToString()));
        }

        public async Task DeleteApplicationFormAsync(Guid id)
        {
            await _container.DeleteItemAsync<ApplicationFormModel>(id.ToString(), new PartitionKey(id.ToString()));
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
                return null;
            }
        }

        public async Task<IEnumerable<ApplicationFormModel>> GetAllApplicationFormsAsync()
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
    }
}
