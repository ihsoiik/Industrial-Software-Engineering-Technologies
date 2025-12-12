namespace CsWinFkey
{
    public static class TemplateConstants
    {
        // Символ-разделитель частей шаблона
        public const char Delimiter = ' ';

        // Индикаторы начала и конца шаблона
        public const char StartTemplate = '(';
        public const char EndTemplate = ')';

        // Маркеры количества символов для обработки
        public const char AllSymbols = 'a';
        public const char SingleSymbol = '1';

        // Символы для добавления после части шаблона
        public const char Space = ' ';
        public const char Dash = '-';
        public const char Point = '.';
        public const char None = 'D';

        // Действия с регистром букв
        public const char ToUpper = 'V';
        public const char ToLower = 'N';
        public const char SaveCase = 'D';

        // Матрица допустимых управляющих символов для каждой позиции в шаблоне
        public static readonly char[] TemplateFirstSymbols = { ToUpper, ToLower, SaveCase };
        public static readonly char[] TemplateSecondSymbols = { AllSymbols, SingleSymbol };
        public static readonly char[] TemplateThirdSymbols = { Dash, Space, Point, None };

        public static readonly char[][] TemplatePartsSymbols =
        {
            TemplateFirstSymbols,
            TemplateSecondSymbols,
            TemplateThirdSymbols
        };
    }
}