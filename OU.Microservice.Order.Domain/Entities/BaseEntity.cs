﻿namespace OU.Microservice.Order.Domain.Entities
{
    public class BaseEntity<TEntityId>
    {
        public TEntityId Id { get; set; } = default!;
    }
}
