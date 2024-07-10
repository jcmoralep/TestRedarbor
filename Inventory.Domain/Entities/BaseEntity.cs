﻿namespace Inventory.Domain.Entities;
public abstract class BaseEntity
{
    public int Id { get; set; }

    public int Status { get; set; }

    protected BaseEntity()
    {
        Id = 0;
    }
}
