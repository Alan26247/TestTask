
namespace TestTask
{
    // Много комментариев которые по сути не нужны, но я их оставил для того чтобы
    // показать ход своих мыслей.
    // В одну функцию(метод) вложить столько логиги считаю неправильным, поэтому
    // решил для реализации данного задания использовать класс.

    public static class MoneyDivisionCalculator
    {
        // главный метод используемый для вычисления 
        public static string Calculate(double money, string listSum, TypeCalculation typeCalculation)
        {
            // округляем деньги до сотых
            money = Math.Round(money, 2);

            if (typeCalculation == TypeCalculation.ПЕРВ) return CalculationTypeFirst(money, listSum);
            else if (typeCalculation == TypeCalculation.ПОСЛ) return CalculationTypeLast(money, listSum);
            else return CalculationTypeProportional(money, listSum);
        }



        // метод вычисляет распределение денег по типу ПРОП
        static string CalculationTypeProportional(double money, string listSumString)
        {
            // получаем список сумм денег
            List<double> listSum = SelectAmountsMoneyFromString(listSumString);

            // вычисляем сумму всех сумм
            double maxSum = 0;
            foreach(double sum in listSum) maxSum += sum;

            // формируем список распределения денег
            List<double> listMoney = new();
            foreach (double sum in listSum)
            {
                // рассчитываем долю приходящую на данную сумму
                double fraction = sum / maxSum;

                // вычисляем количество денег приходящихся на данную сумму
                double moneyFraction = Math.Round(money * fraction, 2);

                // добавляем результат в список вычислений
                listMoney.Add(moneyFraction);
            }

            // остаток от округления добавляем к последней сумме согласно заданию
            // для этого сначало узнаем какую сумму мы уже распределили
            double allFractionMoney = 0;
            foreach (double value in listMoney) allFractionMoney += value;
            // теперь узнаем остаток
            double remain = Math.Round(money - allFractionMoney, 2);
            // добавляем остаток к последней сумме
            if (listMoney.Count > 0) listMoney[listMoney.Count - 1] += remain; 

            // формируем строку с результатом и возвращаем
            return ConvertMoneyListToString(listMoney);
        }

        // метод вычисляет распределение денег по типу ПЕРВ
        static string CalculationTypeFirst(double money, string listSumString)
        {
            // получаем список сумм
            List<double> listSum = SelectAmountsMoneyFromString(listSumString);

            // формируем список распределения денег
            List<double> listMoney = new();
            foreach(double sum in listSum)
            {
                // если денег хватает то начисляем
                if(money >= sum)
                {
                    // добавляем результат в список вычислений
                    listMoney.Add(sum);

                    // вычисляем количество денег приходящихся на данную сумму
                    money = Math.Round(money - sum, 2);
                }
                else
                {
                    // если денег не хватает начисляем сколько есть
                    listMoney.Add(money);

                    // обнуляем деньги
                    money = 0;
                }
            }

            // формируем строку с результатом и возвращаем
            return ConvertMoneyListToString(listMoney);
        }

        // метод вычисляет распределение денег по типу ПОСЛ
        static string CalculationTypeLast(double money, string listSumString)
        {
            // получаем список сумм
            List<double> listSum = SelectAmountsMoneyFromString(listSumString);

            // формируем список распределения денег
            List<double> listMoney = new();
            for (int i = listSum.Count - 1; i >= 0; i--)
            {
                // если денег хватает то начисляем
                if (money >= listSum[i])
                {
                    // добавляем результат в список вычислений
                    listMoney.Add(listSum[i]);

                    // вычисляем количество денег приходящихся на данную сумму
                    money = Math.Round(money - listSum[i], 2);
                }
                else
                {
                    // если денег не хватает начисляем сколько есть
                    listMoney.Add(money);

                    // обнуляем деньги
                    money = 0;
                }
            }

            // переворачиваем список для корректного вывода
            listMoney.Reverse();

            // формируем строку с результатом и возвращаем
            return ConvertMoneyListToString(listMoney);
        }

        // метод получает список сумм из строки
        static List<double> SelectAmountsMoneyFromString(string value)
        {
            List<double> result = new();

            // заменяем точки на запятые
            value = Regex.Replace(value, @"[\.]", ",").ToString();

            // удаляем все ненужные символы
            value = Regex.Replace(value, @"[^0-9,;]+", "");

            // разделяем входную строку на подстроки содержащие суммы
            string[] listMoneyStrings = value.Split(";");

            // заполняем возвращаемый список суммами
            foreach(string moneyString in listMoneyStrings)
            {
                // преобразуем строку в число
                if(double.TryParse(moneyString, out double money))
                {
                    // если удачно добавляем в список
                    result.Add(Math.Round(money, 2));
                }
                else
                {
                    // если с ошибкой то возвращаем так как есть
                    return result;
                }
            }

            // возвращаем заполненый список
            return result;
        }

        // метод преобразующий список денег в строку
        static string ConvertMoneyListToString(List<double> moneyList)
        {
            StringBuilder stringBuilder = new();

            // формируем строку
            foreach(double mony in moneyList)
            {
                stringBuilder.Append(mony.ToString() + ";");
            }

            // заменяем запятые на точки
            string returnString = Regex.Replace(stringBuilder.ToString(), @"[,]", ".").ToString();

            return returnString;
        }



        // содержит типы доступных вычислений
        public enum TypeCalculation
        {
            ПРОП,
            ПЕРВ,
            ПОСЛ
        }
    }
}
