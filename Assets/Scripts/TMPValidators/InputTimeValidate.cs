using TMPro;
using UnityEngine;

namespace TMPValidators
{
    [CreateAssetMenu(fileName = "Input Field Validate - HH:mm", menuName = "Input Field Validator")]
    public class InputTimeValidate : TMP_InputValidator
    {
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (!char.IsDigit(ch))
            {
                return '\0';
            }

            text = text.Insert(pos, ch.ToString());
            pos++;

            if (text.Contains("::"))
            {
                text = text.Replace("::", ":");
            }

            if (text.Length > 5)
            {
                text = text.Substring(0, 5);
                pos = text.Length;
            }

            if (text.Length == 4)
            {
                text = $"{text.Substring(0, 2)}:{text.Substring(2, 2)}";
                pos = text.Length;
            }

            if (text.Length == 5)
            {
                string[] parts = text.Split(':');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int hours) &&
                        int.TryParse(parts[1], out int minutes))
                    {
                        if (hours > 23)
                        {
                            hours = 23;
                            text = $"{hours:D2}:{minutes:D2}";
                        }
                        if (minutes > 59)
                        {
                            minutes = 59;
                            text = $"{hours:D2}:{minutes:D2}";
                        }
                        pos = text.Length;
                    }
                }
            }

            return '\0';
        }
    }
}