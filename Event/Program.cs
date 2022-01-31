using System;

namespace Event
{
    class Program
    {
        static void Main(string[] args)
        {
            // список фамилий
            string[] surnames = new string[5] { "Сёмин", "Петрова", "Сарычева", "Бойко", "Титов" };

            NumberReader numberReader = new NumberReader();
            numberReader.NumberEnteredEvent += SortAndShow;
            try
            {
                numberReader.Read(surnames);
            }
            catch(FormatException)
            {
                Console.WriteLine("Введено некорректное значение.");
            }

            Console.WriteLine();
            Console.WriteLine("Для выхода из программы нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Отсортировать и вывести на экран список фамилий
        /// </summary>
        /// <param name="list">Список фамилий</param>
        /// <param name="typeSorting">Тип сортировки: 1 - сортировка А-Я; 2 - сортировка Я-А</param>
        static void SortAndShow(string[] list, byte typeSorting)
        {
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < list.Length - 1; j++)
                {
                    if (needToReOrder(list[j], list[j + 1], typeSorting))
                    {
                        string s = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = s;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Отсортированный список:");
            for (int i = 0; i < list.Length; i++) Console.WriteLine(list[i]);
        }

        /// <summary>
        /// Сделать перестановку элементов списка
        /// </summary>
        /// <param name="s1">Элемент списка для сравнения</param> 
        /// <param name="s2">Элемент списка для сравнения</param>
        /// <param name="typeSorting">Тип сортировки: 1 - сортировка А-Я; 2 - сортировка Я-А</param>
        /// <returns></returns>
        static bool needToReOrder(string s1, string s2, byte typeSorting)
        {
            for (int i = 0; i < (s1.Length > s2.Length ? s2.Length : s1.Length); i++)
            {
                if (s1.ToCharArray()[i] < s2.ToCharArray()[i]) return (typeSorting == 1 ? false : true);
                if (s1.ToCharArray()[i] > s2.ToCharArray()[i]) return (typeSorting == 1 ? true : false);
            }
            return false;
        }
    }

    /// <summary>
    /// Класс, вызывающий событие - издатель
    /// </summary>
    class NumberReader
    {
        public delegate void NumberEnteredDelegate(string[] list, byte number);
        public event NumberEnteredDelegate NumberEnteredEvent; // событие

        // Читаем введенное число
        public void Read(string[] list)
        {
            Console.WriteLine();
            Console.WriteLine("Укажите тип сортировки: либо 1, либо 2 \n(1 - сортировка А-Я; 2 - сортировка Я-А)");

            byte number = Convert.ToByte(Console.ReadLine()); 

            if (number != 1 && number != 2)
            {
                throw new FormatException();
            }

            NumberEntered(list, number);

        }

        protected virtual void NumberEntered(string[] list, byte number)
        {
            NumberEnteredEvent?.Invoke(list, number); // вызвать событие 
        }
    }
}
