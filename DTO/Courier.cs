// File:    Courriers.cs
// Author:  bbiol
// Created: 17 October 2021 10:47:53
// Purpose: Definition of Class Courriers

using System;

public class Courier
{
    public int IdCourier { get; set; }
    public int IdArea { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }


    public override string ToString()
    {
        return "\nidCourier: " + IdCourier +
               "\nidArea: " + IdArea +
               "\nfirstName: " + FirstName +
               "\nlastName: " + LastName +
               "\nemailAddress: " + EmailAddress +
               "\npassword: " + Password;
    }
}