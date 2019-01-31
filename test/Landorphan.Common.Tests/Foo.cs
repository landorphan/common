namespace Landorphan.Common.Tests
{
   using System.IO;
   using FluentAssertions;
   using Landorphan.TestUtilities;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class Foo
   {
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void Bar()
      {
         // ReSharper disable once StringLiteralTypo
         Directory.Exists(@"\\devresourcesdiag393.file.core.windows.net\landorphan-abstractions-tests-test-target\SharedFolderEveryoneFullControl").Should().BeTrue();
      }
   }
}
