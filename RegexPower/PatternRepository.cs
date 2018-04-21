using System;

namespace RegexPower
{
    class PatternRepository
    {
        public string emailPattern()
        {
            return @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        }

        public string findByPartPattern(string st)
        {
            return $@"{st}(\w*)";
        }
    }
}
