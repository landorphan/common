﻿using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("Landorphan.Common")]
//[assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
[assembly: AssemblyProduct("Landorphan.Common")]
[assembly: AssemblyTitle("Landorphan.Common")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en-US")]

#if BuildServer
[assembly: InternalsVisibleTo(
   "Landorphan.Common.Tests.Core, PublicKey=0024000004800000140100000602000000240000525341310008000001000100c33ba254dba2d79dba4c1fe136efb36588f73cc16afd66a43aebf7489ac82ab66c2fff42d8503459eff95139" +
   "e0ff790b6688513b19edf991760ef7ad02740a296c3ce5d098586b236b6abaa75038eb03af7d643d9a84b47bca0ed1cabbf4de9605c76007aab2e3abe7633d90860648ab1e38035fda5943971c6471249a0bd80f2ab04686c2110ebb10f909fb" +
   "773e3d87f67fb1e2f33ee791e4d8284fe9c6848ea81b3cf6a081df100716a10c68e0dd5219ff1657995777bf03961afdc3c09b040edb5a36baab5075410507f3ba1d9f59c6bd67401819abbbe7712b5f473e052b96efe98c39210bb485c1ba04" +
   "89ed396983beb914a3b2443f6aa2be4f49a88bb0")]
[assembly: InternalsVisibleTo(
   "Landorphan.Common.Tests.NetFx, PublicKey=0024000004800000140100000602000000240000525341310008000001000100c33ba254dba2d79dba4c1fe136efb36588f73cc16afd66a43aebf7489ac82ab66c2fff42d8503459eff9513" +
   "9e0ff790b6688513b19edf991760ef7ad02740a296c3ce5d098586b236b6abaa75038eb03af7d643d9a84b47bca0ed1cabbf4de9605c76007aab2e3abe7633d90860648ab1e38035fda5943971c6471249a0bd80f2ab04686c2110ebb10f909f" +
   "b773e3d87f67fb1e2f33ee791e4d8284fe9c6848ea81b3cf6a081df100716a10c68e0dd5219ff1657995777bf03961afdc3c09b040edb5a36baab5075410507f3ba1d9f59c6bd67401819abbbe7712b5f473e052b96efe98c39210bb485c1ba0" +
   "489ed396983beb914a3b2443f6aa2be4f49a88bb0")]
#else
[assembly: InternalsVisibleTo("Landorphan.Common.Tests.Core")]
[assembly: InternalsVisibleTo("Landorphan.Common.Tests.NetFx")]
#endif
