﻿namespace BuildingBlock.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    public EntityNotFoundException()
    {
    }

    protected EntityNotFoundException(string entity, Guid id) : base($"{entity} with id: {id} not found")
    {
    }

    protected EntityNotFoundException(string entity, string column, object value) : base(
        $"{entity} with {column}: {value} not found")
    {
    }
}