// File:    Restaurants.cs
// Author:  bbiol
// Created: 17 October 2021 10:47:53
// Purpose: Definition of Class Restaurants

using System;

public class Restaurant
{
    public int IdRestaurant { get; set; }
    public int IdArea { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Image { get; set; }
    public string Logo { get; set; }

    public override string ToString()
    {
        return "idRestaurant: " + IdRestaurant +
               "\nidArea: " + IdArea +
               "\nname: " + Name +
               "\nemailAddress: " + EmailAddress +
               "\npassword: " + Password +
               "\nimage: " + Image +
               "\nlogo: " + Logo;
    }
}