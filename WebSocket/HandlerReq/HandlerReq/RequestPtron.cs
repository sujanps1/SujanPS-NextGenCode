using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HandlerReq


{
    public class Identity
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonPropertyName("idReadMethod")]
        public string IdReadMethod { get; set; }

    }


    public class Address
    {
        [JsonPropertyName("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class Contact
    {
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class Transaction
    {
        [JsonPropertyName("transactionSource")]
        public string TransactionSource { get; set; }

        [JsonPropertyName("playerCardNumber")]
        public string PlayerCardNumber { get; set; }

        [JsonPropertyName("patronId")]
        public int PatronId { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("ssn")]
        public string Ssn { get; set; }

        [JsonPropertyName("identity")]
        public Identity Identity { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("contact")]
        public Contact Contact { get; set; }

        [JsonPropertyName("performKYC")]
        public bool PerformKYC { get; set; }

        [JsonPropertyName("modifyBy")]
        public string ModifyBy { get; set; }

        [JsonPropertyName("encryptionType")]
        public int EncryptionType { get; set; }

        [JsonPropertyName("occupation")]
        public string Occupation { get; set; }
    }
}