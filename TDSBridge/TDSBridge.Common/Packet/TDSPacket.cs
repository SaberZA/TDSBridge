﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDSBridge.Common.Header;
using TDSBridge.Common.Utils;

namespace TDSBridge.Common.Packet
{
    public class TDSPacket
    {
        //static int iCnt = 0;

        protected Header.TDSHeader _header = null;        
        protected byte[] _payload = null;

        public byte[] Payload { get { return _payload; } }
        public Header.TDSHeader Header { get { return _header; } }

        public TDSPacket(byte[] bBuffer)
        {
            _header = new Header.TDSHeader(bBuffer);

            _payload = new byte[_header.LengthIncludingHeader - TDSHeader.HEADER_SIZE];
            Array.Copy(bBuffer, TDSHeader.HEADER_SIZE, _payload, 0, _payload.Length);
        }

        public TDSPacket(byte[] bHeader, byte[] bPayload, int iPayloadSize)
        {
            _header = new Header.TDSHeader(bHeader);

            _payload = new byte[iPayloadSize];
            Array.Copy(bPayload, 0, _payload, 0, iPayloadSize);
        }
        
        public UInt32 Data_TotalLength
        {
            get
            {
                var totalLengthBytes = _payload.SubArray(0, 4);
                return BitConverter.ToUInt32(totalLengthBytes,0);
            }
        }
        
        public UInt32 Data_HeaderLength
        {
            get
            {
                var headerLengthBytes = _payload.SubArray(4, 4);
                return BitConverter.ToUInt32(headerLengthBytes,0);
            }
        }
        
        public UInt16 Data_HeaderType
        {
            get
            {
                var headerTypeBytes = _payload.SubArray(8, 2);
                return BitConverter.ToUInt16(headerTypeBytes,0);
            }
        }
        
        public UInt64 Data_TransactionDescriptor
        {
            get
            {
                var transactionDescriptorBytes = _payload.SubArray(10, 8);
                return BitConverter.ToUInt64(transactionDescriptorBytes,0);
            }
        }
        
        public UInt32 Data_OutstandingRequestCount
        {
            get
            {
                var outstandingRequestCountBytes = _payload.SubArray(18, 4);
                return BitConverter.ToUInt32(outstandingRequestCountBytes,0);
            }
        }
        
        public string Data_SqlText
        {
            get
            {
                var sqlTextBytes = _payload.SubArray(22, _payload.Length - 22);
                return Encoding.Unicode.GetString(sqlTextBytes);
            }
        }

        public override string ToString()
        {
            return base.ToString() + "[Header=" + Header + "]";
        }
    }
}
