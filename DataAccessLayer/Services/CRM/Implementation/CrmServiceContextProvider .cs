using DataAccessLayer.Services.CRM.Contract;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DataAccessLayer.Services.CRM.Implementation
{
    public class CrmServiceContextProvider : ICrmServiceContextProvider
    {
        private readonly string _connectionString;
        private ServiceClient _cachedClient;
        private OrganizationServiceContext _cachedContext;

        public CrmServiceContextProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IOrganizationService GetService()
        {
            // Create client once and reuse it
            if (_cachedClient == null)
            {
                _cachedClient = new ServiceClient(_connectionString);

                if (!_cachedClient.IsReady)
                    throw new Exception($"CRM connection failed: {_cachedClient.LastError}");
            }

            return _cachedClient;
        }

        public OrganizationServiceContext GetContext()
        {
            // Cache context to reuse across Add and Save
            if (_cachedContext == null)
            {
                _cachedContext = new OrganizationServiceContext(GetService());
            }

            return _cachedContext;
        }

    }
 }
