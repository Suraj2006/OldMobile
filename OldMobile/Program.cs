using System;
using System.Collections.Generic;
using System.Text;
class Program
{
    static void Main()
    {
        // Test cases
        Console.WriteLine(OldPhonePad("33#"));      // Output: E
        Console.WriteLine(OldPhonePad("22#"));    // Output: B
        Console.WriteLine(OldPhonePad("44 33 555 555 666#")); // Output: HELLO
        
    }
    public static string OldPhonePad(string input)
    {
        // Create the keypad mapping
        Dictionary<char, string> keypad = new Dictionary<char, string>
        {
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }
        };

        // Decode the input
        string output = DecodeKeypadInput(input, keypad);
        return output;


    }

    static string DecodeKeypadInput(string input, Dictionary<char, string> keypad)
    {
        // Remove trailing '#' if present
        if (input.EndsWith("#"))
        {
            input = input.Substring(0, input.Length - 1);
        }

        StringBuilder result = new StringBuilder();
        StringBuilder currentSequence = new StringBuilder();
        bool typingSequence = false;

        foreach (char ch in input)
        {
            if (ch == ' ')
            {
                // Process the current sequence if any
                if (currentSequence.Length > 0)
                {
                    ProcessSequence(currentSequence.ToString(), keypad, result);
                    currentSequence.Clear();
                }
                typingSequence = false;
            }
            else
            {
                // If typing sequence, accumulate the character
                if (typingSequence)
                {
                    currentSequence.Append(ch);
                }
                else
                {
                    // New sequence
                    if (currentSequence.Length > 0)
                    {
                        ProcessSequence(currentSequence.ToString(), keypad, result);
                        currentSequence.Clear();
                    }
                    currentSequence.Append(ch);
                    typingSequence = true;
                }
            }
        }

        // Process the last sequence if any
        if (currentSequence.Length > 0)
        {
            ProcessSequence(currentSequence.ToString(), keypad, result);
        }

        return result.ToString();
    }

    static void ProcessSequence(string sequence, Dictionary<char, string> keypad, StringBuilder result)
    {
        char button = sequence[0];
        int pressCount = sequence.Length;

        if (keypad.ContainsKey(button))
        {
            string letters = keypad[button];
            // Get the correct letter
            char letter = letters[(pressCount - 1) % letters.Length];
            result.Append(letter);
        }
    }
}


