using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    enum task
    {
        maxNumber = 1,
        friendNumbers,
        automorphic,
        arifmeticProgression,
        twinsNumber,
        alphabet,
        exit = 8,
    }
    internal class Program
    {
        private static void PrintValue(int[] number)
        {
            foreach (int digit in number)
            {
                Console.Write(digit);
            }
            Console.WriteLine();
        }

        private static int SumDivider(int num)
        {
            int sum = 1 + num;
            for (int i = 2; i < Math.Ceiling((Math.Sqrt(num))); i++)
            {
                if (num % i == 0)
                    sum = sum + i + num / i;
            }
            if ((int)Math.Sqrt(num) == Math.Sqrt(num))
                sum += (int)Math.Sqrt(num);

            return sum;
        }

        private static (int prev, int next) Twins(int A, int B)
        {
            int[] a = new int[B + 1];
            int countSimple = 0;
            int[] ans = new int[B/2];
            for (int i = 0; i < B + 1; i++)
                a[i] = i;

            for (int j = 4; j < B + 1; j += 2)
                a[j] = 0;
            ans[countSimple] = 0;
            countSimple++;

            for (int p = 3; p < B + 1; p++)
            {
                if (a[p] != 0)
                {

                    ans[countSimple] = a[p];
                    if (ans[countSimple] - ans[countSimple - 1] == 2 && ans[countSimple - 1] >= A)
                        return (prev: p - 2, next: p);

                    countSimple++;
                    for (int j = p * p; j < B + 1; j += p)
                        a[j] = 0;


                }
                

            }

            return (prev: -1, next: -1);
        }

        private static int[] GetNumber(String num)
        {
            int[] number = new int[num.Length];
            for (int i = 0; i < num.Length; i++)
            {
                if (!int.TryParse(num[i].ToString(), out number[i]))
                    return new int[0];
            }
            return number;
        }

        private static void PrintArifmProgr(int a)
        {
            int[] progressions = new int[(int)Math.Ceiling(a / 2.0) + 1];
            progressions[0] = 0;
            for (int i = 1; i < (int)Math.Ceiling(a / 2.0) + 1; i++)
            {
                progressions[i] = progressions[i - 1] + i;
                for (int j = 0; j < i; j++)
                {
                    if (progressions[i] - progressions[j] == a)
                    {
                        for (int num = j + 1; num <= i; num++)
                            Console.Write(num + " ");
                        Console.WriteLine();
                        return;
                    }
                }
            }
            Console.WriteLine("Нельзя представить в виде послдедовательности натуральных чисел");
        }

        public static void PrintAlphabet(int n, int cur = 0, in char alf = 'A', in int sizeAlfabet = 26)
        {
            for (int i = 0; i < cur; i++)
                Console.Write(" ");

            for (int i = cur; i < (sizeAlfabet - cur); i++)
                Console.Write((char)(alf + i));

            Console.WriteLine();

            if (sizeAlfabet - cur*2 > n)
                PrintAlphabet(n, cur + 1, alf);

            if (sizeAlfabet - cur * 2 != n)
            {
                for (int i = 0; i < cur; i++)
                    Console.Write(" ");

                for (int i = cur; i < sizeAlfabet - cur; i++)
                    Console.Write((char)(alf + i));

                Console.WriteLine();
            }
        }


        static void Main(string[] args)
        {
            while (true)
            {

                Console.WriteLine("Введите задание\n" +
                                  "1) Максимальное число\n" +
                                  "2) Дружественные числа\n" +
                                  "3) Автоморфные числа\n" +
                                  "4) Арифметическая прогрессия\n" +
                                  "5) Числа близнецы\n" +
                                  "6) Алфавит\n" +
                                  "8) Закрыть");

                int numTask = -1;
                while (!int.TryParse(Console.ReadLine(), out numTask) ||
                    numTask <= 0 || numTask > 8 || numTask == 7)
                {
                    Console.WriteLine("Неверный ввод задания");
                }
                switch (numTask)
                {
                    case (int)task.maxNumber:
                        {
                            Console.WriteLine("Введите число");

                            int[] number = new int[0];
                            while (number.Length == 0)
                            {

                                String num = Console.ReadLine();
                                number = GetNumber(num);
                            }

                            int[] digits = new int[10];
                            for (int i = 0; i < 10; i++)
                                digits[i] = 0;

                            foreach (int digit in number)
                                digits[digit]++;

                            int sup = 0;
                            for (int digit = digits.Length - 1; digit >= 0; digit--)
                            {
                                for (int j = 0; j < digits[digit]; j++)
                                    number[j + sup] = digit;
                                sup += digits[digit];
                            }
                            Console.WriteLine("Максимальное число из цифр: ");
                            PrintValue(number);
                            break;
                        }

                    case (int)task.friendNumbers:
                        {
                            Console.WriteLine("Введите верхнюю границу");
                            int upperBound = -1;
                            while (!int.TryParse(Console.ReadLine(), out upperBound) || upperBound <= 0)
                                Console.WriteLine("Неверный ввод числа");

                            for (int i = 2; i < upperBound; i++)
                            {
                                int purposeFriend = SumDivider(i) - i;
                                if (SumDivider(purposeFriend) - purposeFriend == i &&
                                    i < purposeFriend && purposeFriend <= upperBound)
                                    Console.WriteLine(i + " / " + purposeFriend);
                            }

                            break;
                        }
                    case (int)task.automorphic:
                        {
                            Console.WriteLine("Введите верхнюю границу");
                            int upperBound = -1;
                            while (!int.TryParse(Console.ReadLine(), out upperBound) || upperBound <= 0)
                                Console.WriteLine("Неверный ввод числа");

                            Console.WriteLine("Автоморфные числа:");
                            int num = 1;
                            int[] sup = new int[3] { 4, 1, 5 };
                            int s = 0;
                            while (num <= upperBound)
                            {
                                int digitCount = (int)Math.Log10(num) + 1;
                                if (Math.Pow(num, 2) % (int)Math.Pow(10, digitCount) == num)
                                    Console.WriteLine(num);
                                num += sup[s];
                                s = (s + 1) % 3;
                            }
                            break;
                        }
                    case (int)task.arifmeticProgression:
                        {
                            Console.WriteLine("Введите A");
                            int a = -1;
                            while (!int.TryParse(Console.ReadLine(), out a) || a <= 0)
                                Console.WriteLine("Неверный ввод числа");

                            PrintArifmProgr(a);

                            break;
                        }

                    case (int)task.twinsNumber://////////////////////////////////
                        {
                            Console.WriteLine("Введите число");
                            int a = -1;
                            while (!int.TryParse(Console.ReadLine(), out a) || a <= 0)
                                Console.WriteLine("Неверный ввод числа");

                            Console.WriteLine("Числа близнецы:");

                            var pair = Twins(a, 2 * a);

                            if (pair.prev != -1)
                                Console.WriteLine(pair.next + "/" + pair.prev);
                            else
                                Console.WriteLine("Нет близнецов");

                            break;
                        }
                    case (int)task.alphabet:
                        {
                            ///////////////////////
                            const int sizeAlfabet = 26;
                            Console.WriteLine("Введите число");
                            int a = -1;
                            while (!int.TryParse(Console.ReadLine(), out a) || a <= 0 || a > sizeAlfabet)
                                Console.WriteLine("Неверный ввод числа");

                            PrintAlphabet(a);

                            break;
                        }
                    case (int)task.exit:
                        {
                            return;
                        }


                }

                Console.WriteLine("Продолжить?(да/нет)");
                String ans = Console.ReadLine();
                while (ans != "да" && ans != "нет")
                    Console.WriteLine("Не понял команду");

                if (ans == "нет")
                    return;

                Console.Clear();
            }
        }
    }
}


