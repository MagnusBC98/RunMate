namespace RunMate.Application.Exceptions;

public class ValidationException(string message) : Exception(message)
{
}