using DynamicProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace ImitatorProcessorLibTest
{
    [TestClass]
    public class SignValueTest
    {
        [TestMethod]
        public void _SignValueTest()
        {
            SignValue sv = new SignValue(16777215);
            Assert.AreEqual(16777215, sv.Value);
            sv.Value = unchecked((int)0x00105110);
            Assert.AreEqual(0x00105110, sv.Value);
            sv.ValueColor = Color.White;
            int expected = Color.White.ToArgb() & 0x00FFFFFF;
            Assert.AreEqual(expected, sv.Value);
            Assert.AreEqual(Color.White.ToArgb(), sv.ValueColor.ToArgb());
            Assert.AreEqual(sv - 5000, 16772215);
            Assert.AreEqual(sv.Subtract(5000), 16772215);
            Assert.AreEqual(sv - 1000, sv.Subtract(1000));
            Assert.AreEqual(sv + 1000, sv.Add(1000));
            SignValue sv1 = new SignValue(Color.Red);
            SignValue svMinus1 = sv - sv1, svMinus2 = sv1 - sv;
            Assert.AreEqual(new SignValue(65535), svMinus1);
            Assert.AreEqual(new SignValue(65535), svMinus2);
            Assert.AreEqual(svMinus1, svMinus2);
            Assert.AreEqual(sv - sv1, sv.Subtract(sv1));
            sv.ValueColor = Color.Blue;
            Assert.AreEqual(sv + sv1, sv.Add(sv1));
            Assert.AreEqual(sv - sv1, sv1 - sv);
            Assert.AreEqual(sv + sv1, sv1 + sv);
            Assert.AreEqual(1, sv.CompareTo(null));
            Assert.AreEqual(1, sv.CompareTo(new object()));
            Assert.AreEqual(true, sv > 100);
            Assert.AreEqual(false, sv < 100);
            Assert.AreEqual(true, sv >= 100);
            Assert.AreEqual(false, sv <= 100);
            sv.ValueColor = Color.White;
            Assert.AreEqual(true, sv > sv1);
            Assert.AreEqual(false, sv < sv1);
            SignValue sveq = new SignValue(sv.Value);
            Assert.AreEqual(true, sv >= sveq);
            Assert.AreEqual(true, sv <= sveq);
            Assert.AreEqual(true, sv == new SignValue(Color.White));
            Assert.AreEqual(true, sv != sv1);
            Assert.AreEqual(true, sv == (Color.White.ToArgb() & 0x00FFFFFF));
            Assert.AreEqual(true, sv != 0);
            Assert.AreEqual(true, sv.Equals(new SignValue(Color.White)));
            Assert.AreEqual(false, sv.Equals(sv1));
            Assert.AreEqual(true, sv.Value == sv.GetHashCode());
            Assert.AreEqual(new SignValue(Color.White), SignValue.MaxValue);
            Assert.AreNotEqual(new SignValue(Color.Black), SignValue.MaxValue);
            Assert.AreEqual(new SignValue(Color.Black), SignValue.MinValue);
            Assert.AreNotEqual(new SignValue(Color.White), SignValue.MinValue);
            Assert.AreEqual(new SignValue(0x00FFFFFF), SignValue.MaxValue);
            Assert.AreNotEqual(new SignValue(0), SignValue.MaxValue);
            Assert.AreEqual(new SignValue(0), SignValue.MinValue);
            Assert.AreNotEqual(new SignValue(0x00FFFFFF), SignValue.MinValue);
            Assert.AreEqual(true, new SignValue(Color.White) == SignValue.MaxValue);
            Assert.AreEqual(true, new SignValue(Color.Black) != SignValue.MaxValue);
            Assert.AreEqual(true, new SignValue(Color.Black) == SignValue.MinValue);
            Assert.AreEqual(true, new SignValue(Color.White) != SignValue.MinValue);
            Assert.AreEqual(true, new SignValue(0x00FFFFFF) == SignValue.MaxValue);
            Assert.AreEqual(false, new SignValue(0) == SignValue.MaxValue);
            Assert.AreEqual(true, new SignValue(0) == SignValue.MinValue);
            Assert.AreEqual(false, new SignValue(0x00FFFFFF) == SignValue.MinValue);
            Assert.AreEqual(false, new SignValue(0x00FFFFFF) != SignValue.MaxValue);
            Assert.AreEqual(true, new SignValue(0) != SignValue.MaxValue);
            Assert.AreEqual(false, new SignValue(0) != SignValue.MinValue);
            Assert.AreEqual(true, new SignValue(0x00FFFFFF) != SignValue.MinValue);
            Assert.AreEqual(true, SignValue.MaxValue == new SignValue(Color.White));
            Assert.AreEqual(false, SignValue.MaxValue == new SignValue(Color.Black));
            Assert.AreEqual(false, SignValue.MinValue != new SignValue(Color.Black));
            Assert.AreEqual(true, SignValue.MinValue != new SignValue(Color.White));
            Assert.AreEqual(true, SignValue.MaxValue == new SignValue(0x00FFFFFF));
            Assert.AreEqual(false, SignValue.MaxValue == new SignValue(0));
            Assert.AreEqual(false, SignValue.MinValue != new SignValue(0));
            Assert.AreEqual(true, SignValue.MinValue != new SignValue(0x00FFFFFF));
            Assert.AreEqual("16777215", sv.ToString());
            SignValue av1 = new SignValue(5);
            Assert.AreEqual(new SignValue(5), av1.Average(new SignValue(6)));
            Assert.AreEqual(5, av1.Average(new SignValue(6)).Value);
            av1.Value = 101;
            Assert.AreEqual(101, av1.Average(new SignValue(101)).Value);
            Assert.AreEqual(new SignValue(101), av1.Average(new SignValue(101)));
            Assert.AreEqual(true, new SignValue(101) == av1.Average(new SignValue(101)));
            av1 = new SignValue(6);
            Assert.AreEqual(new SignValue(5), av1.Average(new SignValue(5)));
            Assert.AreEqual(new SignValue(6), av1.Average(new SignValue(6)));
            Assert.AreEqual(5, av1.Average(new SignValue(5)).Value);
            Assert.AreEqual(6, av1.Average(new SignValue(6)).Value);
            Assert.AreEqual(true, new SignValue(5) == av1.Average(new SignValue(5)));
            Assert.AreEqual(true, new SignValue(6) == av1.Average(new SignValue(6)));
            Assert.AreEqual(false, new SignValue(5) != av1.Average(new SignValue(5)));
            Assert.AreEqual(false, new SignValue(6) != av1.Average(new SignValue(6)));
            Assert.AreEqual(true, av1.Average(new SignValue(5)) == new SignValue(5));
            Assert.AreEqual(true, av1.Average(new SignValue(6)) == new SignValue(6));
            Assert.AreEqual(false, av1.Average(new SignValue(5)) != new SignValue(5));
            Assert.AreEqual(false, av1.Average(new SignValue(6)) != new SignValue(6));
            sv = new SignValue(Color.Black);
            sv = sv - SignValue.MaxValue;
            Assert.AreEqual(new SignValue(Color.White), sv);
            sv = new SignValue(Color.Black);
            sv = sv.Subtract(SignValue.MaxValue);
            Assert.AreEqual(new SignValue(Color.White), sv);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SignValueOut1()
        {
            new SignValue(16777216);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SignValueOut2()
        {
            new SignValue(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void SignValueAdd1()
        {
            SignValue sv = new SignValue(Color.White);
            int a = sv + int.MaxValue;
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void SignValueAdd2()
        {
            SignValue sv = new SignValue(Color.White);
            sv.Add(int.MaxValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SignValueAdd3()
        {
            SignValue sv = new SignValue(Color.White);
            sv.Add(new SignValue(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SignValueAdd4()
        {
            SignValue sv = new SignValue(Color.White);
            sv = sv + SignValue.MaxValue;
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void SignValueSub1()
        {
            SignValue sv = new SignValue(Color.Black);
            int a = sv - int.MinValue;
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void SignValueSub2()
        {
            SignValue sv = new SignValue(Color.Black);
            sv.Subtract(int.MinValue);
        }
    }
}