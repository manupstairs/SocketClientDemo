using System;

namespace SocketClientForm
{

    class Decode
    {
        unsafe public int AlphabetTransBack(char* Alphabet)
        {
            char AlphabetBefore = *Alphabet;
            char AlphabetAfter = (char)0;

            char AlphabetSerialBefore = (char)0;
            char AlphabetSerialAfter = (char)0;

            if (AlphabetBefore >= 'a' && AlphabetBefore <= 'z')
                AlphabetSerialBefore = (char)(AlphabetBefore - 'a' + 1);

            if (AlphabetBefore >= 'A' && AlphabetBefore <= 'Z')
                AlphabetSerialBefore = (char)(AlphabetBefore - 'A' + 27);

            if (AlphabetSerialBefore == 0) return 0;

            if (AlphabetSerialBefore % 3 == 0) AlphabetSerialAfter = (char)(AlphabetSerialBefore / 3);
            else if ((AlphabetSerialBefore + 52) % 3 == 0) AlphabetSerialAfter = (char)((AlphabetSerialBefore + 52) / 3);
            else if ((AlphabetSerialBefore + 104) % 3 == 0) AlphabetSerialAfter = (char)((AlphabetSerialBefore + 104) / 3);
            else return 0;

            if (AlphabetSerialAfter < 27)
                AlphabetAfter = (char)('a' + AlphabetSerialAfter - 1);

            if (AlphabetSerialAfter >= 27 && AlphabetSerialAfter < 53)
                AlphabetAfter = (char)('A' + AlphabetSerialAfter - 27);

            *Alphabet = AlphabetAfter;

            return 1;
        }

        unsafe public int StringTransBack(char* StringHead)
        {
            char* PString = StringHead;

            while (*PString != '\0')
            {
                if (AlphabetTransBack(PString) == 0) return 0;
                PString++;
            }
            return 1;
        }
    }
}