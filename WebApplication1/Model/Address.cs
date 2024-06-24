﻿namespace WebApplication1.Model
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Foreign key
        public int EmployeeId { get; set; }

        // Navigation property
        public Employee Employee { get; set; }
    }
}
