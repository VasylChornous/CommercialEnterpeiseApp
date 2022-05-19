using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.UI
{
    public static class ConsoleSettingsChanger
    {
        public static void DisplayRedMessage(String message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }

        public static void DisplayGreenMessage(String message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }
    }
}
