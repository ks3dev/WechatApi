﻿namespace Infrastructure.Core.DDD
{
    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }
    }
}
