using System;

namespace DynamicProcessor
{
    /// <summary>
    /// Представляет объект карты.
    /// </summary>
    [Serializable]
    public class MapObject : IComparable
    {
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MapObject() { ObjectY = ObjectX = DiscountNumber = -1; }

        /// <summary>
        /// Знак. Представляет значение цвета в виде 24-битного целого числа без знака.
        /// </summary>
        public SignValue Sign { get; set; }
        /// <summary>
        /// Координата X на экране.
        /// </summary>
        public int ObjectX { get; set; }
        /// <summary>
        /// Координата Y на экране.
        /// </summary>
        public int ObjectY { get; set; }
        /// <summary>
        /// Получает или задаёт номер пройденного объекта процессором в рамках текущей сессии.
        /// Если значение меньше нуля, то объект считается непройденным.
        /// </summary>
        public int DiscountNumber { get; set; }

        /// <summary>
        /// Сравнивает заданные экземпляры.
        /// </summary>
        /// <param name="obj">Экземпляр для сравнения.</param>
        /// <returns>Возвращает значение, указывающее, как соотносятся заданные значения.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (obj.GetType() != typeof(MapObject))
                return 1;
            MapObject other = (MapObject)obj;
            return Sign.Value.CompareTo(other.Sign.Value);
        }

        /// <summary>
        /// Получает хеш-код текущего экземпляра.
        /// </summary>
        /// <returns>Возвращает хеш-код текущего экземпляра.</returns>
        public override int GetHashCode()
        {
            return Sign.Value;
        }

        /// <summary>
        /// Сравнивает значения знаков.
        /// </summary>
        /// <param name="obj">Экземпляр для сравнения.</param>
        /// <returns>Возвращает true - если значения знаков равны, в противном случае - false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            MapObject mpo = obj as MapObject;
            if (mpo == null)
                return false;
            return Sign == mpo.Sign;
        }

        /// <summary>
        /// Сравнивает значения знаков.
        /// </summary>
        /// <param name="s1">Первый экземпляр для сравнения.</param>
        /// <param name="s2">Второй экземпляр для сравнения.</param>
        /// <returns>Возвращает true - если значения знаков равны, в противном случае - false.</returns>
        public static bool operator ==(MapObject s1, object s2)
        {
            object sb = s1;
            if (sb == null && s2 == null)
                return true;
            if (sb == null)
                return false;
            return s1.Equals(s2);
        }

        /// <summary>
        /// Сравнивает значения знаков.
        /// </summary>
        /// <param name="s1">Первый экземпляр для сравнения.</param>
        /// <param name="s2">Второй экземпляр для сравнения.</param>
        /// <returns>Возвращает true - если значения знаков различаются, в противном случае - false.</returns>
        public static bool operator !=(MapObject s1, object s2)
        {
            object sb = s1;
            if (sb == null && s2 == null)
                return false;
            if (sb == null)
                return true;
            return !s1.Equals(s2);
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="val">Экземпляр для сравнения.</param>
        /// <returns>Возвращает 0 в случае равенства, -1 если значение текущего экземпляра меньше, 1 если больше.</returns>
        public int Compare(SignValue val)
        {
            if (Sign == val)
                return 0;
            return (Sign < val) ? -1 : 1;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра больше заданного.</returns>
        public static bool operator >(MapObject sv, MapObject val)
        {
            if (sv == null || val == null)
                return false;
            return sv.Sign > val.Sign;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра меньше заданного.</returns>
        public static bool operator <(MapObject sv, MapObject val)
        {
            if (sv == null || val == null)
                return false;
            return sv.Sign < val.Sign;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра больше или равно заданному.</returns>
        public static bool operator >=(MapObject sv, MapObject val)
        {
            if (sv == null || val == null)
                return false;
            return sv.Sign >= val.Sign;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра меньше или равно заданному.</returns>
        public static bool operator <=(MapObject sv, MapObject val)
        {
            if (sv == null || val == null)
                return false;
            return sv.Sign <= val.Sign;
        }
    }
}