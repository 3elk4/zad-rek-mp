﻿namespace MediportaZadRek.Data.Exceptions
{
    [Serializable]
    internal class InvalidResponseException : Exception
    {
        public InvalidResponseException()
        {
        }

        public InvalidResponseException(string? message) : base(message)
        {
        }

        public InvalidResponseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}