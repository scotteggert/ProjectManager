namespace Estimator.Domain.Exceptions;
public class EstimatorDomainException : Exception
{
    public EstimatorDomainException()
    { }

    public EstimatorDomainException(string message)
        : base(message)
    { }

    public EstimatorDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
