using System.ServiceModel;

namespace NumbersToWordsService
{
    [ServiceContract]
    public interface INumbersToWordsConverterService
    {
        [OperationContract]
        string Convert(string input);
    }
}
