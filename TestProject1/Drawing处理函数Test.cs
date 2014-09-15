using Core.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TestProject1
{
    
    
    /// <summary>
    ///这是 Drawing处理函数Test 的测试类，旨在
    ///包含所有 Drawing处理函数Test 单元测试
    ///</summary>
    [TestClass()]
    public class Drawing处理函数Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///缩放图像 的测试
        ///</summary>
        [TestMethod()]
        public void 缩放图像Test()
        {
            Image 图像 = null; // TODO: 初始化为适当的值
            int 指定宽度 = 0; // TODO: 初始化为适当的值
            int 指定高度 = 0; // TODO: 初始化为适当的值
            缩放方式 缩放方式 = new 缩放方式(); // TODO: 初始化为适当的值
            InterpolationMode 插值算法 = new InterpolationMode(); // TODO: 初始化为适当的值
            SmoothingMode 平滑模式 = new SmoothingMode(); // TODO: 初始化为适当的值
            CompositingQuality 合成质量 = new CompositingQuality(); // TODO: 初始化为适当的值
            Bitmap expected = null; // TODO: 初始化为适当的值
            Bitmap actual;
            actual = Drawing处理函数.缩放图像(图像, 指定宽度, 指定高度, 缩放方式, 插值算法, 平滑模式, 合成质量);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///缩放图像 的测试
        ///</summary>
        [TestMethod()]
        public void 缩放图像Test1()
        {
            Image 图像 = Bitmap.FromFile(@"E:\下载\6db1a742jw1dhybtayuajj.jpg");
            int 指定宽度 = 300; // TODO: 初始化为适当的值
            int 指定高度 = 300; // TODO: 初始化为适当的值
            缩放方式 缩放方式 = Core.Drawing.缩放方式.强制裁剪;
            Bitmap expected = null; // TODO: 初始化为适当的值
            Bitmap actual;
            actual = Drawing处理函数.缩放图像(图像, 指定宽度, 指定高度, 缩放方式);
            //Assert.AreEqual(expected, actual);
            actual.Save("i:\\test.jpg", ImageFormat.Jpeg);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
