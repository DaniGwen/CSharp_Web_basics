namespace SIS.MvcFramework.Tests
{
    using SIS.MvcFramework.ViewEngine;
    using System.IO;
    using Xunit;
    public class TestSisViewEngine
    {
        [Theory]
        [InlineData("TestWithoutCSharpCode")]
        [InlineData("UseForForeachAndIf")]
        [InlineData("UseModelData")]
        public void TestGetHtml(string testFileName)
        {
            IViewEngine viewEngine = new SisViewEngine();

            var viewFileName = $"ViewTests/{testFileName}.html";
            var expectedResultFileName = $"viewTests/{testFileName}.Result.html";

            var viewContent = File.ReadAllText(viewFileName);
            var expectedResult = File.ReadAllText(expectedResultFileName);

            var actualResult = viewEngine.GetHtml<object>(viewContent, null);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
