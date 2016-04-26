using DynamicProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace ImitatorProcessorLibTest
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public void _CommandExecutorTest()
        {
            Map map = new Map();
            {
                MapObject mpo1 = new MapObject();
                mpo1.Sign = new SignValue(0);
                map.Add(mpo1);
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
            {
                Map clMap1 = (Map)map.Clone();
                Map clMap2 = (Map)map.Clone();
                Assert.AreEqual(clMap1.Count, clMap2.Count);
                int debCount = 0;
                Processor ce = new Processor(map);
                ce.ProcDebugObject = (Func<SignValue, SignValue, MapObject, int, bool>)((create, next, objgo, count) =>
                    {
                        switch (count)
                        {
                            case 0:
                                Assert.AreEqual(0, create.Value);
                                Assert.AreEqual(0, next.Value);
                                Assert.AreEqual(0, objgo.Sign.Value);
                                break;
                            case 1:
                                Assert.AreEqual(4, create.Value);
                                Assert.AreEqual(0, next.Value);
                                Assert.AreEqual(9, objgo.Sign.Value);
                                break;
                            case 2:
                                Assert.AreEqual(9, create.Value);
                                Assert.AreEqual(4, next.Value);
                                Assert.AreEqual(15, objgo.Sign.Value);
                                break;
                            case 3:
                                Assert.AreEqual(19, create.Value);
                                Assert.AreEqual(9, next.Value);
                                Assert.AreEqual(30, objgo.Sign.Value);
                                break;
                            case 4:
                                Assert.AreEqual(29, create.Value);
                                Assert.AreEqual(19, next.Value);
                                Assert.AreEqual(40, objgo.Sign.Value);
                                break;
                        }
                        return ++debCount <= map.Count;
                    });
                Assert.AreEqual(0, debCount);
                Assert.AreEqual(0, map.CountDiscounted);
                SignValue? result = ce.Run(new SignValue(0));
                Assert.AreEqual(map.Count, debCount);
                Assert.AreEqual(map.Count, map.CountDiscounted);
                map.ClearDiscount();
                Assert.AreEqual(0, map.CountDiscounted);
                Assert.AreEqual(new SignValue(29), result);
                debCount = 0;
                ce.ProcDebugObject = (Func<SignValue, SignValue, MapObject, int, bool>)((create, next, objgo, count) =>
                {
                    switch (count)
                    {
                        case 0:
                            Assert.AreEqual(37, create.Value);
                            Assert.AreEqual(45, next.Value);
                            Assert.AreEqual(29, objgo.Sign.Value);
                            break;
                        case 1:
                            Assert.AreEqual(28, create.Value);
                            Assert.AreEqual(37, next.Value);
                            Assert.AreEqual(19, objgo.Sign.Value);
                            break;
                        case 2:
                            Assert.AreEqual(18, create.Value);
                            Assert.AreEqual(28, next.Value);
                            Assert.AreEqual(9, objgo.Sign.Value);
                            break;
                        case 3:
                            Assert.AreEqual(11, create.Value);
                            Assert.AreEqual(18, next.Value);
                            Assert.AreEqual(4, objgo.Sign.Value);
                            break;
                        case 4:
                            Assert.AreEqual(5, create.Value);
                            Assert.AreEqual(11, next.Value);
                            Assert.AreEqual(0, objgo.Sign.Value);
                            break;
                    }
                    return ++debCount <= map.Count;
                });
                result = ce.Run(new SignValue(45));
                Assert.AreEqual(0, map[4].DiscountNumber);
                Assert.AreEqual(1, map[3].DiscountNumber);
                Assert.AreEqual(2, map[2].DiscountNumber);
                Assert.AreEqual(3, map[1].DiscountNumber);
                Assert.AreEqual(4, map[0].DiscountNumber);
                Assert.AreEqual(map.Count, debCount);
                Assert.AreEqual(map.Count, map.CountDiscounted);
                map.ClearDiscount();
                Assert.AreEqual(0, map.CountDiscounted);
                Assert.AreEqual(new SignValue(5), result);
                ce = new Processor(clMap1);
                debCount = 0;
                ce.ProcDebugObject = (Func<SignValue, SignValue, MapObject, int, bool>)((create, next, objgo, count) =>
                    {
                        switch (count)
                        {
                            case 0:
                                Assert.AreEqual(1, create.Value);
                                Assert.AreEqual(3, next.Value);
                                Assert.AreEqual(0, objgo.Sign.Value);
                                break;
                            case 1:
                                Assert.AreEqual(5, create.Value);
                                Assert.AreEqual(1, next.Value);
                                Assert.AreEqual(9, objgo.Sign.Value);
                                break;
                            case 2:
                                Assert.AreEqual(10, create.Value);
                                Assert.AreEqual(5, next.Value);
                                Assert.AreEqual(15, objgo.Sign.Value);
                                break;
                            case 3:
                                Assert.AreEqual(20, create.Value);
                                Assert.AreEqual(10, next.Value);
                                Assert.AreEqual(30, objgo.Sign.Value);
                                break;
                        }
                        return ++debCount <= map.Count - 2;
                    });
                result = ce.Run(new SignValue(3));
                Assert.AreEqual(0, clMap1[0].DiscountNumber);
                Assert.AreEqual(1, clMap1[1].DiscountNumber);
                Assert.AreEqual(2, clMap1[2].DiscountNumber);
                Assert.AreEqual(clMap1.Count - 1, debCount);
                Assert.AreEqual(clMap1.Count - 2, clMap1.CountDiscounted);
                clMap1.ClearDiscount();
                Assert.AreEqual(0, clMap1.CountDiscounted);
                Assert.AreEqual(null, result);
                ce.ProcDebugObject = (Func<SignValue, SignValue, MapObject, int, bool>)((create, next, objgo, count) =>
                    {
                        switch (count)
                        {
                            case 0:
                                Assert.AreEqual(42, create.Value);
                                Assert.AreEqual(45, next.Value);
                                Assert.AreEqual(40, objgo.Sign.Value);
                                break;
                            case 1:
                                Assert.AreEqual(36, create.Value);
                                Assert.AreEqual(42, next.Value);
                                Assert.AreEqual(30, objgo.Sign.Value);
                                break;
                            case 2:
                                Assert.AreEqual(25, create.Value);
                                Assert.AreEqual(36, next.Value);
                                Assert.AreEqual(15, objgo.Sign.Value);
                                break;
                            case 3:
                                Assert.AreEqual(17, create.Value);
                                Assert.AreEqual(25, next.Value);
                                Assert.AreEqual(9, objgo.Sign.Value);
                                break;
                            case 4:
                                Assert.AreEqual(8, create.Value);
                                Assert.AreEqual(17, next.Value);
                                Assert.AreEqual(0, objgo.Sign.Value);
                                break;
                        }
                        return ++debCount <= clMap1.Count;
                    });
                debCount = 0;
                for (int k = 0; k < clMap1.Count; k++)
                    clMap1[k].Sign = clMap2[k].Sign;
                result = ce.Run(new SignValue(45));
                Assert.AreEqual(0, clMap1[4].DiscountNumber);
                Assert.AreEqual(1, clMap1[3].DiscountNumber);
                Assert.AreEqual(2, clMap1[2].DiscountNumber);
                Assert.AreEqual(3, clMap1[1].DiscountNumber);
                Assert.AreEqual(4, clMap1[0].DiscountNumber);
                ce.ProcDebugObject = null;
                Assert.AreEqual(clMap1.Count, debCount);
                Assert.AreEqual(clMap1.Count, clMap1.CountDiscounted);
                clMap1.ClearDiscount();
                Assert.AreEqual(0, clMap1.CountDiscounted);
                Assert.AreEqual(new SignValue(8), result);
                ce = new Processor(clMap2);
                debCount = 0;
                ce.ProcDebugObject = (Func<SignValue, SignValue, MapObject, int, bool>)((create, next, objgo, count) =>
                {
                    switch (count)
                    {
                        case 0:
                            Assert.AreEqual(37, create.Value);
                            Assert.AreEqual(35, next.Value);
                            Assert.AreEqual(40, objgo.Sign.Value);
                            break;
                        case 1:
                            Assert.AreEqual(33, create.Value);
                            Assert.AreEqual(37, next.Value);
                            Assert.AreEqual(30, objgo.Sign.Value);
                            break;
                        case 2:
                            Assert.AreEqual(24, create.Value);
                            Assert.AreEqual(33, next.Value);
                            Assert.AreEqual(15, objgo.Sign.Value);
                            break;
                        case 3:
                            Assert.AreEqual(16, create.Value);
                            Assert.AreEqual(24, next.Value);
                            Assert.AreEqual(9, objgo.Sign.Value);
                            break;
                        case 4:
                            Assert.AreEqual(8, create.Value);
                            Assert.AreEqual(16, next.Value);
                            Assert.AreEqual(0, objgo.Sign.Value);
                            break;
                    }
                    return ++debCount <= clMap2.Count;
                });
                result = ce.Run(new SignValue(35));
                Assert.AreEqual(0, clMap2[4].DiscountNumber);
                Assert.AreEqual(1, clMap2[3].DiscountNumber);
                Assert.AreEqual(2, clMap2[2].DiscountNumber);
                Assert.AreEqual(3, clMap2[1].DiscountNumber);
                Assert.AreEqual(4, clMap2[0].DiscountNumber);
                Assert.AreEqual(clMap2.Count, clMap2.CountDiscounted);
                clMap2.ClearDiscount();
                Assert.AreEqual(0, clMap2.CountDiscounted);
                Assert.AreEqual(new SignValue(8), result);
            }
        }
    }
}