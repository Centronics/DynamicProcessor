using DynamicProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ImitatorProcessorLibTest
{
    [TestClass]
    public class MapObjectTest
    {
        [TestMethod]
        public void _MapObjectTest()
        {
            MapObject mo = new MapObject(), mo1 = new MapObject();
            mo.Sign = SignValue.MaxValue; mo1.Sign = SignValue.MaxValue;
            Assert.AreEqual(mo, mo1);
            mo.Sign = SignValue.MaxValue; mo1.Sign = SignValue.MinValue;
            Assert.AreNotEqual(mo, mo1);
            Assert.AreEqual(false, mo == null);
            Assert.AreEqual(true, mo != null);
            Assert.AreEqual(false, mo == new object());
            Assert.AreEqual(true, mo != new object());
            Assert.AreEqual(mo.GetHashCode(), mo.Sign.Value);
            Assert.AreEqual(1, mo.CompareTo(null));
            Assert.AreEqual(1, mo.CompareTo(new object()));
            Assert.AreEqual(1, mo.CompareTo(mo1));
            Assert.AreEqual(0, mo.CompareTo(mo));
            Assert.AreEqual(-1, mo1.CompareTo(mo));
            Assert.AreEqual(false, mo.Equals(null));
            Assert.AreEqual(false, mo.Equals(new object()));
            Assert.AreEqual(false, mo.Equals(mo1));
            Assert.AreEqual(true, mo.Equals(mo));
            Assert.AreEqual(false, mo.Equals(mo1));
            Assert.AreEqual(0, mo.Compare(mo.Sign));
            Assert.AreEqual(1, mo.Compare(mo1.Sign));
            Assert.AreEqual(-1, mo1.Compare(mo.Sign));
            Assert.AreEqual(true, mo > mo1);
            Assert.AreEqual(true, mo >= mo1);
            Assert.AreEqual(false, mo < mo1);
            Assert.AreEqual(false, mo <= mo1);
            mo1.Sign = SignValue.MaxValue;
            Assert.AreEqual(true, mo >= mo1);
            Assert.AreEqual(true, mo <= mo1);
            mo = null;
            Assert.AreEqual(false, mo == new MapObject());
            Assert.AreEqual(true, mo != new MapObject());
        }
    }
}