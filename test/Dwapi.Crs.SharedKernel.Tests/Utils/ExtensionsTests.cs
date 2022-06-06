﻿using System;
using System.Collections.Generic;
using Dwapi.Crs.SharedKernel.Utils;
using NUnit.Framework;
using Serilog;

namespace Dwapi.Crs.SharedKernel.Tests.TestData.Utils
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void should_Truncate()
        {
            var testItem = new string('x', 10);
            Console.WriteLine($"Before:{testItem}");
            Assert.True(testItem.Length==10);
            testItem=testItem.Truncate(5);
            Assert.True(testItem.Length==5);
            Console.WriteLine($"After:{testItem}");
        }
        
        [TestCase("Marital","Other","Unknown")]
        [TestCase("Marital","Xyz","Unknown")]
        [TestCase("Marital","Single","Single")]
        [TestCase("Marital","Unkown","Unknown")]
        [TestCase("Marital","Unknown","Unknown")]
        public void should_Transform(string cat, string val,string tVal)
        {
            var tf = val.Transfrom(cat);
           Assert.AreEqual(tVal,tf);
           Console.WriteLine($"After:{tf}");
        }
    }
}