using System;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;

namespace DynamicProcessor
{
    /// <summary>
    /// Представляет знак объекта карты.
    /// Знак является 24-битным целым беззнаковым числом типа int, представляющи цвет без прозрачности (Alpha).
    /// </summary>
    [Serializable]
    public struct SignValue : IComparable
    {
        /// <summary>
        /// Текущее значение знака.
        /// </summary>
        int _sign;

        /// <summary>
        /// Инициализирует экземпляр без параметра прозрачности (Alpha).
        /// Если он имеет ненулевое значение, то выбрасывается исключение ArgumentOutOfRangeException.
        /// </summary>
        /// <param name="sign">Значение знака.</param>
        public SignValue(int sign)
        {
            if (sign < 0 || sign > 0x00FFFFFF)
                throw new ArgumentOutOfRangeException("sign", "Знак должен быть в диапазоне от 0 до 16777215");
            _sign = sign;
        }

        /// <summary>
        /// Инициализирует экземпляр, игнорируя параметр прозрачности (Alpha).
        /// </summary>
        /// <param name="col">Значение знака.</param>
        public SignValue(Color col) { _sign = col.ToArgb() & unchecked((int)0x00FFFFFF); }

        /// <summary>
        /// Получает или задаёт значение без параметра прозрачности (Alpha).
        /// Если он имеет ненулевое значение, то выбрасывается исключение ArgumentOutOfRangeException.
        /// </summary>
        public int Value
        {
            get { return _sign; }
            set
            {
                if (value < 0 || value > 0x00FFFFFF)
                    throw new ArgumentOutOfRangeException("value", "Знак должен быть в диапазоне от 0 до 16777215");
                _sign = value;
            }
        }

        /// <summary>
        /// Получает или задаёт значение знака в виде цвета, игнорируя параметр прозрачности (Alpha).
        /// </summary>
        [XmlIgnoreAttribute]
        public Color ValueColor
        {
            get { return Color.FromArgb(_sign | unchecked((int)0xFF000000)); }
            set { _sign = value.ToArgb() & unchecked((int)0x00FFFFFF); }
        }

        /// <summary>
        /// Вычитает заданное значение из текущего экземпляра.
        /// </summary>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает разность значений.</returns>
        public int Subtract(int val)
        {
            return this - val;
        }

        /// <summary>
        /// Вычитает из текущего экземпляра заданный экземпляр, вычитая из большего меньшее.
        /// </summary>
        /// <param name="sv">Заданный экземпляр.</param>
        /// <returns>Возвращает разность значений.</returns>
        public SignValue Subtract(SignValue sv)
        {
            return this - sv;
        }

        /// <summary>
        /// Вычитает заданное значение из текущего экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает разность значений.</returns>
        public static int operator -(SignValue sv, int val)
        {
            return checked(sv.Value - val);
        }

        /// <summary>
        /// Суммирует значение текущего экземпляра с заданным значением.
        /// </summary>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает сумму значений.</returns>
        public int Add(int val)
        {
            return this + val;
        }

        /// <summary>
        /// Суммирует значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Заданный экземпляр.</param>
        /// <returns>Возвращает сумму значений.</returns>
        public SignValue Add(SignValue sv)
        {
            return this + sv;
        }

