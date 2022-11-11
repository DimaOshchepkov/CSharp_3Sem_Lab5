using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    enum task
    {
        maxNumber = 1,
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введити задание\n" +
                              "1) Максимальное число\n" +
                              "");

            int numTask = -1;
            while(!int.TryParse(Console.ReadLine(), out numTask) ||
                numTask <=0 || numTask > 8  || numTask == 7 || numTask == 6)
            {
                Console.WriteLine("Неверный ввод задания");
            };
            switch(numTask)
            {
                case (int)task.maxNumber:
                    {
                        Console.WriteLine("Введите количество цифр числа");
                        int count = -1;
                        while (!int.TryParse(Console.ReadLine(), out numTask) || numTask < 0 )
                            Console.WriteLine("Неверный ввод количества цифр");

                        int[] number = new int[count];
                        int[] digits = new int[10];
                        for (int i = 0; i < 10; i++)
                            number[i] = 0;

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
                        Console.WriteLine(number);
                        break;
                    }
            }
            Console.ReadKey();
        }
    }
}
