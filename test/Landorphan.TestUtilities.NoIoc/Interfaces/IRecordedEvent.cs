namespace Landorphan.TestUtilities
{
   using System;

   /// <summary>
   /// Represents a recorded event.
   /// </summary>
   public interface IRecordedEvent
   {
      /// <summary>
      /// Gets the actual event source.
      /// </summary>
      /// <value>
      /// The actual event source.
      /// </value>
      Object ActualEventSource { get; }

      /// <summary>
      /// Gets the additional data passed to the event handler.
      /// </summary>
      /// <value>
      /// Additional data passed to the event handler.
      /// </value>
      EventArgs AdditionalData { get; }

      /// <summary>
      /// Gets the name of the event.
      /// </summary>
      /// <value>
      /// The name of the event.
      /// </value>
      String EventName { get; }

      /// <summary>
      /// Gets the expected event source.
      /// </summary>
      /// <value>
      /// The expected event source.
      /// </value>
      Object ExpectedEventSource { get; }

      /// <summary>
      /// Gets the managed thread identifier of the thread that fired the event.
      /// </summary>
      /// <value>
      /// The managed thread identifier of the thread that fired the event.
      /// </value>
      Int32 ManagedThreadId { get; }

      /// <summary>
      /// Gets the relative sequence number.
      /// </summary>
      /// <value>
      /// The relative sequence number.
      /// </value>
      Int64 SequenceNumber { get; }

      /// <summary>
      /// Gets the timestamp when the event was recorded.
      /// </summary>
      /// <value>
      /// The timestamp when the event was recorded.
      /// </value>
      DateTimeOffset Timestamp { get; }
   }
}