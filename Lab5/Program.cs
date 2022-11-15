using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

        private static int[] Eratosphen(int n)
        {
            int[] a = new int[n + 1];
            int countSimple = 0;
            int[] ans = new int[n/2];
            for (int i = 0; i < n + 1; i++)
                a[i] = i;
            for (int p = 2; p < n + 1; p++)
            {
                if (a[p] != 0)
                {
                    ans[countSimple] = a[p];
                    countSimple++;
                    for (int j = p * p; j < n + 1; j += p)
                        a[j] = 0;
                }
            }

            Array.Resize(ref ans, countSimple);
            return ans;
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


        static void Main(string[] args)
        {
            Console.WriteLine("Введити задание\n" +
                              "1) Максимальное число\n" +
                              "2) Дружественные числа\n" +
                              "3) Автоморфные числа\n" + 
                              "4) Арифметическая прогрессия\n" +
                              "5) Числа близнецы");

            int numTask = -1;
            while(!int.TryParse(Console.ReadLine(), out numTask) ||
                numTask <=0 || numTask > 8  || numTask == 7 || numTask == 6)
            { 
                Console.WriteLine("Неверный ввод задания");
            }
            switch(numTask)
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
                        for(int digit = digits.Length - 1; digit >= 0; digit--)
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
                        while (!int.TryParse(Console.ReadLine(), out upperBound))
                            Console.WriteLine("Неверный ввод числа");

                        for(int i = 2; i < upperBound; i++)
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
                        while (!int.TryParse(Console.ReadLine(), out upperBound))
                            Console.WriteLine("Неверный ввод числа");

                        Console.WriteLine("Автоморфные числа:");
                        int num = 1;
                        int[] sup = new int[3] { 4, 1, 5 };
                        int s = 0;
                        while(num <= upperBound)
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
                        while (!int.TryParse(Console.ReadLine(), out a))
                            Console.WriteLine("Неверный ввод числа");

                        PrintArifmProgr(a);

                        break;
                    }

                case (int)task.twinsNumber:
                    {
                        Console.WriteLine("Введите число");
                        int a = -1;
                        while (!int.TryParse(Console.ReadLine(), out a))
                            Console.WriteLine("Неверный ввод числа");

                        Console.WriteLine("Числа близнецы:");

                        int[] simplesNum = Eratosphen(2*a);
                        int indA = Array.FindIndex(simplesNum, (x => x >= a));
                        for (int i = indA + 1; i <= 2 * a; i++)
                            if (simplesNum[i] - simplesNum[i - 1] == 2)
                                Console.WriteLine(simplesNum[i] + " " + simplesNum[i - 1]);

                        break;
                    }

                    
            }
            Console.ReadKey();
        }
    }
}


