using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2all5
{
    // Класс, который хранит данные о температуре
    class TemperatureChangedEventArgs : EventArgs
    {
        public double NewTemperature { get; }

        public TemperatureChangedEventArgs(double newTemperature)
        {
            NewTemperature = newTemperature;
        }
    }

    // Класс датчика температуры, который генерирует событие TemperatureChanged
    class TemperatureSensor
    {
        private double currentTemperature;

        // Событие, которое вызывается при изменении температуры
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        // Метод для симуляции изменения температуры
        public void SetTemperature(double newTemperature)
        {
            if (newTemperature != currentTemperature)
            {
                currentTemperature = newTemperature;
                OnTemperatureChanged(new TemperatureChangedEventArgs(newTemperature));
            }
        }

        // Метод для вызова события TemperatureChanged
        protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
        {
            TemperatureChanged?.Invoke(this, e);
        }
    }

    // Класс термостата, который реагирует на изменение температуры
    class Thermostat
    {
        private const double MinTemperature = 5.0; // Минимальная комфортная температура
        private const double MaxTemperature = 18.0; // Максимальная комфортная температура

        public void Subscribe(TemperatureSensor sensor)
        {
            // Подписываемся на событие TemperatureChanged
            sensor.TemperatureChanged += OnTemperatureChanged;
        }

        // Обработчик события
        private void OnTemperatureChanged(object sender, TemperatureChangedEventArgs e)
        {
            if (e.NewTemperature < MinTemperature)
            {
                Console.WriteLine($"Температура {e.NewTemperature}°C. Включаем отопление.");
            }
            else if (e.NewTemperature > MaxTemperature)
            {
                Console.WriteLine($"Температура {e.NewTemperature}°C. Выключаем отопление.");
            }
            else
            {
                Console.WriteLine($"Температура {e.NewTemperature}°C. Температура в пределах нормы.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("---------События----------");
            // Создаем объекты TemperatureSensor и Thermostat
            TemperatureSensor sensor = new TemperatureSensor();
            Thermostat thermostat = new Thermostat();

            // Подписываем термостат на событие изменения температуры
            thermostat.Subscribe(sensor);

            // Симуляция изменения температуры
            sensor.SetTemperature(-2.0);  // Должно включить отопление
            sensor.SetTemperature(15.0);  // Температура в пределах нормы
            sensor.SetTemperature(20.0);  // Должно выключить отопление

            Console.ReadLine();

        }
    }
}
