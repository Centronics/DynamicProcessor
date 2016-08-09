using System;

namespace DynamicProcessor
{
    /// <summary>
    /// Обрабатывает объекты карты.
    /// </summary>
    public sealed class Processor
    {
        /// <summary>
        /// Карта объектов текущего экземпляра.
        /// </summary>
        readonly Map _currentMassive;
        /// <summary>
        /// Вызывается при остановке на отлаживаемом объекте.
        /// Получает порождаемый знак, стартовый знак, найденный объект и количество пройденных объектов.
        /// Возвращает значение true - если необходимо продолжить обработку после отладочного прерывания, false - если нет.
        /// </summary>
        public Func<SignValue, SignValue, MapObject, int, bool> ProcDebugObject { get; set; }

        /// <summary>
        /// Инициализирует экземпляр с указанием ссылки на карту обрабатываемых объектов.
        /// </summary>
        /// <param name="map">Карта обрабатываемых объектов.</param>
        public Processor(Map map)
        {
            if (map == null)
                throw new ArgumentNullException("map", "Processor: Карта для обработки не может быть пустой (null)");
            _currentMassive = map;
        }

        /// <summary>
        /// Обрабатывает карту с учётом заданного знака. Обработка производится по принципу нахождения объекта с ближайшим знаком относительно заданного,
        /// нахождения среднего арифметического между обоими знаками. При этом, найденный объект считается пройденным и в
        /// рамках текущей сессии возврат к нему не производится. Средний знак сохраняется как знак пройденного объекта.
        /// Таким образом, состояние карты полностью изменяется.
        /// Когда все объекты карты пройдены, возвращается результат, представляющий собой среднее арифметическое от последнего найденного объекта.
        /// Возвращает null, если выполнение было прервано.
        /// </summary>
        /// <param name="start">Стартовый знак.</param>
        /// <returns>Возвращает результат обработки карты или null, если выполнение было прервано.</returns>
        public SignValue? Run(SignValue start)
        {
            if (_currentMassive.Count <= 0)
                return null;
            _currentMassive.ClearDiscount();
            int discountNumber = 0;
            while (true)
            {
                MapObject curObject = _currentMassive.FindBySign(start);
                if (curObject == null)
                    return start;
                SignValue newSign = curObject.Sign.Average(start);
                if (ProcDebugObject != null)
                    if (!ProcDebugObject(newSign, start, curObject, _currentMassive.CountDiscounted))
                        return null;
                curObject.DiscountNumber = discountNumber++;
                curObject.Sign = start = newSign;
            }
        }
    }
}