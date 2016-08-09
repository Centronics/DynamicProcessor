using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
[assembly: CLSCompliant(true)]

namespace DynamicProcessor
{
    /// <summary>
    /// Представляет карту и позволяет производить операции, согласно стандарту её обработки.
    /// </summary>
    public sealed class Map : ICloneable
    {
        /// <summary>
        /// Максимальное количество объектов по оси X.
        /// </summary>
        public const int MaxX = 8;
        /// <summary>
        /// Максимальное количество объектов по оси Y.
        /// </summary>
        public const int MaxY = 5;
        /// <summary>
        /// Максимальное количество объектов на карте.
        /// </summary>
        public const int AllMax = MaxX * MaxY;

        /// <summary>
        /// Содержит объекты карты.
        /// </summary>
        readonly List<MapObject> _mapMas;

        /// <summary>
        /// Инициализирует пустую карту.
        /// </summary>
        public Map()
        {
            _mapMas = new List<MapObject>();
        }

        /// <summary>
        /// Загружает карту из указанного потока, в формате XML.
        /// </summary>
        /// <param name="st">Поток, из которого необходимо осуществить загрузку.</param>
        public Map(Stream st)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<MapObject>));
            _mapMas = (List<MapObject>)formatter.Deserialize(st);
            if (_mapMas.Count > AllMax)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Загружаемый XML некорректен: Количество объектов ({0}) превышает допустимое ({1}).",
                    _mapMas.Count, AllMax));
        }

        /// <summary>
        /// Нумерует все объекты карты для просмотра в редакторе карт.
        /// </summary>
        public void ObjectNumeration()
        {
            if (_mapMas.Count <= 0)
                return;
            for (int y = 0, k = 0; y < MaxY; y++)
                for (int x = 0; x < MaxX && k < _mapMas.Count; x++, k++)
                {
                    _mapMas[k].ObjectX = x;
                    _mapMas[k].ObjectY = y;
                }
        }

        /// <summary>
        /// Производит поиск наиболее близкого объекта по полю "знак", игнорируя при этом пройденные объекты.
        /// </summary>
        /// <param name="sign">Искомый знак объекта.</param>
        /// <returns>Возвращает найденный объект или null, если объект не найден.</returns>
        public MapObject FindBySign(SignValue sign)
        {
            SignValue diff = SignValue.MaxValue;
            return _mapMas.LastOrDefault(it =>
            {
                if (it == null)
                    return false;
                if (it.DiscountNumber >= 0)
                    return false;
                SignValue d = it.Sign - sign;
                if (d <= diff)
                {
                    diff = d;
                    return true;
                }
                return false;
            });
        }

        /// <summary>
        /// Добавляет объект на карту (в конец коллекции). Объект не может иметь значение null. Количество объектов не может быть больше AllMax.
        /// </summary>
        /// <param name="obj">Объект для добавления.</param>
        public void Add(MapObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj", "Map.Add: объект не задан");
            if (_mapMas.Count >= AllMax)
                throw new OverflowException(string.Format(CultureInfo.CurrentCulture, "Невозможно добавить новый объект на карту: на карте не может быть более {0} объектов", AllMax));
            _mapMas.Add(obj);
        }

        /// <summary>
        /// Очищает список пройденных объектов.
        /// </summary>
        public void ClearDiscount()
        {
            _mapMas.ForEach(it => it.DiscountNumber = -1);
        }

        /// <summary>
        /// Создаёт полную копию текущей карты.
        /// </summary>
        /// <returns>Возвращает полную копию текущей карты.</returns>
        public object Clone()
        {
            Map nmap = new Map();
            foreach (MapObject obj in _mapMas)
                nmap._mapMas.Add(new MapObject { DiscountNumber = obj.DiscountNumber, ObjectX = obj.ObjectX, ObjectY = obj.ObjectY, Sign = obj.Sign });
            return nmap;
        }

        /// <summary>
        /// Получает объект карты по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс объекта карты.</param>
        /// <returns>Возвращает объект карты по указанному индексу.</returns>
        public MapObject this[int index]
        {
            get
            {
                return _mapMas[index];
            }
        }

        /// <summary>
        /// Получает количество объектов на карте.
        /// </summary>
        public int Count
        {
            get
            {
                return _mapMas.Count;
            }
        }

        /// <summary>
        /// Получает количество пройденных объектов на карте.
        /// </summary>
        public int CountDiscounted
        {
            get
            {
                int disc = 0;
                _mapMas.ForEach(obj =>
                    {
                        if (obj.DiscountNumber >= 0)
                            disc++;
                    });
                return disc;
            }
        }

        /// <summary>
        /// Выполняет поиск объекта с указанными координатами.
        /// </summary>
        /// <param name="objX">Координата X, не может быть меньше нуля.</param>
        /// <param name="objY">Координата Y, не может быть меньше нуля.</param>
        /// <returns>Возвращает найденный объект или null, если ничего не найдено.</returns>
        public MapObject GetObjectByXY(int objX, int objY)
        {
            if (objX < 0 || objY < 0)
                return null;
            return _mapMas.Find(obj => (obj.ObjectX == objX && obj.ObjectY == objY));
        }

        /// <summary>
        /// Удаляет объект с указанным знаком.
        /// </summary>
        /// <param name="sv">Знак для удаления.</param>
        public void RemoveObject(SignValue sv)
        {
            _mapMas.Remove(new MapObject { Sign = sv });
        }

        /// <summary>
        /// Удаляет все объекты с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public void RemoveObject(int x, int y)
        {
            _mapMas.RemoveAll(it => (it.ObjectX == x && it.ObjectY == y));
        }

        /// <summary>
        /// Удаляет объект с указанным индексом.
        /// </summary>
        /// <param name="index">Отсчитываемый от нуля индекс объекта, который требуется удалить.</param>
        public void RemoveObject(int index)
        {
            _mapMas.RemoveAt(index);
        }

        /// <summary>
        /// Удаляет все объекты с карты.
        /// </summary>
        public void Clear()
        {
            _mapMas.Clear();
        }

        /// <summary>
        /// Записывает карту в указанный поток, в формате XML.
        /// </summary>
        /// <param name="st">Поток, в который необходимо сохранить текущую карту.</param>
        public void ToStream(Stream st)
        {
            (new XmlSerializer(typeof(List<MapObject>))).Serialize(st, _mapMas);
        }
    }
}