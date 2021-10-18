// File:    Dishes.cs
// Author:  bbiol
// Created: 17 October 2021 10:47:53
// Purpose: Definition of Class Dishes

using System;

public class Dish
{
    public int IdDish { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Image { get; set; }
    public bool IsAvailable { get; set; }

    public override string ToString()
    {
        return "IdDish: " + IdDish +
               "\nName: " + Name +
               "\nPrice: " + Price+
               "\nImage: " + Image +
               "\nIsAvailable: " + IsAvailable;
    }

}