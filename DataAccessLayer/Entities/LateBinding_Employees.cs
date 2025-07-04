using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{

    //[EntityLogicalName("cr5c1_employee")]
    public class LateBinding_Employee : Entity
    {
        public const string LogicalName = "cr5c1_employee";

        public LateBinding_Employee() : base(LogicalName) { }

        [AttributeLogicalName("cr5c1_employeeid")]
        public Guid ID
        {
            get => GetAttributeValue<Guid>("cr5c1_employeeid");
            set => SetAttributeValue("cr5c1_employeeid", value);
        }

        [AttributeLogicalName("cr5c1_employeename")]
        public string Name
        {
            get => GetAttributeValue<string>("cr5c1_employeename");
            set => SetAttributeValue("cr5c1_employeename", value);
        }

        [AttributeLogicalName("cr5c1_email")]
        public string Email
        {
            get => GetAttributeValue<string>("cr5c1_email");
            set => SetAttributeValue("cr5c1_email", value);
        }

        [AttributeLogicalName("cr5c1_phone")]
        public string Phone
        {
            get => GetAttributeValue<string>("cr5c1_phone");
            set => SetAttributeValue("cr5c1_phone", value);
        }

        [AttributeLogicalName("cr5c1_position")]
        public string Position
        {
            get => GetAttributeValue<string>("cr5c1_position");
            set => SetAttributeValue("cr5c1_position", value);
        }

        [AttributeLogicalName("cr5c1_managerid")]
        public Guid ManagerID
        {
            get => GetAttributeValue<Guid>("cr5c1_managerid");
            set => SetAttributeValue("cr5c1_managerid", value);
        }

        // Optional: navigation property if you have relationship metadata loaded
        [AttributeLogicalName("cr5c1_managerid")]
        public EntityReference ManagerRef
        {
            get => GetAttributeValue<EntityReference>("cr5c1_managerid");
            set => SetAttributeValue("cr5c1_managerid", value);
        }

    }



    //[EntityLogicalName("cr5c1_employee")]
    //public class Employee : Entity
    //{
    //    public const string LogicalName = "cr5c1_employee";

    //    public Employee() : base(LogicalName) { }

    //    [JsonProperty("cr5c1_employeeid")]
    //    [JsonPropertyName("cr5c1_employeeid")]
    //    public Guid ID { get; set; }

    //    [JsonProperty("cr5c1_employeename")]
    //    [JsonPropertyName("cr5c1_employeename")]
    //    public string Name { get; set; }

    //    [JsonProperty("cr5c1_email")]
    //    [JsonPropertyName("cr5c1_email")]
    //    public string Email { get; set; }


    //    [JsonProperty("cr5c1_phone")]
    //    [JsonPropertyName("cr5c1_phone")]
    //    public string Phone { get; set; }

    //    [JsonProperty("cr5c1_position")]
    //    [JsonPropertyName("cr5c1_position")]
    //    public string Position { get; set; }

    //    [JsonProperty("cr5c1_managerid")]
    //    [JsonPropertyName("cr5c1_managerid")]
    //    public Guid ManagerID { get; set; } = new Guid( "8ba0d235-ba72-487c-aa38-660495ffc19c");

    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public virtual Employee Manager { get; set; }





}

