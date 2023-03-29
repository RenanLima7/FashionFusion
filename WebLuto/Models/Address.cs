﻿namespace WebLuto.Models
{
    public class Address
    {
        public long Id { get; set; }

        public Client Client { get; set; }

        public string AddressLine { get; set; }

        public string AddressLineNumber { get; set; }

        public string Neighborhood { get; set; }

        public string? ZipCode { get; set; }
    }
}
