using System.Text;


namespace Analysis
{
    class TextAnalyzer : StreamReader
    {
        private Dictionary<string, int> _Words { get; }
        public int _WordCount { get; private set; }
        public int _CharactersNoSpacesCount { get; private set; }
        public int _CharactersCount { get; private set; }
        public List<string> TrimmedLines { get; private set; }

        public TextAnalyzer(string filename) : base(filename)
        {
            _Words = new Dictionary<string, int>();
            _WordCount = 0;
            _CharactersNoSpacesCount = 0;
            _CharactersCount = 0;
            TrimmedLines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Analyzuj(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Soubor jménem {filename} neexistuje.");
            }
            catch (Exception)
            {
                Console.WriteLine("Input File Error");
            }
        }

        private void Analyzuj(string line)
        {
            var WordsLine = line.Split( ' ', '\t', '\n', '\r' );
            StringBuilder sb = new StringBuilder();

            foreach (var word in WordsLine)
            {
                if (word != null && word != "")
                {
                    _WordCount++;
                    string _word = word.Trim();
                    sb.Append(_word);
                    sb.Append(' ');
                    _word.ToLower();

                    if (_Words.ContainsKey(_word))
                    {
                        _Words[_word]++;
                    }
                    else
                    {
                        _Words[_word] = 1;
                    }
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            TrimmedLines.Add(sb.ToString());

            _CharactersNoSpacesCount += line.Count(c => !Char.IsWhiteSpace(c));
            _CharactersCount += line.Length + 2;
        }

        private string TrimmedText()
        {
            return string.Join(Environment.NewLine, TrimmedLines);
        }

        public void Write_out(string vystup)
        {
            try
            {
                using (StreamWriter writer = new(vystup))
                {
                    writer.WriteLine("Počet slov: " + _WordCount);
                    writer.WriteLine("Počet znaků (bez bílých znaků): " + _CharactersNoSpacesCount);
                    writer.WriteLine("Počet znaků (s bílými znaky): " + (_CharactersCount-2));
                    writer.WriteLine();

                    foreach (KeyValuePair<string, int> stat in _Words)
                    {
                        writer.WriteLine(stat.Key + ": " + stat.Value);
                    }
                    writer.WriteLine();
                    writer.WriteLine(TrimmedText());
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Soubor jménem {vystup} neexistuje.");
            }
            catch (Exception)
            {
                Console.WriteLine("Input File Error");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string inputfile = "2_vstup.txt";
            string outputfile = "2_vystup.txt";
            try
            {
                TextAnalyzer textAnalyzer = new(inputfile);
                textAnalyzer.Write_out(outputfile);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Soubor jménem {vystup} neexistuje.");
            }
            catch (Exception)
            {
                Console.WriteLine("Input File Error");
            }
        }

    }
}
