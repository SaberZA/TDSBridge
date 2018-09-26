using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDSBridge.Common.Utils;

namespace TDSBridge.Common.Header
{
    public class TDSHeader
    {
        public const int HEADER_SIZE = 8;

        protected byte[] _Buffer = new byte[HEADER_SIZE];
        protected byte[] PayloadBuffer;

         public TDSHeader(byte[] bPacket)
        {
            Array.Copy(bPacket, 0, this._Buffer, 0, HEADER_SIZE);
//            var payloadSize = _Buffer[2] * 0x100 + _Buffer[3] - HEADER_SIZE;
//            PayloadBuffer = new byte[payloadSize];
//            Array.Copy(bPacket, HEADER_SIZE, PayloadBuffer, 0, payloadSize);
        }

        public HeaderType Type { get { return (HeaderType)_Buffer[0]; } }
        public byte StatusBitMask { get { return _Buffer[1]; } }

//        public UInt32 Data_TotalLength
//        {
//            get
//            {
//                var totalLengthBytes = PayloadBuffer.SubArray(0, 4);
//                return BitConverter.ToUInt32(totalLengthBytes,0);
//            }
//        }
//        
//        public UInt32 Data_HeaderLength
//        {
//            get
//            {
//                var headerLengthBytes = PayloadBuffer.SubArray(4, 4);
//                return BitConverter.ToUInt32(headerLengthBytes,0);
//            }
//        }
//        
//        public UInt16 Data_HeaderType
//        {
//            get
//            {
//                var headerTypeBytes = PayloadBuffer.SubArray(8, 2);
//                return BitConverter.ToUInt16(headerTypeBytes,0);
//            }
//        }
//        
//        public UInt64 Data_TransactionDescriptor
//        {
//            get
//            {
//                var transactionDescriptorBytes = PayloadBuffer.SubArray(10, 8);
//                return BitConverter.ToUInt64(transactionDescriptorBytes,0);
//            }
//        }

        public int LengthIncludingHeader { 
            get
            {
                var lengthIncludingHeader = _Buffer[2] * 0x100 + _Buffer[3];
                return lengthIncludingHeader;
            } 
        }

        public int PayloadSize
        {
            get
            {
                return LengthIncludingHeader - HEADER_SIZE;
            }
        }

        public byte this[int idx]
        {
            get { return _Buffer[idx]; }
            set { _Buffer[idx] = value; }
        }

        public override string ToString()
        {
            return GetType().FullName +
                "[Type=" + Type +
                ";StatusBitMask=" + StatusBitMask +
                ";LengthIncludingHeader=" + LengthIncludingHeader +
                ";PayloadSize=" + PayloadSize +
                "]";
        }
    }
}
