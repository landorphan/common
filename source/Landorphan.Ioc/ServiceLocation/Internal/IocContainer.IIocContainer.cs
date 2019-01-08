﻿namespace Landorphan.Ioc.ServiceLocation.Internal
{
   using System;
   using System.Collections.Generic;
   using Landorphan.Common;

   internal sealed partial class IocContainer : DisposableObject, IOwnedIocContainer, IIocContainerManager, IIocContainerRegistrar, IIocContainerResolver
   {
      /// <inheritdoc/>
      IReadOnlyCollection<IIocContainer> IIocContainer.Children => _children;

      /// <inheritdoc/>
      Boolean IIocContainer.IsRoot => _parent.IsNull();

      /// <inheritdoc/>
      IIocContainer IIocContainer.Parent => _parent;
   }
}