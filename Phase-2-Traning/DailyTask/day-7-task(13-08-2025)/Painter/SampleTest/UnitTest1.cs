using NUnit.Framework;
using System;

namespace Painter.Tests
{
    [TestFixture]
    public class PainterWorkerTests
    {
        private PaintBrush _paintBrush;
        private SprayGun _sprayGun;

        [SetUp]
        public void Setup()
        {
            _paintBrush = new PaintBrush();
            _sprayGun = new SprayGun();
        }

        [Test]
        public void Paint_WithPaintBrush_ReturnsBrushMessage()
        {
            var painter = new PainterWorker(_paintBrush);
            Assert.AreEqual("Painting with a brush", painter.Paint());
        }

        [Test]
        public void Paint_WithSprayGun_ReturnsSprayGunMessage()
        {
            var painter = new PainterWorker(_sprayGun);
            Assert.AreEqual("Painting with a spray gun", painter.Paint());
        }

        [Test]
        public void Constructor_WithNullTool_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new PainterWorker(null));
        }

        [Test]
        public void PaintBrush_UseTool_ReturnsCorrectMessage()
        {
            Assert.AreEqual("Painting with a brush", _paintBrush.UseTool());
        } 

        [Test]
        public void SprayGun_UseTool_ReturnsCorrectMessage()
        {
            Assert.AreEqual("Painting with a spray gun", _sprayGun.UseTool());
        }

        [Test]
        public void DifferentTools_HaveDifferentOutputs()
        {
            Assert.AreNotEqual(_paintBrush.UseTool(), _sprayGun.UseTool());
        }

        [Test]
        public void Painter_Paint_ResultIsNotEmpty()
        {
            var painter = new PainterWorker(_paintBrush);
            Assert.IsFalse(string.IsNullOrEmpty(painter.Paint()));
        }

        [Test]
        public void Painter_Paint_ResultContainsWordPainting()
        {
            var painter = new PainterWorker(_paintBrush);
            StringAssert.Contains("Painting", painter.Paint());
        }

        [Test]
        public void Painter_UsingBrush_ProducesSameResultEachTime()
        {
            var painter = new PainterWorker(_paintBrush);
            Assert.AreEqual(painter.Paint(), painter.Paint());
        }

        [Test]
        public void Painter_UsingSprayGun_ProducesSameResultEachTime()
        {
            var painter = new PainterWorker(_sprayGun);
            Assert.AreEqual(painter.Paint(), painter.Paint());
        }
    }
}
