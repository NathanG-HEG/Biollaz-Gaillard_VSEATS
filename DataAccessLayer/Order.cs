// File:    Order.cs
// Author:  bbiol
// Created: 17 October 2021 10:47:53
// Purpose: Definition of Class Order

using System;

public class Order
{
    public int idOrder { get; set; }
    public DateTime expectedDeliveryTime { get; set; }
    public DateTime timeOfOrder { get; set; }
    public DateTime timeOfDelivery { get; set; }
    public string deliveryAddress { get; set; }
    public int orderTotal { get; set; }



}