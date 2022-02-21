using System;
using System.Text.RegularExpressions;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string moneyString;      // используется для ввода суммы денег для распределения
            double money;            // переменная хранит введеную сумму денег
            string sumsString;             // хранит введенные суммы

            Console.WriteLine("    Реализация тестового задания от AKELON\n");
            Console.WriteLine("Задание 4. Разработка функции распределения суммы");
            Console.WriteLine("             2022г Романов С.Г.");



            //------ основной цикл программы

            bool eventExit = false;   // если истина происходит выход из программы
            do
            {
                // просим ввести сумму денег для распределения
                Console.WriteLine("\nВведите сумму денег для распределения  (Например: 1136.54 ) " +
                                                                "или 'EXIT' для завершения программы:");
                moneyString = Console.ReadLine();

                // проверяем не запрошен ли выход из программы
                if (moneyString.ToLower() == "exit")
                {
                    // выходим из программы
                    eventExit = true;
                    continue;
                }

                // заменяем точки на запятые
                moneyString = Regex.Replace(moneyString, @"[\.]", ",").ToString();

                // если сумма введена не корректно то просим ввести заново
                while (!double.TryParse(moneyString, out money))
                {
                    Console.WriteLine("Сумма введена не корректно:\n");
                    Console.WriteLine("\nВведите сумму денег для распределения  (Например: 1136.54 ) " +
                                                                "или 'EXIT' для завершения программы:");
                    moneyString = Console.ReadLine();
                }

                // просим ввести суммы
                Console.WriteLine("\nВведите список сумм (Например: 456.28;1000;136.54 ) :");
                sumsString = Console.ReadLine();

                // просим ввести тип распределения
                string typeCalculate;
                do
                {
                    Console.WriteLine("\nВведите тип распределения 'ПРОП' 'ПЕРВ' 'ПОСЛ':");
                    typeCalculate = Console.ReadLine().ToUpper();
                }
                while (typeCalculate != "ПРОП" && typeCalculate != "ПЕРВ" && typeCalculate != "ПОСЛ");

                // производим вычисления
                string result;
                if (typeCalculate == "ПЕРВ") result = MoneyDivisionCalculator.Calculate(money, sumsString,
                    MoneyDivisionCalculator.TypeCalculation.ПЕРВ);
                else if (typeCalculate == "ПОСЛ") result = MoneyDivisionCalculator.Calculate(money, sumsString,
                    MoneyDivisionCalculator.TypeCalculation.ПОСЛ);
                else result = MoneyDivisionCalculator.Calculate(money, sumsString,
                    MoneyDivisionCalculator.TypeCalculation.ПРОП);

                // выводим результат
                Console.WriteLine("\nРезультат вычислений:");
                Console.WriteLine(result);
            }
            while (!eventExit) ;
        }
    }
}
