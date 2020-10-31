using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

namespace Hill
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> eng_abc = new List<char>() {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z', '_',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '_'};
            int n = 27;
            //Шифр Хилла
            /*
            {
                int[,] key = new int[3, 3];
                int[] key1 = new int[9];
            Key_in:
                Console.WriteLine("Введите матрицу-ключ (ввод осуществляется построчно)");
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        key[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                int opred = Opred(key);
                Console.WriteLine($"Определитель равен {opred}");
                if (NOD(n, opred) == false || opred == 0)
                {
                    Console.WriteLine("Неподходящий ключ");
                    goto Key_in;
                }
                else
                {
                    Console.WriteLine("Введите текст, который необходимо зашифровать:");
                    string text = Console.ReadLine();
                    if (text.Length % 3 != 0)
                    {
                        int i = text.Length;
                        while (i % 3 != 0)
                        {
                            text = $"{text}_";
                            i++;
                        }
                        Console.WriteLine(text);
                    }
                    //Шифровка
                    char[] text_encoded = new char[text.Length];
                    text_encoded = text.ToCharArray();
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text_encoded[i] == ' ') text_encoded[i] = '_';
                    }
                    Console.WriteLine(text_encoded);
                    int[] open_codes = new int[text.Length];
                    int[] close_codes = new int[text.Length];
                    Console.WriteLine("Коды:");
                    for (int i = 0; i < text.Length; i++)
                    {
                        open_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                        Console.Write($"{open_codes[i]}  ");
                    }
                    int a = 0;
                    Console.WriteLine("\nЗашифрованный текст:");
                    while (a < text.Length)
                    {
                        try
                        {

                            int ost = a % 3;
                            switch (ost)
                            {
                                case 0:
                                    close_codes[a] = (key[0, 0] * open_codes[a] + key[0, 1] * open_codes[a + 1] + key[0, 2] * open_codes[a + 2]) % 27;
                                    text_encoded[a] = eng_abc[close_codes[a]];
                                    Console.Write(text_encoded[a]);
                                    a++;
                                    break;
                                case 1:
                                    close_codes[a] = (key[1, 0] * open_codes[a - 1] + key[1, 1] * open_codes[a] + key[1, 2] * open_codes[a + 1]) % 27;
                                    text_encoded[a] = eng_abc[close_codes[a]];
                                    Console.Write(text_encoded[a]);
                                    a++;
                                    break;
                                case 2:
                                    close_codes[a] = (key[2, 0] * open_codes[a - 2] + key[2, 1] * open_codes[a - 1] + key[2, 2] * open_codes[a]) % 27;
                                    text_encoded[a] = eng_abc[close_codes[a]];
                                    Console.Write(text_encoded[a]);
                                    a++;
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    //Дешифровка
                    Console.WriteLine("\nОбратная матрица ключа:");
                    int[,] key_temp = new int[3, 3];
                    int[,] key_obr = new int[3, 3];
                    int M1 = (key[1, 1] * key[2, 2] - key[1, 2] * key[2, 1]);
                    if (M1 > 27) M1 = M1 % 27;
                    else M1 = mod_otr(M1, 27);
                    int M2 = (-(key[1, 0] * key[2, 2] - key[1, 2] * key[2, 0]));
                    if (M2 > 27) M2 = M2 % 27;
                    else M2 = mod_otr(M2, 27);
                    int M3 = (key[1, 0] * key[2, 1] - key[1, 1] * key[2, 0]);
                    if (M3 > 27) M3 = M3 % 27;
                    else M3 = mod_otr(M3, 27);
                    int M4 = (-(key[0, 1] * key[2, 2] - key[0, 2] * key[2, 1]));
                    if (M4 > 27) M4 = M4 % 27;
                    else M4 = mod_otr(M4, 27);
                    int M5 = (key[0, 0] * key[2, 2] - key[0, 2] * key[2, 0]);
                    if (M5 > 27) M5 = M5 % 27;
                    else M5 = mod_otr(M5, 27);
                    int M6 = (-(key[0, 0] * key[2, 1] - key[0, 1] * key[2, 0]));
                    if (M6 > 27) M6 = M6 % 27;
                    else M6 = mod_otr(M6, 27);
                    int M7 = (key[0, 1] * key[1, 2] - key[0, 2] * key[1, 1]);
                    if (M7 > 27) M7 = M7 % 27;
                    else M7 = mod_otr(M7, 27);
                    int M8 = (-(key[0, 0] * key[1, 2] - key[0, 2] * key[1, 0]));
                    if (M8 > 27) M8 = M8 % 27;
                    else M8 = mod_otr(M8, 27);
                    int M9 = (key[0, 0] * key[1, 1] - key[1, 0] * key[0, 1]);
                    if (M9 > 27) M9 = M9 % 27;
                    else M9 = mod_otr(M9, 27);
                    key_temp[0, 0] = M1; key_temp[0, 1] = M4; key_temp[0, 2] = M7;
                    key_temp[1, 0] = M2; key_temp[1, 1] = M5; key_temp[1, 2] = M8;
                    key_temp[2, 0] = M3; key_temp[2, 1] = M6; key_temp[2, 2] = M9;
                    int opred_obr = Alpha_Obr(opred, n);
                    if (opred_obr > 27) opred_obr = opred_obr % 27;
                    else opred_obr = mod_otr(opred_obr, 27);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            key_obr[i, j] = opred_obr * key_temp[i, j];
                            if (key_obr[i, j] > 27) key_obr[i, j] = key_obr[i, j] % 27;
                            else key_obr[i, j] = mod_otr(key_obr[i, j], 27);
                            Console.Write($"{key_obr[i, j]}  ");
                        }
                        Console.Write("\n");
                    }
                    int b = 0;
                    Console.WriteLine("Коды:");
                    for (int i = 0; i < text.Length; i++)
                    {
                        close_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                        Console.Write($"{close_codes[i]} ");
                    }
                    Console.WriteLine("\nРасшифрованнй текст:");
                    while (b < text.Length)
                    {
                        try
                        {
                            int ost = b % 3;
                            switch (ost)
                            {
                                case 0:
                                    open_codes[b] = (key_obr[0, 0] * close_codes[b] + key_obr[0, 1] * close_codes[b + 1] + key_obr[0, 2] * close_codes[b + 2]) % 27;
                                    text_encoded[b] = eng_abc[open_codes[b]];
                                    Console.Write(text_encoded[b]);
                                    b++;
                                    break;
                                case 1:
                                    open_codes[b] = (key_obr[1, 0] * close_codes[b - 1] + key_obr[1, 1] * close_codes[b] + key_obr[1, 2] * close_codes[b + 1]) % 27;
                                    text_encoded[b] = eng_abc[open_codes[b]];
                                    Console.Write(text_encoded[b]);
                                    b++;
                                    break;
                                case 2:
                                    open_codes[b] = (key_obr[2, 0] * close_codes[b - 2] + key_obr[2, 1] * close_codes[b - 1] + key_obr[2, 2] * close_codes[b]) % 27;
                                    text_encoded[b] = eng_abc[open_codes[b]];
                                    Console.Write(text_encoded[b]);
                                    b++;
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            */
            //Рекуррентный шифр Хилла
            Console.WriteLine("\n\n");
            {
                int[,] key1 = new int[3, 3];
                int[,] key2 = new int[3, 3];
            Key1_in:
                Console.WriteLine("Введите матрицу-ключ (ввод осуществляется построчно)");
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        key1[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                int opred1 = Opred(key1);
                Console.WriteLine($"Определитель равен {opred1}");
                if (NOD(n, opred1) == false || opred1 == 0)
                {
                    Console.WriteLine("Неподходящий ключ");
                    goto Key1_in;
                }
            Key2_in:
                Console.WriteLine("Введите матрицу-ключ (ввод осуществляется построчно)");
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        key2[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                int opred2 = Opred(key2);
                Console.WriteLine($"Определитель равен {opred2}");
                if (NOD(n, opred2) == false || opred2 == 0)
                {
                    Console.WriteLine("Неподходящий ключ");
                    goto Key2_in;
                }
                else
                {
                    
                    Console.WriteLine("Введите текст, который необходимо зашифровать:");
                    string text = Console.ReadLine();
                    if (text.Length % 3 != 0)
                    {
                        int i = text.Length;
                        while (i % 3 != 0)
                        {
                            text = $"{text}_";
                            i++;
                        }
                        Console.WriteLine(text);
                    }
                    //Шифровка
                    int[,,] keys = new int[3, 3, text.Length];
                    for (int i = 0; i<3; i++)
                    {
                        for (int j=0;j<3;j++)
                        {
                            keys[i, j, 0] = key1[i, j];
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            keys[i, j, 1] = key2[i, j];
                        }
                    }
                    char[] text_encoded = new char[text.Length];
                    text_encoded = text.ToCharArray();
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text_encoded[i] == ' ') text_encoded[i] = '_';
                    }
                    Console.WriteLine(text_encoded);
                    int[] open_codes = new int[text.Length];
                    int[] close_codes = new int[text.Length];
                    Console.WriteLine("Коды:");
                    for (int i = 0; i < text.Length; i++)
                    {
                        open_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                        Console.Write($"{open_codes[i]}  ");
                    }
                    int a = 0;
                    Console.WriteLine("\nЗашифрованный текст:");
                    while (a<text.Length)
                    {
                        try
                        {
                            if (a==0)
                            {
                                close_codes[a] = (keys[0, 0, a] * open_codes[a] + keys[0, 1, a] * open_codes[a + 1] + keys[0, 2, a] * open_codes[a + 2]) % 27;
                                text_encoded[a] = eng_abc[close_codes[a]];
                                Console.Write(text_encoded[a]);
                                a++;
                            }
                            else if (a==1)
                            {
                                close_codes[a] = (keys[1, 0, a] * open_codes[a - 1] + keys[1, 1, a] * open_codes[a] + keys[1, 2, a] * open_codes[a + 1]) % 27;
                                text_encoded[a] = eng_abc[close_codes[a]];
                                Console.Write(text_encoded[a]);
                                a++;
                            }
                            else
                            {
                                int ost = a % 3;
                                keys[0, 0, a] = (keys[0, 0, a - 1] * keys[0, 0, a - 2] + keys[0, 1, a - 1] * keys[1, 0, a - 2] + keys[0, 2, a - 1] * keys[2, 0, a - 2]) % 27;
                                keys[0, 1, a] = (keys[0, 0, a - 1] * keys[0, 1, a - 2] + keys[0, 1, a - 1] * keys[1, 1, a - 2] + keys[0, 2, a - 1] * keys[2, 1, a - 2]) % 27;
                                keys[0, 2, a] = (keys[0, 0, a - 1] * keys[0, 2, a - 2] + keys[0, 1, a - 1] * keys[1, 2, a - 2] + keys[0, 2, a - 1] * keys[2, 2, a - 2]) % 27;
                                keys[1, 0, a] = (keys[1, 0, a - 1] * keys[0, 0, a - 2] + keys[1, 1, a - 1] * keys[1, 0, a - 2] + keys[1, 2, a - 1] * keys[2, 0, a - 2]) % 27;
                                keys[1, 1, a] = (keys[1, 0, a - 1] * keys[0, 1, a - 2] + keys[1, 1, a - 1] * keys[1, 1, a - 2] + keys[1, 2, a - 1] * keys[2, 1, a - 2]) % 27;
                                keys[1, 2, a] = (keys[1, 0, a - 1] * keys[0, 2, a - 2] + keys[1, 1, a - 1] * keys[1, 2, a - 2] + keys[1, 2, a - 1] * keys[2, 2, a - 2]) % 27;
                                keys[2, 0, a] = (keys[0, 0, a - 1] * keys[0, 0, a - 2] + keys[0, 1, a - 1] * keys[1, 0, a - 2] + keys[0, 2, a - 1] * keys[2, 0, a - 2]) % 27;
                                keys[2, 1, a] = (keys[0, 0, a - 1] * keys[0, 1, a - 2] + keys[0, 1, a - 1] * keys[1, 1, a - 2] + keys[0, 2, a - 1] * keys[2, 1, a - 2]) % 27;
                                keys[2, 2, a] = (keys[0, 0, a - 1] * keys[0, 2, a - 2] + keys[0, 1, a - 1] * keys[1, 2, a - 2] + keys[0, 2, a - 1] * keys[2, 2, a - 2]) % 27;
                                switch(ost)
                                {
                                    case 0:
                                        close_codes[a] = (keys[0, 0, a] * open_codes[a] + keys[0, 1, a] * open_codes[a + 1] + keys[0, 2, a] * open_codes[a + 2]) % 27;
                                        text_encoded[a] = eng_abc[close_codes[a]];
                                        Console.Write(text_encoded[a]);
                                        a++;
                                        break;
                                    case 1:
                                        close_codes[a] = (keys[1, 0, a] * open_codes[a - 1] + keys[1, 1, a] * open_codes[a] + keys[1, 2, a] * open_codes[a + 1]) % 27;
                                        text_encoded[a] = eng_abc[close_codes[a]];
                                        Console.Write(text_encoded[a]);
                                        a++;
                                        break;
                                    case 2:
                                        close_codes[a] = (keys[2, 0, a] * open_codes[a - 2] + keys[2, 1, a] * open_codes[a - 1] + keys[2, 2, a] * open_codes[a]) % 27;
                                        text_encoded[a] = eng_abc[close_codes[a]];
                                        Console.Write(text_encoded[a]);
                                        a++;
                                        break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    //Дешифровка
                    Console.WriteLine("\nОбратная матрица ключа:");
                    int[,,] keys_temp = new int[3, 3,text.Length];
                    int[,,] keys_obr = new int[3, 3, text.Length];
                    for (int i=0; i<text.Length;i++)
                    {
                        //m1
                        keys_temp[0, 0, i] = keys[1, 1, i] * keys[2, 2, i] - keys[1, 2, i] * keys[2, 1, i];
                        if (keys_temp[0, 0, i] > 27) keys_temp[0, 0, i] = keys_temp[0, 0, i] % 27;
                        else keys_temp[0, 0, i] = mod_otr(keys_temp[0, 0, i], 27);
                        //m4
                        keys_temp[0, 1, i] = keys[0, 1, i] * keys[2, 2, i] - keys[0, 2, i] * keys[2, 1, i];
                        if (keys_temp[0, 1, i] > 27) keys_temp[0, 1, i] = keys_temp[0, 1, i] % 27;
                        else keys_temp[0, 1, i] = mod_otr(keys_temp[0, 1, i], 27);
                        //m7
                        keys_temp[0, 2, i] = keys[0, 1, i] * keys[1, 2, i] - keys[0, 2, i] * keys[1, 1, i];
                        if (keys_temp[0, 2, i] > 27) keys_temp[0, 2, i] = keys_temp[0, 2, i] % 27;
                        else keys_temp[0, 2, i] = mod_otr(keys_temp[0, 2, i], 27);
                        //m2
                        keys_temp[1, 0, i] = keys[1, 0, i] * keys[2, 2, i] - keys[1, 2, i] * keys[2, 0, i];
                        if (keys_temp[1, 0, i] > 27) keys_temp[1, 0, i] = keys_temp[1, 0, i] % 27;
                        else keys_temp[1, 0, i] = mod_otr(keys_temp[1, 0, i], 27);
                        //m5
                        keys_temp[1, 1, i] = keys[0, 0, i] * keys[2, 2, i] - keys[0, 2, i] * keys[2, 0, i];
                        if (keys_temp[1, 1, i] > 27) keys_temp[1, 1, i] = keys_temp[1, 1, i] % 27;
                        else keys_temp[1, 1, i] = mod_otr(keys_temp[1, 1, i], 27);
                        //m8
                        keys_temp[1, 2, i] = keys[0, 0, i] * keys[1, 2, i] - keys[0, 2, i] * keys[1, 0, i];
                        if (keys_temp[1, 2, i] > 27) keys_temp[1, 2, i] = keys_temp[1, 2, i] % 27;
                        else keys_temp[1, 2, i] = mod_otr(keys_temp[1, 2, i], 27);
                        //m3
                        keys_temp[2, 0, i] = keys[1, 0, i] * keys[2, 1, i] - keys[1, 1, i] * keys[2, 0, i];
                        if (keys_temp[2, 0, i] > 27) keys_temp[2, 0, i] = keys_temp[2, 0, i] % 27;
                        else keys_temp[2, 0, i] = mod_otr(keys_temp[2, 0, i], 27);
                        //m6
                        keys_temp[2, 1, i] = keys[0, 0, i] * keys[2, 1, i] - keys[0, 1, i] * keys[2, 0, i];
                        if (keys_temp[2, 1, i] > 27) keys_temp[2, 1, i] = keys_temp[2, 1, i] % 27;
                        else keys_temp[2, 1, i] = mod_otr(keys_temp[2, 1, i], 27);
                        //m9
                        keys_temp[2, 2, i] = keys[0, 0, i] * keys[1, 1, i] - keys[0, 1, i] * keys[1, 0, i];
                        if (keys_temp[2, 2, i] > 27) keys_temp[2, 2, i] = keys_temp[2, 2, i] % 27;
                        else keys_temp[2, 2, i] = mod_otr(keys_temp[2, 2, i], 27);
                    }
                    int[] opred_obr = new int[text.Length];
                    opred_obr = Opred_Obr(Opred3d(keys, text.Length), 27, text.Length);
                    keys_obr = Obr_Keys(keys_temp, opred_obr);
                    for ( int i =0; i<text.Length; i++)
                    {
                        for (int j  = 0; j<3; j++)
                        {
                            for (int z = 0; z<3; z++)
                            {
                                Console.Write($"{keys_obr[j, z, i]}  ");
                            }
                            Console.Write("\n");
                        }
                        Console.Write("\n\n");
                    }
                    int b = 0;
                    Console.WriteLine("Коды:");
                    for (int i = 0; i < text.Length; i++)
                    {
                        close_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                        Console.Write($"{close_codes[i]} ");
                    }
                    Console.WriteLine("\nРасшифрованнй текст:");
                    while (b<text.Length)
                    {
                        try
                        {
                            if (b == 0)
                            {
                                open_codes[b] = (keys_obr[0, 0, b] * close_codes[b] + keys_obr[0, 1, b] * close_codes[b + 1] + keys_obr[0, 2, b] * close_codes[b + 2]) % 27;
                                text_encoded[b] = eng_abc[open_codes[b]];
                                Console.Write(text_encoded[b]);
                                b++;
                            }
                            else if (b == 1)
                            {
                                open_codes[b] = (keys_obr[1, 0, b] * close_codes[b - 1] + keys_obr[1, 1, b] * close_codes[b] + keys_obr[1, 2, b] * close_codes[b + 1]) % 27;
                                text_encoded[b] = eng_abc[open_codes[b]];
                                Console.Write(text_encoded[b]);
                                b++;
                            }
                            else
                            {
                                int ost = b % 3;
                                keys_obr[0, 0, b] = (keys_obr[0, 0, b - 2] * keys_obr[0, 0, b - 1] + keys_obr[0, 1, b - 2] * keys_obr[1, 0, b - 1] + keys_obr[0, 2, b - 2] * keys_obr[2, 0, b - 1]) % 27;
                                keys_obr[0, 1, b] = (keys_obr[0, 0, b - 2] * keys_obr[0, 1, b - 1] + keys_obr[0, 1, b - 2] * keys_obr[1, 1, b - 1] + keys_obr[0, 2, b - 2] * keys_obr[2, 1, b - 1]) % 27;
                                keys_obr[0, 2, b] = (keys_obr[0, 0, b - 2] * keys_obr[0, 2, b - 1] + keys_obr[0, 1, b - 2] * keys_obr[1, 2, b - 1] + keys_obr[0, 2, b - 2] * keys_obr[2, 2, b - 1]) % 27;
                                keys_obr[1, 0, b] = (keys_obr[1, 0, b - 2] * keys_obr[0, 0, b - 1] + keys_obr[1, 1, b - 2] * keys_obr[1, 0, b - 1] + keys_obr[1, 2, b - 2] * keys_obr[2, 0, b - 1]) % 27;
                                keys_obr[1, 1, b] = (keys_obr[1, 0, b - 2] * keys_obr[0, 1, b - 1] + keys_obr[1, 1, b - 2] * keys_obr[1, 1, b - 1] + keys_obr[1, 2, b - 2] * keys_obr[2, 1, b - 1]) % 27;
                                keys_obr[1, 2, b] = (keys_obr[1, 0, b - 2] * keys_obr[0, 2, b - 1] + keys_obr[1, 1, b - 2] * keys_obr[1, 2, b - 1] + keys_obr[1, 2, b - 2] * keys_obr[2, 2, b - 1]) % 27;
                                keys_obr[2, 0, b] = (keys_obr[0, 0, b - 2] * keys_obr[0, 0, b - 1] + keys_obr[0, 1, b - 2] * keys_obr[1, 0, b - 1] + keys_obr[0, 2, b - 2] * keys_obr[2, 0, b - 1]) % 27;
                                keys_obr[2, 1, b] = (keys_obr[0, 0, b - 2] * keys_obr[0, 1, b - 1] + keys_obr[0, 1, b - 2] * keys_obr[1, 1, b - 1] + keys_obr[0, 2, b - 2] * keys_obr[2, 1, b - 1]) % 27;
                                keys_obr[2, 2, b] = (keys_obr[0, 0, b - 2] * keys_obr[0, 2, b - 1] + keys_obr[0, 1, b - 2] * keys_obr[1, 2, b - 1] + keys_obr[0, 2, b - 2] * keys_obr[2, 2, b - 1]) % 27;
                                switch (ost)
                                {
                                    case 0:
                                        open_codes[b] = (keys_obr[0, 0, b] * close_codes[b] + keys_obr[0, 1, b] * close_codes[b + 1] + keys_obr[0, 2, b] * close_codes[b + 2]) % 27;
                                        text_encoded[b] = eng_abc[open_codes[b]];
                                        Console.Write(text_encoded[b]);
                                        b++;
                                        break;
                                    case 1:
                                        open_codes[b] = (keys_obr[1, 0, b] * close_codes[b - 1] + keys_obr[1, 1, b] * close_codes[b] + keys_obr[1, 2, b] * close_codes[b + 1]) % 27;
                                        text_encoded[b] = eng_abc[open_codes[b]];
                                        Console.Write(text_encoded[b]);
                                        b++;
                                        break;
                                    case 2:
                                        open_codes[b] = (keys_obr[2, 0, b] * close_codes[b - 2] + keys_obr[2, 1, b] * close_codes[b - 1] + keys_obr[2, 2, b] * close_codes[b]) % 27;
                                        text_encoded[b] = eng_abc[open_codes[b]];
                                        Console.Write(text_encoded[b]);
                                        b++;
                                        break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }


                }
            }
            static bool NOD(int n, int alpha)
            {
                while (n != 0)
                {
                    int temp = n;
                    n = alpha % n;
                    alpha = temp;
                }
                if (alpha == 1) return true;
                else return false;
            }
            static int Alpha_Obr(int alpha, int n)
            {
                int y1 = 1; int y2 = 0;
                while (alpha > 0)
                {
                    int q = n / alpha;
                    int r = n - q * alpha;
                    int y = y2 - q * y1;
                    n = alpha;
                    alpha = Convert.ToInt32(r);
                    y2 = y1;
                    y1 = y;
                }
                int d = n; int y_ = y2;
                if (d == 1) return y_;
                else return 0;
            }
            static int Opred(int[,] x)
            {
                int opred = (x[0, 0] * x[1, 1] * x[2, 2] + x[0, 1] * x[1, 2] * x[2, 0] + x[0, 2] * x[1, 0] * x[2, 1]) -
                    (x[0, 2] * x[1, 1] * x[2, 0] + x[0, 0] * x[1, 2] * x[2, 1] + x[2, 2] * x[0, 1] * x[1, 0]);
                if (opred > 27) return (opred % 27);
                else return (mod_otr(opred,27));
            }
            static int mod_otr(int x, int n)
            {
                while (x < 0)
                {
                    x = x + n;
                }
                return x;
            }
            static int[] Opred3d(int[,,] x, int l)
            {
                int[] opred = new int[l];
                for (int i = 0; i<l;i++)
                {
                    for (int j = 0; j<3;j++)
                    {
                        for (int z = 0; z<3;z++)
                        {
                            opred[i] = (x[0, 0, i] * x[1, 1, i] * x[2, 2, i] + x[0, 1, i] * x[1, 2, i] * x[2, 0, i] + x[0, 2, i] * x[1, 0, i] * x[2, 1, i]) -
                                (x[0, 2, i] * x[1, 1, i] * x[2, 0, i] + x[0, 0, i] * x[1, 2, i] * x[2, 1, i] + x[2, 2, i] * x[0, 1, i] * x[1, 0, i]);
                            if (opred[i] > 27) opred[i] = (opred[i] % 27);
                            else if(opred[i]<0) opred[i] = (mod_otr(opred[i], 27));
                        }
                    }
                }
                return opred;
            }
            static int[,,] Obr_Keys(int [,,]keys_temp, int[] opred_obr)
            {
                int[,,] keys_obr = new int[3, 3, opred_obr.Length];
                for (int i = 0; i<opred_obr.Length; i++)
                {
                    for (int j = 0; j<3; j++)
                    {
                        for (int z = 0; z<3; z++)
                        {
                            keys_obr[j, z, i] = opred_obr[i] * keys_temp[j, z, i];
                            if (keys_obr[j, z, i] > 27) keys_obr[j, z, i] = keys_obr[j, z, i] % 27;
                            else keys_obr[j, z, i] = mod_otr(keys_obr[j, z, i], 27);
                        }
                    }
                }
                return keys_obr;
            }
            static int[] Opred_Obr(int[] opred, int n, int l)
            {
                int[] opred_obr = new int[l];
                for (int i =0; i<l; i++)
                {
                    opred_obr[i] = Alpha_Obr(opred[i], 27);
                    if (opred_obr[i] > 27) opred_obr[i] = opred_obr[i] % 27;
                    else if (opred_obr[i] < 0) opred_obr[i] = mod_otr(opred_obr[i], 27);
                }
                return opred_obr;
            }
        }
    }
}
