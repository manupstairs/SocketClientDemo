using System;
namespace SocketClientForm
{
    class Encryption
    {
        unsafe public int AlphabetTransfer(char* Alphabet)
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

            AlphabetSerialAfter = (char)(AlphabetSerialBefore * 3);

            if (AlphabetSerialAfter > 52)
                AlphabetSerialAfter = (char)(AlphabetSerialAfter % 52);

            if (AlphabetSerialAfter < 27)
                AlphabetAfter = (char)('a' + AlphabetSerialAfter - 1);

            if (AlphabetSerialAfter >= 27 && AlphabetSerialAfter < 53)
                AlphabetAfter = (char)('A' + AlphabetSerialAfter - 27);

            *Alphabet = AlphabetAfter;

            return 1;
        }


        unsafe public int StringTransfer(char* StringHead)
        {
            char* PString = StringHead;

            while (*PString != '\0')
            {
                if (AlphabetTransfer(PString) == 0) return 0;
                PString++;
            }
            return 1;
        }
    }
}