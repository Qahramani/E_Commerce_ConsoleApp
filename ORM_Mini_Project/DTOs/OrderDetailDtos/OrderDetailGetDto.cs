﻿using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.OrderDetailDtos;

public class OrderDetailGetDto
{
    public int Id { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
}
