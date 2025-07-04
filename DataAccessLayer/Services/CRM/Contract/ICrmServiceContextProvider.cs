using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.CRM.Contract
{
    public interface ICrmServiceContextProvider
    {
        OrganizationServiceContext GetContext();
        IOrganizationService GetService();
    }
}
