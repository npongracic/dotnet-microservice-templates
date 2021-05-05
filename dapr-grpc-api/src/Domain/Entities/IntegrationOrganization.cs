using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class IntegrationOrganization
    {
        public long Id { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsUpdate { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string Oib { get; set; }
        public string AddressStreet { get; set; }
        public string AddressBuildingNumber { get; set; }
        public string AddressPostalOfficeCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string ErrorDetails { get; set; }
        public bool IsProcessed { get; set; }
    }
}
