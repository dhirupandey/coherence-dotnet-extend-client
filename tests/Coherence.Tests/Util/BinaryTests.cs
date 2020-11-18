/*
 * Copyright (c) 2000, 2020, Oracle and/or its affiliates.
 *
 * Licensed under the Universal Permissive License v 1.0 as shown at
 * http://oss.oracle.com/licenses/upl.
 */
using System.IO;

using NUnit.Framework;

namespace Tangosol.Util
{
    [TestFixture]
    public class BinaryTests
    {
        [Test]
        public void TestBinary()
        {
            Binary b1 = new Binary();
            Assert.IsNotNull(b1);
            MemoryStream stream = new MemoryStream();
            b1.WriteTo(stream);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 0);
            byte[] bytes = b1.ToByteArray();
            Assert.IsNotNull(bytes);
            Assert.AreEqual(bytes.Length, 0);
            
            stream = new MemoryStream();
            byte[] arr = new byte[] {1, 2, 3, 4, 5};
            Binary b2 = new Binary(arr);
            Assert.IsNotNull(b2);
            b2.WriteTo(stream);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 5);
            bytes = b2.ToByteArray();
            Assert.IsNotNull(bytes);
            Assert.AreEqual(bytes.Length, 5);
            
            stream = new MemoryStream();
            Binary b3 = new Binary(arr, 1, 2);
            Assert.IsNotNull(b3);
            b3.WriteTo(stream);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 2);
            bytes = b3.ToByteArray();
            Assert.IsNotNull(bytes);
            Assert.AreEqual(bytes.Length, 2);
        }

        [Test]
        public void TestCalculateNaturalPartition()
        {
            Binary bin   = new Binary(new byte[] {124});
            int    nHash = bin.GetHashCode();
            Assert.AreEqual(nHash,     bin.CalculateNaturalPartition(0));
            Assert.AreEqual(nHash % 7, bin.CalculateNaturalPartition(7));

            // Check portability. Verify that partition values are consistent
            // with those generated by Java and C++
            Assert.AreEqual(-1, (new Binary()).CalculateNaturalPartition(0));

            byte[] ab = new byte[256];
            for (int i = 0, b = -128; i < 256; ++i, ++b)
            {
                ab[i] = (byte) b;
            }

            int[] aiExpected = new int[]
            {
                -1069182126, -1220369468,  776729214,   1498202856, -953657525,  -1339070499,  690370151,   1579222769,
                -828499104,  -1181144074,  546339404,   1469532890, -906764423,  -1091244049,  670940757,   1358597827,
                -571309258,  -1426738272,  872210970,   1157354124, -627095761,  -1382516807,  881927683,   1133909653,
                -752284924,  -1540473966,  1025993256,  1243634366, -733688035,  -1555824757,  977972785,   1296932519,
                -81022054,   -1943239924,  354800310,   1646453280, -62490749,   -1958656235,  306714287,   1699685945,
                -168805464,  -2097738946,  469654148,   1828284946, -224526415,  -2053451993,  479436445,   1804905995,
                -425942018,  -1852075160,  143835858,   2140533316, -504272921,  -1762240655,  268371659,   2029532765,
                -397988916,  -1623188646,  105466592,   1900968566, -282398763,  -1741824189,  19173113,    1982053999,
                -1231433022, -1046551980,  1486337006,  797999992,  -1309403429, -957143475,   1610250231,  687687521,
                -1203610896, -817534362,   1447836636,  558566218,  -1087398167, -936857985,   1361182661,  640077651,
                -1422998874, -601230800,   1159766922,  841454364,  -1404893505, -616286679,   1112369043,  894064389,
                -1510651244, -755860990,   1274751928,  1023154990, -1567060339, -710951397,   1284960161,  999415607,
                -1913130486, -84884836,    1677300518,  352232368,  -1969605101, -40040827,    1687443263,  328427433,
                -2094237128, -198489426,   1830951700,  438643586,  -2076066271, -213479753,   1783619341,  491319195,
                -1874795922, -414723336,   2119074626,  155825108,  -1758648713, -534112543,   2032355163,  237270989,
                -1633981860, -375629110,   1888815984,  127024102,  -1711886779, -286155053,   2012794729,  16777215,
                 771559538,   1526341860, -1007455906, -1259060792,  714134635,   1570235645, -996231865,  -1281784367,
                 589731904,   1411492054, -852952724,  -1171273222,  608918617,   1397517519, -901431947,  -1119744541,
                 810156054,   1196241024, -565944006,  -1455205972,  925352975,   1075901593, -651582173,  -1372678731,
                 1049724964,  1234614450, -794826488,  -1483155042,  972835901,   1325104299, -671994607,  -1594548857,
                 378745018,   1637089324, -123907690,  -1885708032,  301921443,   1727644725, -1010289,    -1997036263,
                 407419016,   1867483166, -163128924,  -2126386894,  522550417,   1747078151, -248832579,  -2043925205,
                 186917086,   2082672712, -450215438,  -1842515612,  206169287,   2068763729, -498629141,  -1790921347,
                 100641004,   1928894586, -336475712,  -1661535914,  43150581,    1972722787, -325317159,  -1684325041,
                 1528910306,  740712820,  -1255198514, -1037565864,  1548523003,  726377837,  -1304234793, -985283519,
                 1442503120,  587065670,  -1141589764, -856455062,   1385635273,  630205791,  -1130791707, -878818189,
                 1184252294,  831615248,  -1466425174, -543223748,   1107002783,  922531081,  -1342839629, -655174619,
                 1213057460,  1061878050, -1505515368, -784033778,   1327500717,  942095675,  -1590793087, -701932521,
                 1615819050,  390611388,  -1908338682, -112844656,   1730327859,  270894501,  -1993550817, -30677879,
                 1855256856,  429115790,  -2137352140, -140662622,   1777941761,  519966103,  -2013832147, -252678981,
                 2113429838,  184504792,  -1812594590, -453955340,   2056627543,  227710401,  -1801730949, -476252947,
                 1931733372,  69523946,   -1657960368, -366298938,   1951280485,  55123443,   -1707062199, -314082081
            };

            Assert.AreEqual(-2018391514, (new Binary(ab)).CalculateNaturalPartition(0));

            for (int i = 0; i < ab.Length; ++i)
            {
                bin   = new Binary(ab, i, 1);
                nHash = bin.GetHashCode();

                int nPart = bin.CalculateNaturalPartition(0);
                Assert.AreEqual(aiExpected[i], nPart);
                Assert.AreEqual(nHash,         nPart);
            }
        }

        [Test]
        public void TestEquals()
        {
            Binary b1 = new Binary();
            Binary b2 = Binary.NO_BINARY;
            byte[] ba1 = new byte[0];
            byte[] ba2;

            Assert.AreEqual(b1, b1);
            Assert.AreNotEqual(b1, ba1);
            Assert.AreEqual(b1, b2);

            ba1 = new byte[] {1, 5, 10};
            ba2 = new byte[] {1, 5};
            b1 = new Binary(ba1);
            b2 = new Binary(ba2);
            Assert.AreNotEqual(b1, b2);

            ba2 = new byte[] {1, 5, 100};
            b2 = new Binary(ba2);
            Assert.AreNotEqual(b1, b2);

            ba2 = new byte[] { 1, 5, 10 };
            b2 = new Binary(ba2);
            Assert.AreEqual(b1, b2);
        }
    }
}