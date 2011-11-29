using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BlueChips.DanaManager.MainApp.Logic
{
    public class PublicMessageException : Exception, ISerializable
    {
        public string PublicMessage { get; protected set; }
        #region Construction
        /// <summary>
        /// build empty
        /// </summary>
        public PublicMessageException()
            : this(null, null,null)
        {
        }

        
        /// <summary>
        /// build with a message and an inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public PublicMessageException(string message, string publicMessage = null, Exception innerException = null)
            : base(message, innerException)
        {
        }
        #endregion

        #region Serialization
        /// <summary>
        /// protected constructor to use during deserialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PublicMessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            PublicMessage = info.GetValue("publicMessage", typeof(String)) as String;
        }

        /// <summary>
        /// provides support for deserialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("publicMessage", PublicMessage);
            base.GetObjectData(info, context);
        }
        #endregion
    }
}