        /// <summary>
        /// Суммирует значение текущего экземпляра с заданным значением.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает сумму значений.</returns>
        public static int operator +(SignValue sv, int val)
        {
            return checked(sv.Value + val);
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="obj">Экземпляр для сравнения.</param>
        /// <returns>Возвращает 0 в случае равенства, -1 если значение текущего экземпляра меньше, 1 если больше.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (obj.GetType() != typeof(SignValue))
                return 1;
            SignValue sv = (SignValue)obj;
            return Value.CompareTo(sv.Value);
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра с заданным значением.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра больше заданного.</returns>
        public static bool operator >(SignValue sv, int val)
        {
            return sv.Value > val;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра с заданным значением.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра меньше заданного.</returns>
        public static bool operator <(SignValue sv, int val)
        {
            return sv.Value < val;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра с заданным значением.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра больше или равно заданному.</returns>
        public static bool operator >=(SignValue sv, int val)
        {
            return sv.Value >= val;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра с заданным значением.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра меньше или равно заданному.</returns>
        public static bool operator <=(SignValue sv, int val)
        {
            return sv.Value <= val;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра больше заданного.</returns>
        public static bool operator >(SignValue sv, SignValue val)
        {
            return sv.Value > val.Value;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра меньше заданного.</returns>
        public static bool operator <(SignValue sv, SignValue val)
        {
            return sv.Value < val.Value;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра больше или равно заданному.</returns>
        public static bool operator >=(SignValue sv, SignValue val)
        {
            return sv.Value >= val.Value;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра со значением заданного экземпляра.
        /// </summary>
        /// <param name="sv">Текущий экземпляр.</param>
        /// <param name="val">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает значение true в случае, если значение текущего экземпляра меньше или равно заданному.</returns>
        public static bool operator <=(SignValue sv, SignValue val)
        {
            return sv.Value <= val.Value;
        }

        /// <summary>
        /// Получает хеш-код текущего экземпляра.
        /// </summary>
        /// <returns>Возвращает хеш-код текущего экземпляра.</returns>
        public override int GetHashCode()
        {
            return _sign;
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
            if (obj.GetType() != typeof(SignValue))
                return false;
            SignValue cm = (SignValue)obj;
            return Equals(cm);
        }

        /// <summary>
        /// Сравнивает значения знаков.
        /// </summary>
        /// <param name="other">Экземпляр для сравнения.</param>
        /// <returns>Возвращает true - если значения знаков равны, в противном случае - false.</returns>
        public bool Equals(SignValue other)
        {
            return (Value == other.Value);
        }

        /// <summary>
        /// Получает минимальное значение знака.
        /// </summary>
        public static SignValue MinValue
        {
            get
            {
                return new SignValue(0);
            }
        }

        /// <summary>
        /// Получает максимальное значение знака.
        /// </summary>
        public static SignValue MaxValue
        {
            get
            {
                return new SignValue(0x00FFFFFF);
            }
        }

        /// <summary>
        /// Вычисляет среднее значение знака.
        /// </summary>
        /// <param name="s2">Второй знак.</param>
        /// <returns>Возвращает среднее значение знака.</returns>
        public SignValue Average(SignValue s2)
        {
            return new SignValue((Value + s2.Value) / 2);
        }

        /// <summary>
        /// Вычитает из значения текущего экземпляра значение указанного экземпляра, при этом всегда из большего вычитает меньшее.
        /// </summary>
        /// <param name="s1">Текущий экземпляр.</param>
        /// <param name="s2">Заданный экземпляр.</param>
        /// <returns>Возвращает разность знаков.</returns>
        public static SignValue operator -(SignValue s1, SignValue s2)
        {
            return new SignValue(Math.Abs(checked(s1.Value - s2.Value)));
        }

        /// <summary>
        /// Суммирует текущий экземпляр с указанным, выполняя проверку на переполнение.
        /// </summary>
        /// <param name="s1">Текущий экземпляр.</param>
        /// <param name="s2">Заданный экземпляр.</param>
        /// <returns>Возвращает сумму знаков.</returns>
        public static SignValue operator +(SignValue s1, SignValue s2)
        {
            return new SignValue(checked(s1.Value + s2.Value));
        }

        /// <summary>
        /// Сравнивает значения знаков.
        /// </summary>
        /// <param name="s1">Текущий экземпляр.</param>
        /// <param name="s2">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает true - если значения знаков равны, в противном случае - false.</returns>
        public static bool operator ==(SignValue s1, SignValue s2)
        {
            return s1.Equals(s2);
        }

        /// <summary>
        /// Сравнивает значения знаков.
        /// </summary>
        /// <param name="s1">Текущий экземпляр.</param>
        /// <param name="s2">Сопоставляемый экземпляр.</param>
        /// <returns>Возвращает true - если значения знаков различаются, в противном случае - false.</returns>
        public static bool operator !=(SignValue s1, SignValue s2)
        {
            return !s1.Equals(s2);
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра с указанным значением.
        /// </summary>
        /// <param name="s1">Текущий экземпляр.</param>
        /// <param name="sign">Значение.</param>
        /// <returns>Возвращает true - если значения знаков равны, в противном случае - false.</returns>
        public static bool operator ==(SignValue s1, int sign)
        {
            return s1.Value == sign;
        }

        /// <summary>
        /// Сравнивает значение текущего экземпляра с указанным значением.
        /// </summary>
        /// <param name="s1">Текущий экземпляр.</param>
        /// <param name="sign">Значение.</param>
        /// <returns>Возвращает true - если значения знаков различаются, в противном случае - false.</returns>
        public static bool operator !=(SignValue s1, int sign)
        {
            return s1.Value != sign;
        }

        /// <summary>
        /// Получает строковое представление значения текущего экземпляра.
        /// </summary>
        /// <returns>Возвращает строковое представление значения текущего экземпляра.</returns>
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}