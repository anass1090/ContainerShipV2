﻿using ClassLibraryContainerShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestContainerShip
{

    [TestClass]
    public class RowTests
    {

        [TestMethod]
        public void TryAddingContainer_ShouldAddContainer_WhenRowIsEmpty()
        {
            Row row = new Row(3, Row.Sides.Left);
            Container container = new Container(10, 1);

            bool result = row.TryAddingContainer(container);

            Assert.IsTrue(result);
            Assert.AreEqual(1, row.StackList[0].containers.Count);
        }

    }

}
