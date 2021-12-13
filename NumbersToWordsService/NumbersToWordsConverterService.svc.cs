namespace NumbersToWordsService
{
    public class NumbersToWordsConverterService : INumbersToWordsConverterService
    {
        private readonly NumberToWordsConverter _converter;

        public NumbersToWordsConverterService()
        {
            _converter = new NumberToWordsConverter();
        }

        public string Convert(string input)
        {
            return _converter.Convert(input);
        }
    }
}
