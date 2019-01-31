namespace Landorphan.Common.Tests.Extensions
{
   using System;
   using System.IO;
   using FluentAssertions;
   using Landorphan.TestUtilities;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // ReSharper disable InconsistentNaming

   public static class TestExtensions_Tests
   {
      [TestClass]
      public class When_I_call_IsStatic_on_a_static_type : ArrangeActAssert
      {
         private Boolean actual;

         protected override void ActMethod()
         {
            actual = typeof(File).IsStatic();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_true()
         {
            actual.Should().BeTrue();
         }
      }

      [TestClass]
      public class When_I_call_IsStatic_on_a_non_static_type : ArrangeActAssert
      {
         private Boolean actual = true;

         protected override void ActMethod()
         {
            actual = typeof(FileInfo).IsStatic();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_false()
         {
            actual.Should().BeFalse();
         }
      }
   }
}