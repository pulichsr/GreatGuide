using System;

namespace Nucleo.GoodGuide.Types
{
  public interface IDestinationTypeImageIndexMapper
  {
    Int16 DestinationTypeToImageIndex(Int32 destinationTypeId);
  }
}
