﻿namespace GeradorDeBoletos.Domain.Features.Shared.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message)
    {
        
    }
}
