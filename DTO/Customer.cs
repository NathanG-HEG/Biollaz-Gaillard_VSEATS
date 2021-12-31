// File:    Customers.cs
// Author:  bbiol
// Created: 17 October 2021 10:47:53
// Purpose: Definition of Class Customers

using System;

public class Customer
{
    public int IdCustomer { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PwdHash { get; set; }
    public string Salt { get; set; }

}