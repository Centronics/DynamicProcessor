using DynamicProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ImitatorProcessorLibTest
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void MapTest0()
        {
            {
                Map mp = new Map();
                MapObject obj10 = new MapObject { Sign = new SignValue(10) };
                MapObject obj20 = new MapObject { Sign = new SignValue(20) };
                MapObject obj30 = new MapObject { Sign = new SignValue(30) };
                MapObject obj90 = new MapObject { Sign = new SignValue(90) };
                mp.Add(obj10);
                mp.Add(obj20);
                mp.Add(obj30);
                mp.Add(obj90);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(2)) == obj10);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(20)) == obj20);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(21)) == obj20);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(19)) == obj20);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(15)) == obj20);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(16)) == obj20);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(14)) == obj10);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(35)) == obj30);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(100)) == obj90);
                Assert.AreEqual(0, mp.CountDiscounted);
                obj90.DiscountNumber = 0;
                Assert.AreEqual(1, mp.CountDiscounted);
                Assert.AreEqual(true, mp.FindBySign(new SignValue(90)) == obj30);
                Assert.AreEqual(1, mp.CountDiscounted);
            }
            Map map = new Map();
            Assert.AreEqual(true, map.FindBySign(SignValue.MaxValue) == null);
            map.Add(new MapObject { Sign = new SignValue(135) });
            Assert.AreEqual(true, map.FindBySign(new SignValue(135)) == new MapObject { Sign = new SignValue(135) });
            map.RemoveObject(new SignValue(135));
            MapObject mo = new MapObject();
            MapObject mo1 = new MapObject();
            map.Add(mo);
            map.Add(mo1);
            MapObject mo2 = new MapObject();
            mo2.Sign = new SignValue(10);
            map.Add(mo2);
            MapObject mo3 = new MapObject();
            mo3.Sign = new SignValue(20);
            map.Add(mo3);
            MapObject mo4 = new MapObject();
            mo4.Sign = new SignValue(100);
            map.Add(mo4);
            MapObject mo5 = new MapObject();
            mo5.Sign = new SignValue(5000);
            map.Add(mo5);
            MapObject mo6 = new MapObject();
            mo6.Sign = new SignValue(100000);
            map.Add(mo6);
            MapObject mo7 = new MapObject();
            mo7.Sign = new SignValue(130000);
            map.Add(mo7);
            MapObject mo8 = new MapObject();
            mo8.Sign = new SignValue(999);
            map.Add(mo8);
            MapObject mo9 = new MapObject();
            mo9.Sign = new SignValue(1200);
            map.Add(mo9);
            Assert.AreEqual(0, map.CountDiscounted);
            Assert.AreEqual(mo7, map.FindBySign(SignValue.MaxValue));
            Assert.AreEqual(mo, map.FindBySign(SignValue.MinValue));
            Assert.AreEqual(mo4, map.FindBySign(new SignValue(100)));
            MapObject mof = map.FindBySign(new SignValue(100));
            Assert.AreEqual(mo4, mof);
            mof.DiscountNumber = 0;
            Assert.AreEqual(1, map.CountDiscounted);
            mof = map.FindBySign(new SignValue(13000));
            Assert.AreEqual(new MapObject { Sign = new SignValue(5000) }, mof);
            mof = map.FindBySign(new SignValue(100));
            Assert.AreEqual(new MapObject { Sign = new SignValue(20) }, mof);
            map = new Map();
            Assert.AreEqual(null, map.FindBySign(new SignValue(100)));
            map.Add(new MapObject { Sign = new SignValue(100), DiscountNumber = 0 });
            Assert.AreEqual(null, map.FindBySign(new SignValue(100)));
            map = new Map();
            MapObject nobj = new MapObject { Sign = new SignValue(20), DiscountNumber = 1 };
            map.Add(nobj);
            Assert.AreEqual(null, map.FindBySign(new SignValue(100)));
            Assert.AreEqual(1, map.CountDiscounted);
            nobj.DiscountNumber = -1;
            Assert.AreEqual(0, map.CountDiscounted);
            Assert.AreEqual(new MapObject { Sign = new SignValue(20) }, map.FindBySign(new SignValue(100)));
            nobj.DiscountNumber = 0;
            Assert.AreEqual(1, map.CountDiscounted);
            Assert.AreEqual(null, map.FindBySign(new SignValue(100)));
            map.RemoveObject(new SignValue(20));
            Assert.AreEqual(null, map.FindBySign(new SignValue(100)));
            MapObject trem = new MapObject();
            trem.Sign = new SignValue(151);
            map.ClearDiscount();
            Map newmap = new Map();
            MapObject one = new MapObject();
            newmap.Add(one);
            newmap = new Map();
            one = new MapObject();
            one.Sign = SignValue.MaxValue;
            newmap.Add(one);
            map = new Map();
            Assert.AreEqual(true, map.GetObjectByXY(7, 5) == null);
            map.Add(new MapObject { ObjectX = 5, ObjectY = 1, Sign = new SignValue(100) });
            map.Add(new MapObject { ObjectX = 6, ObjectY = 2, Sign = new SignValue(200) });
            Assert.AreEqual(true, map.GetObjectByXY(5, 1) == new MapObject { Sign = new SignValue(100) });
            Assert.AreEqual(true, map.GetObjectByXY(6, 2) == new MapObject { Sign = new SignValue(200) });
            Assert.AreEqual(true, map.GetObjectByXY(7, 5) == null);
            Assert.AreEqual(true, map[0] == new MapObject { ObjectX = 5, ObjectY = 1, Sign = new SignValue(100) });
            Assert.AreEqual(true, map[1] == new MapObject { ObjectX = 6, ObjectY = 2, Sign = new SignValue(200) });
            MapObject mpo = map.FindBySign(new SignValue(100));
            mpo.Sign = new SignValue(200);
            map.RemoveObject(new SignValue(200));
            map.RemoveObject(new SignValue(200));
            Assert.AreEqual(true, map.Count == 0);
        }

        [TestMethod]
        public void MapTest1()
        {
            Map map = new Map();
            {
                MapObject mpo1 = new MapObject();
                mpo1.Sign = new SignValue(0);
                map.Add(mpo1);
                mpo1.DiscountNumber = 0;
                Assert.AreEqual(1, map.Count);
                map.RemoveObject(new SignValue(0));
                Assert.AreEqual(0, map.Count);
                map.Add(mpo1);
                Assert.AreEqual(1, map.Count);
            }
            {
                MapObject mpo2 = new MapObject();
                mpo2.Sign = new SignValue(9);
                map.Add(mpo2);
            }
            {
                MapObject mpo3 = new MapObject();
                mpo3.Sign = new SignValue(15);
                map.Add(mpo3);
                mpo3.DiscountNumber = 1;
            }
            {
                MapObject mpo4 = new MapObject();
                mpo4.Sign = new SignValue(30);
                map.Add(mpo4);
            }
            {
                MapObject mpo5 = new MapObject();
                mpo5.Sign = new SignValue(40);
                map.Add(mpo5);
            }
            Assert.AreEqual(0, map[0].DiscountNumber);
            Assert.AreEqual(-1, map[1].DiscountNumber);
            Assert.AreEqual(1, map[2].DiscountNumber);
            Assert.AreEqual(-1, map[3].DiscountNumber);
            Assert.AreEqual(-1, map[4].DiscountNumber);
            map.ClearDiscount();
            Assert.AreEqual(-1, map[0].DiscountNumber);
            Assert.AreEqual(-1, map[1].DiscountNumber);
            Assert.AreEqual(-1, map[2].DiscountNumber);
            Assert.AreEqual(-1, map[3].DiscountNumber);
            Assert.AreEqual(-1, map[4].DiscountNumber);
            Map clMap1 = (Map)map.Clone();
            Map clMap2 = (Map)map.Clone();
            Assert.AreEqual(clMap1.Count, map.Count);
            Assert.AreEqual(clMap2.Count, map.Count);
            Assert.AreEqual(clMap1.Count, clMap2.Count);
            Assert.AreNotEqual((object)map, (object)clMap1);
            Assert.AreNotEqual((object)map, (object)clMap2);
            Assert.AreNotEqual((object)clMap1, (object)clMap2);
            for (int k = 0; k < map.Count; k++)
                Assert.AreEqual(true, clMap1[k] == map[k]);
            for (int k = 0; k < map.Count; k++)
                Assert.AreEqual(true, clMap2[k] == clMap1[k]);
            MapObject obj1 = clMap1[0];
            Assert.AreEqual(true, obj1 == map[0]);
            Assert.AreEqual(true, obj1 == clMap2[0]);
            obj1.Sign = new SignValue(23);
            Assert.AreEqual(false, obj1 == map[0]);
            Assert.AreEqual(false, obj1 == clMap2[0]);
        }

        [TestMethod]
        public void MapObjectNumeration()
        {
            Map map = new Map();
            for (int k = 0; k < Map.AllMax; k++)
                map.Add(new MapObject { Sign = new SignValue(k) });
            Assert.AreEqual(Map.AllMax, map.Count);
            map.ObjectNumeration();
            Assert.AreEqual(Map.AllMax, map.Count);

            Assert.AreEqual(0, map[0].ObjectX);
            Assert.AreEqual(0, map[0].ObjectY);
            Assert.AreEqual(0, map.FindBySign(new SignValue(0)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(0)).ObjectY);

            Assert.AreEqual(1, map[1].ObjectX);
            Assert.AreEqual(0, map[1].ObjectY);
            Assert.AreEqual(1, map.FindBySign(new SignValue(1)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(1)).ObjectY);

            Assert.AreEqual(2, map[2].ObjectX);
            Assert.AreEqual(0, map[2].ObjectY);
            Assert.AreEqual(2, map.FindBySign(new SignValue(2)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(2)).ObjectY);

            Assert.AreEqual(3, map[3].ObjectX);
            Assert.AreEqual(0, map[3].ObjectY);
            Assert.AreEqual(3, map.FindBySign(new SignValue(3)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(3)).ObjectY);

            Assert.AreEqual(4, map[4].ObjectX);
            Assert.AreEqual(0, map[4].ObjectY);
            Assert.AreEqual(4, map.FindBySign(new SignValue(4)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(4)).ObjectY);

            Assert.AreEqual(5, map[5].ObjectX);
            Assert.AreEqual(0, map[5].ObjectY);
            Assert.AreEqual(5, map.FindBySign(new SignValue(5)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(5)).ObjectY);

            Assert.AreEqual(6, map[6].ObjectX);
            Assert.AreEqual(0, map[6].ObjectY);
            Assert.AreEqual(6, map.FindBySign(new SignValue(6)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(6)).ObjectY);

            Assert.AreEqual(7, map[7].ObjectX);
            Assert.AreEqual(0, map[7].ObjectY);
            Assert.AreEqual(7, map.FindBySign(new SignValue(7)).ObjectX);
            Assert.AreEqual(0, map.FindBySign(new SignValue(7)).ObjectY);

            Assert.AreEqual(0, map[8].ObjectX);
            Assert.AreEqual(1, map[8].ObjectY);
            Assert.AreEqual(0, map.FindBySign(new SignValue(8)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(8)).ObjectY);

            Assert.AreEqual(1, map[9].ObjectX);
            Assert.AreEqual(1, map[9].ObjectY);
            Assert.AreEqual(1, map.FindBySign(new SignValue(9)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(9)).ObjectY);

            Assert.AreEqual(2, map[10].ObjectX);
            Assert.AreEqual(1, map[10].ObjectY);
            Assert.AreEqual(2, map.FindBySign(new SignValue(10)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(10)).ObjectY);

            Assert.AreEqual(3, map[11].ObjectX);
            Assert.AreEqual(1, map[11].ObjectY);
            Assert.AreEqual(3, map.FindBySign(new SignValue(11)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(11)).ObjectY);

            Assert.AreEqual(4, map[12].ObjectX);
            Assert.AreEqual(1, map[12].ObjectY);
            Assert.AreEqual(4, map.FindBySign(new SignValue(12)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(12)).ObjectY);

            Assert.AreEqual(5, map[13].ObjectX);
            Assert.AreEqual(1, map[13].ObjectY);
            Assert.AreEqual(5, map.FindBySign(new SignValue(13)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(13)).ObjectY);

            Assert.AreEqual(6, map[14].ObjectX);
            Assert.AreEqual(1, map[14].ObjectY);
            Assert.AreEqual(6, map.FindBySign(new SignValue(14)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(14)).ObjectY);

            Assert.AreEqual(7, map[15].ObjectX);
            Assert.AreEqual(1, map[15].ObjectY);
            Assert.AreEqual(7, map.FindBySign(new SignValue(15)).ObjectX);
            Assert.AreEqual(1, map.FindBySign(new SignValue(15)).ObjectY);

            Assert.AreEqual(0, map[16].ObjectX);
            Assert.AreEqual(2, map[16].ObjectY);
            Assert.AreEqual(0, map.FindBySign(new SignValue(16)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(16)).ObjectY);

            Assert.AreEqual(1, map[17].ObjectX);
            Assert.AreEqual(2, map[17].ObjectY);
            Assert.AreEqual(1, map.FindBySign(new SignValue(17)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(17)).ObjectY);

            Assert.AreEqual(2, map[18].ObjectX);
            Assert.AreEqual(2, map[18].ObjectY);
            Assert.AreEqual(2, map.FindBySign(new SignValue(18)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(18)).ObjectY);

            Assert.AreEqual(3, map[19].ObjectX);
            Assert.AreEqual(2, map[19].ObjectY);
            Assert.AreEqual(3, map.FindBySign(new SignValue(19)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(19)).ObjectY);

            Assert.AreEqual(4, map[20].ObjectX);
            Assert.AreEqual(2, map[20].ObjectY);
            Assert.AreEqual(4, map.FindBySign(new SignValue(20)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(20)).ObjectY);

            Assert.AreEqual(5, map[21].ObjectX);
            Assert.AreEqual(2, map[21].ObjectY);
            Assert.AreEqual(5, map.FindBySign(new SignValue(21)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(21)).ObjectY);

            Assert.AreEqual(6, map[22].ObjectX);
            Assert.AreEqual(2, map[22].ObjectY);
            Assert.AreEqual(6, map.FindBySign(new SignValue(22)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(22)).ObjectY);

            Assert.AreEqual(7, map[23].ObjectX);
            Assert.AreEqual(2, map[23].ObjectY);
            Assert.AreEqual(7, map.FindBySign(new SignValue(23)).ObjectX);
            Assert.AreEqual(2, map.FindBySign(new SignValue(23)).ObjectY);

            Assert.AreEqual(0, map[24].ObjectX);
            Assert.AreEqual(3, map[24].ObjectY);
            Assert.AreEqual(0, map.FindBySign(new SignValue(24)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(24)).ObjectY);

            Assert.AreEqual(1, map[25].ObjectX);
            Assert.AreEqual(3, map[25].ObjectY);
            Assert.AreEqual(1, map.FindBySign(new SignValue(25)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(25)).ObjectY);

            Assert.AreEqual(2, map[26].ObjectX);
            Assert.AreEqual(3, map[26].ObjectY);
            Assert.AreEqual(2, map.FindBySign(new SignValue(26)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(26)).ObjectY);

            Assert.AreEqual(3, map[27].ObjectX);
            Assert.AreEqual(3, map[27].ObjectY);
            Assert.AreEqual(3, map.FindBySign(new SignValue(27)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(27)).ObjectY);

            Assert.AreEqual(4, map[28].ObjectX);
            Assert.AreEqual(3, map[28].ObjectY);
            Assert.AreEqual(4, map.FindBySign(new SignValue(28)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(28)).ObjectY);

            Assert.AreEqual(5, map[29].ObjectX);
            Assert.AreEqual(3, map[29].ObjectY);
            Assert.AreEqual(5, map.FindBySign(new SignValue(29)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(29)).ObjectY);

            Assert.AreEqual(6, map[30].ObjectX);
            Assert.AreEqual(3, map[30].ObjectY);
            Assert.AreEqual(6, map.FindBySign(new SignValue(30)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(30)).ObjectY);

            Assert.AreEqual(7, map[31].ObjectX);
            Assert.AreEqual(3, map[31].ObjectY);
            Assert.AreEqual(7, map.FindBySign(new SignValue(31)).ObjectX);
            Assert.AreEqual(3, map.FindBySign(new SignValue(31)).ObjectY);

            Assert.AreEqual(0, map[32].ObjectX);
            Assert.AreEqual(4, map[32].ObjectY);
            Assert.AreEqual(0, map.FindBySign(new SignValue(32)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(32)).ObjectY);

            Assert.AreEqual(1, map[33].ObjectX);
            Assert.AreEqual(4, map[33].ObjectY);
            Assert.AreEqual(1, map.FindBySign(new SignValue(33)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(33)).ObjectY);

            Assert.AreEqual(2, map[34].ObjectX);
            Assert.AreEqual(4, map[34].ObjectY);
            Assert.AreEqual(2, map.FindBySign(new SignValue(34)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(34)).ObjectY);

            Assert.AreEqual(3, map[35].ObjectX);
            Assert.AreEqual(4, map[35].ObjectY);
            Assert.AreEqual(3, map.FindBySign(new SignValue(35)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(35)).ObjectY);

            Assert.AreEqual(4, map[36].ObjectX);
            Assert.AreEqual(4, map[36].ObjectY);
            Assert.AreEqual(4, map.FindBySign(new SignValue(36)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(36)).ObjectY);

            Assert.AreEqual(5, map[37].ObjectX);
            Assert.AreEqual(4, map[37].ObjectY);
            Assert.AreEqual(5, map.FindBySign(new SignValue(37)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(37)).ObjectY);

            Assert.AreEqual(6, map[38].ObjectX);
            Assert.AreEqual(4, map[38].ObjectY);
            Assert.AreEqual(6, map.FindBySign(new SignValue(38)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(38)).ObjectY);

            Assert.AreEqual(7, map[39].ObjectX);
            Assert.AreEqual(4, map[39].ObjectY);
            Assert.AreEqual(7, map.FindBySign(new SignValue(39)).ObjectX);
            Assert.AreEqual(4, map.FindBySign(new SignValue(39)).ObjectY);
        }

        [TestMethod]
        public void MapRemoveTest()
        {
            Map map = new Map();
            map.Add(new MapObject());
            map.Add(new MapObject());
            map.Add(new MapObject { ObjectX = 1, ObjectY = 0 });
            map.Add(new MapObject { ObjectX = 1, ObjectY = 1 });
            map.Add(new MapObject { ObjectX = 0, ObjectY = 1 });
            map.Add(new MapObject { ObjectX = 0, ObjectY = 0 });
            Assert.AreEqual(6, map.Count);
            map.RemoveObject(1, 1);
            for (int k = 0; k < map.Count; k++)
                if (map[k].ObjectX == 1 && map[k].ObjectY == 1)
                    throw new Exception("Объект (1,1) не удалён");
            Assert.AreEqual(5, map.Count);
            map.RemoveObject(0, 0);
            Assert.AreEqual(4, map.Count);
            map.RemoveObject(0, 1);
            for (int k = 0; k < map.Count; k++)
                if (map[k].ObjectX == 0 && map[k].ObjectY == 1)
                    throw new Exception("Объект (0,1) не удалён");
            map.RemoveObject(3, 3);
            Assert.AreEqual(3, map.Count);
            map.RemoveObject(1, 0);
            for (int k = 0; k < map.Count; k++)
                if (map[k].ObjectX == 1 && map[k].ObjectY == 0)
                    throw new Exception("Объект (1,0) не удалён");
            Assert.AreEqual(2, map.Count);
            map.RemoveObject(1, 0);
            for (int k = 0; k < map.Count; k++)
                if (map[k].ObjectX == 1 && map[k].ObjectY == 0)
                    throw new Exception("Объект (1,0) не удалён");
            Assert.AreEqual(2, map.Count);
            map.RemoveObject(-1, -1);
            Assert.AreEqual(0, map.Count);
            map.RemoveObject(0, 0);
            Assert.AreEqual(0, map.Count);
            map.RemoveObject(2, 2);
            Assert.AreEqual(0, map.Count);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Add(new MapObject());
            map.Add(new MapObject());
            Assert.AreEqual(2, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
        }

        [TestMethod]
        public void MapRemoveIndexTest()
        {
            Map map = new Map();
            map.Add(new MapObject { Sign = new SignValue(10) });
            map.Add(new MapObject { Sign = new SignValue(20) });
            Assert.AreEqual(2, map.Count);
            map.RemoveObject(0);
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual(new SignValue(20), map[0].Sign);
            map.RemoveObject(0);
            Assert.AreEqual(0, map.Count);
            map.Add(new MapObject { Sign = new SignValue() });
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Add(new MapObject { Sign = new SignValue(10000) });
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map = new Map();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Add(new MapObject { Sign = new SignValue() });
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Add(new MapObject { Sign = new SignValue(10000) });
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
            map.Clear();
            Assert.AreEqual(0, map.Count);
            Assert.AreEqual(0, map.CountDiscounted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MapTest2()
        {
            Map map = new Map();
            MapObject obj = map[0];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MapTest3()
        {
            Map map = new Map();
            map.Add(new MapObject { Sign = new SignValue(100) });
            MapObject obj = map[1];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MapTest4()
        {
            Map map = new Map();
            map.RemoveObject(0);
        }
    }
}