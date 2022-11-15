using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        static void Main(string[] args)
        {
            Console.WriteLine("Введити задание\n" +
                              "1) Максимальное число\n" +
                              "2) Дружественные числа\n" +
                              "3) Автоморфные числа");

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

                    
            }
            Console.ReadKey();
        }
    }
}


